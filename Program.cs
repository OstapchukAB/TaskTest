#region Example 01
//Task[] tasks = new Task[2];

//tasks[0] = new Task(() =>      // внешняя задача
//{
//    Console.WriteLine("Outer task starting...");
//    tasks[1] = new Task(() =>  // вложенная задача
//    {
//        Console.WriteLine("Inner task starting...");
//        Thread.Sleep(2000);
//        Console.WriteLine("Inner task finished.");
//    }, TaskCreationOptions.AttachedToParent);
//    //Console.WriteLine("Outer task runnig...");
//    tasks[1].Start();
//});
//tasks[0].Start();
////outer.Wait(); // ожидаем выполнения внешней задачи
//Task.WaitAll(tasks[0]);
//Console.WriteLine($"completedOuterTask:{tasks[0]?.IsCompletedSuccessfully}");
//Console.WriteLine($"completedInnerTask:{tasks[1]?.IsCompletedSuccessfully}");
//Console.WriteLine("End of Main");
#endregion
#region Example 02
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;

//(string Name,DateTime Dt) tt;
//var tasks = new Task<(string Name, DateTime Dt)>[3];
//for (var i = 0; i < tasks.Length; i++)
//{
//    var j = i;
//    tasks[i] = new(() =>
//    {
//        Console.WriteLine($"task {j} run {string.Format("{0:o}",DateTime.Now)}");
//        Thread.Sleep(2000);
//        tt = ($"Finish task {j} {string.Format("{0:o}", DateTime.Now)}", DateTime.Now);
//        return tt;
//    });
//    tasks[i].Start();
//}
////Task.WaitAny(tasks);    
////Console.WriteLine(task.Result);
////Console.WriteLine(tasks[0].Result);
//Task.WaitAll(tasks);
//foreach (var task in tasks.OrderByDescending(x=>x.Result.Dt.Ticks))
//{
//    //Thread.Sleep(1000);
//    Console.WriteLine(task.Result);
//}

////Console.WriteLine($"task.IsCompletedSuccessfully: {task.IsCompletedSuccessfully}");
//Console.WriteLine($"Завершение метода Main {DateTime.Now}");


#endregion
#region continuation task
//Task task1 = new Task(() =>
//{
//    Console.WriteLine($"Id задачи (task1): {Task.CurrentId}");
//});

//// задача продолжения - task2 выполняется после task1
//Task task2 = task1.ContinueWith(PrintTask);

//task1.Start();

//// ждем окончания второй задачи
//task2.Wait();
//Console.WriteLine("Конец метода Main");


//void PrintTask(Task t)
//{
//    Console.WriteLine($"Id задачи (task2): {Task.CurrentId}");
//    Console.WriteLine($"Id предыдущей задачи (task1): {t.Id}");
//    Thread.Sleep(3000);
//}
#endregion
#region Parallel.Invoke
//// метод Parallel.Invoke выполняет три метода
//Parallel.Invoke(
//    Print,
//    () =>
//    {
//        Console.WriteLine($"Выполняется задача {Task.CurrentId}");
//        Thread.Sleep(3000);
//    },
//    () => Square(5)
//);
//Console.WriteLine($"Завершение метода Main");
//void Print()
//{
//    Console.WriteLine($"Выполняется задача Print {Task.CurrentId}");
//    Thread.Sleep(3000);
//}
//// вычисляем квадрат числа
//void Square(int n)
//{
//    Console.WriteLine($"Выполняется задача Square {Task.CurrentId}");
//    Thread.Sleep(3000);
//    Console.WriteLine($"Результат {n * n}");
//}
#endregion
#region Async Example 01
//class Program
//{
//    async static Task Main(string[] args)
//    {
//        while (true)
//        {
//            var task = await PrintAsync();   // вызов асинхронного метода
//            Console.WriteLine("Некоторые действия в методе Main");
//            if (task.IsCompletedSuccessfully)
//                break;

//        }
//    }
//        static void Print()
//        {
//            Thread.Sleep(3000);     // имитация продолжительной работы
//            Console.WriteLine("Hello METANIT.COM");
//        }

//        // определение асинхронного метода
//        async static Task<Task> PrintAsync()
//        {

//            Console.WriteLine("Начало метода PrintAsync"); // выполняется синхронно
//            Task task = new Task(() => Print());                // выполняется асинхронно
//            task.Start();
//            await task;

//            Console.WriteLine("Конец метода PrintAsync");
//            return task;
//        }
//    }
#endregion
#region Async Example 02
//class Program
//{
//    async static Task Main(string[] args)
//    {
//        Console.WriteLine($"Main Works Start {DateTime.Now}");
//        var task = PrintAsync("Hello Metanit.com"); // задача начинает выполняться
//                                                    //while (task.IsCompleted ==false)
//                                                    //{
//        //await task;
//        Console.WriteLine(task.Result.ToString());
//        Console.WriteLine($"Main Works End {DateTime.Now}");

//       // }

//        //await task; // ожидаем завершения задачи
//    }
//    async static Task<DateTime> PrintAsync(string message)
//    {
//        await Task.Delay(3000);
//        Console.WriteLine(message);
//        return  DateTime.Now;
//    }
//}
#endregion
#region Async Example 03
using System.Threading.Tasks;
class Program
{
    
    async static Task Main(string[] args)
    {
        // определяем и запускаем задачи
        Task<int>[] tasks = new Task<int> [100];
        for (int i=0;i<tasks.Length;i++)
        {
            var j = i;
            tasks[j] = SquareAsync(j);
        }


       
        var result=await Task.WhenAny(tasks);
        Console.WriteLine($"id={result.Id} result:{result.Result} {result.Status}");
        var results = await Task.WhenAll(tasks);
        foreach (var task in results) 
            Console.WriteLine(task);
    }
    async static Task<int> SquareAsync(int n)
    {
        await Task.Delay(3000);
        return n * n;
    }
}
#endregion