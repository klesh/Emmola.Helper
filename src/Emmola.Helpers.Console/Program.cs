using Emmola.Helpers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emmola.Helpers.Cmd
{
  class Program
  {
    static void Main(string[] args)
    {
      var simpleCounter = new SimplePerfCounter();
      Console.WriteLine("System: {0}", simpleCounter.OsFullname);
      Console.WriteLine("Cpu Cores: {0}", simpleCounter.CpuCores);
      Console.WriteLine("Memory Total: {0}", simpleCounter.MemoryTotal.ToCapacity());
      Console.WriteLine("Harddisk Total: {0}", simpleCounter.HarddiskTotal.ToCapacity());
      Task.Factory.StartNew(() =>
      {
        while (true)
        {
          Console.WriteLine("Cpu Usage: {0}", simpleCounter.GetCpuUsage());
          Console.WriteLine("Memory Usage: {0}", simpleCounter.GetMemoryUsage());
          Console.WriteLine("Harddisk Usage: {0}", simpleCounter.GetHarddiskUsage());
          Thread.Sleep(1000);
        }
      });
      Console.ReadKey();
    }
  }
}
