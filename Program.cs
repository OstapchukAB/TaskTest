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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

(string Name,DateTime Dt) tt;
var tasks = new Task<(string Name, DateTime Dt)>[3];
for (var i = 0; i < tasks.Length; i++)
{
    var j = i;
    tasks[i] = new(() =>
    {
        Console.WriteLine($"task {j} run {string.Format("{0:o}",DateTime.Now)}");
        Thread.Sleep(2000);
        tt = ($"Finish task {j} {string.Format("{0:o}", DateTime.Now)}", DateTime.Now);
        return tt;
    });
    tasks[i].Start();
}
//Task.WaitAny(tasks);    
//Console.WriteLine(task.Result);
//Console.WriteLine(tasks[0].Result);
Task.WaitAll(tasks);
foreach (var task in tasks.OrderByDescending(x=>x.Result.Dt.Ticks))
{
    //Thread.Sleep(1000);
    Console.WriteLine(task.Result);
}

//Console.WriteLine($"task.IsCompletedSuccessfully: {task.IsCompletedSuccessfully}");
Console.WriteLine($"Завершение метода Main {DateTime.Now}");


#endregion