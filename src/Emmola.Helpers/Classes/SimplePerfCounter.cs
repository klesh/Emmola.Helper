using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Emmola.Helpers.Classes
{
  /// <summary>
  /// Monitor CPU/Memory/Harddriver state
  /// </summary>
  public class SimplePerfCounter
  {
    private PerformanceCounter _cpuCounter;
    private PerformanceCounter _memoryCounter;
    private DriveInfo _driverInfo;

    private string _osFullName;
    private long _totalMemoryInBytes;
    private long _totalHarddiskInBytes;
    private int _cpuCores;

    public SimplePerfCounter()
    {
      _cpuCounter = new PerformanceCounter();
      _cpuCounter.CategoryName = "Processor";
      _cpuCounter.CounterName = "% Processor Time";
      _cpuCounter.InstanceName = "_Total";
      _cpuCounter.NextValue(); // 初始化计数器


      _osFullName = SystemHelper.Os;
      _cpuCores = Environment.ProcessorCount;

      if (SystemHelper.IsUnix)
      {
        _totalMemoryInBytes = new PerformanceCounter("Mono Memory", "Total Physical Memory").RawValue;
        _totalHarddiskInBytes = long.Parse(FindDriverWithDf()[1]) * 1024;
      }
      else
      {
        _totalMemoryInBytes = SystemHelper.Run("wmic", "memorychip get capacity")
          .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
          .Skip(1).Select(t => long.Parse(t)).Sum();

        _driverInfo = new DriveInfo(AppDomain.CurrentDomain.BaseDirectory);
        _totalHarddiskInBytes = (long)_driverInfo.TotalSize;

        _memoryCounter = new PerformanceCounter();
        _memoryCounter.CategoryName = "Memory";
        _memoryCounter.CounterName = "% Committed Bytes In Use";
        _memoryCounter.NextValue(); // 初始化计数器
      }
    }

    /// <summary>
    /// Mono + *nix
    /// </summary>
    /// <returns></returns>
    protected string[] FindDriverWithDf()
    {
      var baseDir = AppDomain.CurrentDomain.BaseDirectory;
      return SystemHelper.Run("df")
              .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
              .Skip(1).Select(line => Regex.Split(line, @"\s+")).OrderByDescending(a => a.Last().Length)
              .FirstOrDefault(a => baseDir.StartsWith(a.Last()));
    }

    /// <summary>
    /// Os Full name
    /// </summary>
    public string OsFullname
    {
      get { return _osFullName; }
    }

    /// <summary>
    /// Cpu cores
    /// </summary>
    public int CpuCores
    {
      get { return _cpuCores; }
    }

    /// <summary>
    /// Total physical memory
    /// </summary>
    public long MemoryTotal
    {
      get { return _totalMemoryInBytes; }
    }

    /// <summary>
    /// Total harddisk of Current App 
    /// </summary>
    public long HarddiskTotal
    {
      get { return _totalHarddiskInBytes; }
    }

    /// <summary>
    /// return CPU usage
    /// </summary>
    /// <returns></returns>
    public float GetCpuUsage()
    {
      return _cpuCounter.NextValue();
    }

    /// <summary>
    /// return Memory usage
    /// </summary>
    /// <returns></returns>
    public float GetMemoryUsage()
    {
      if (SystemHelper.IsUnix)
      {
        var tmp = SystemHelper.Run("free", "-m")
          .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
          .Where(l => l.StartsWith("Mem"))
          .Select(l => Regex.Split(l, @"\s+"))
          .First();
        var used = long.Parse(tmp[2]) * 1024 * 1024;
        return (float)((decimal)used / (decimal)_totalMemoryInBytes);
      }
      
      return _memoryCounter.NextValue();
    }

    /// <summary>
    /// return Harddisk usage
    /// </summary>
    /// <returns></returns>
    public float GetHarddiskUsage()
    {
      if (SystemHelper.IsUnix)
        return float.Parse(FindDriverWithDf()[4].TrimEnd('%')) / 100f;

      return (float)((decimal)(_driverInfo.TotalSize - _driverInfo.AvailableFreeSpace) / (decimal)_driverInfo.TotalSize * 100);
    }
  }
}
