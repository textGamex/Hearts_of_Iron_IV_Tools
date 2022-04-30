namespace Test;

using Hearts_of_Iron_IV_Tools.Country;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class Test
{
    private const string test = "]lapjf2o 2u30f920r9209ru09a0dkasnocbsdpugf9    w[ [ewqf     0230t8y3462\r\n=9r4213r\\   qw\\udqowjd09nqu2-9 r\\N\\N\\nbxnz=1 qdjwe[fu-2039ru=nznnUP9SDF=0N3CXN-Z2900H(UG80uhPG806rUp8d69029E2309XQWN9-O";

    private const uint INDUSTRIAL_COMPLEX_MANUFACTURING_COST = 10800;
    private const uint ARMS_FACTORY_MANUFACTURING_COST = 7200;
    private const byte INDUSTRIAL_COMPLEX_OUTPUT = 5;
    private const byte MAX_INDUSTRIAL_COMPLEX_INVESTMENT = 15;

    static ConcurrentBag<State> states = new ConcurrentBag<State>();
    public static void Main()
    {
        string path = @"D:\STEAM\steamapps\common\Hearts of Iron IV\history\states";
        var files = new DirectoryInfo(path).GetFiles();

        System.Diagnostics.Stopwatch oTime = new System.Diagnostics.Stopwatch();   //定义一个计时对象  
        oTime.Start(); //开始计时         

        Task[] tasks = new Task[files.Length];
        int count = 0;
        Console.WriteLine(files.Length);
        foreach (var file in files)
        {
            tasks[count++] = Task.Factory.StartNew(() => states.Add(State.ByPath(file.FullName)),
                TaskCreationOptions.LongRunning);
        }
        Task.WaitAll(tasks);

        oTime.Stop(); //结束计时

        //输出运行时间。  
        Console.WriteLine("程序的运行时间：{0}秒", oTime.Elapsed.TotalSeconds);
        Console.WriteLine("程序的运行时间：{0}毫秒", oTime.Elapsed.TotalMilliseconds);

        var list = CountryInfo.GetCountryInfoList(states.ToArray());
        Console.WriteLine();
        //var xml = new BinaryFormatter();
        //var stream = new FileStream("TestData.txt", FileMode.Create);
        //xml.Serialize(stream, list);
        //var list = (List<CountryInfo>) xml.Deserialize(stream);
        //stream.Close();

        //Console.WriteLine();
    }
}