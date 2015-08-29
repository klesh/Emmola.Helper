using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

namespace Emmola.Helpers
{
  /// <summary>
  /// came from https://blez.wordpress.com/2012/09/17/determine-os-with-netmono/
  /// </summary>
  public class SystemHelper
  {
    public static bool IsMono { get; private set; }
    public static bool IsWindows { get; private set; }
    public static bool IsUnix { get; private set; }
    public static bool IsMac { get; private set; }
    public static bool IsLinux { get; private set; }
    public static bool IsUnknown { get; private set; }
    public static bool Is32bit { get; private set; }
    public static bool Is64bit { get; private set; }
    public static bool Is64BitProcess { get { return (IntPtr.Size == 8); } }
    public static bool Is32BitProcess { get { return (IntPtr.Size == 4); } }
    public static string Os { get; private set; }

    [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

    private static bool Is64bitWindows
    {
      get
      {
        if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
        {
          using (Process p = Process.GetCurrentProcess())
          {
            bool retVal;
            if (!IsWow64Process(p.Handle, out retVal)) return false;
            return retVal;
          }
        }
        else return false;
      }
    }

    static SystemHelper()
    {
      IsWindows = Path.DirectorySeparatorChar == '\\';
      if (IsWindows)
      {
        Os = Environment.OSVersion.VersionString;

        Os = Os.Replace("Microsoft ", "");
        Os = Os.Replace("  ", " ");
        Os = Os.Replace(" )", ")");
        Os = Os.Trim();

        Os = Os.Replace("NT 6.2", "8 %bit 6.2");
        Os = Os.Replace("NT 6.1", "7 %bit 6.1");
        Os = Os.Replace("NT 6.0", "Vista %bit 6.0");
        Os = Os.Replace("NT 5.", "XP %bit 5.");
        Os = Os.Replace("%bit", (Is64bitWindows ? "64bit" : "32bit"));

        if (Is64bitWindows)
          Is64bit = true;
        else
          Is32bit = true;
      }
      else
      {
        string UnixName = Run("uname");
        if (UnixName.Contains("Darwin"))
        {
          IsUnix = true;
          IsMac = true;

          Os = "MacOS X " + Run("sw_vers", "-productVersion");
          Os = Os.Trim();

          string machine = Run("uname", "-m");
          if (machine.Contains("x86_64"))
            Is64bit = true;
          else
            Is32bit = true;

          Os += " " + (Is32bit ? "32bit" : "64bit");
        }
        else if (UnixName.Contains("Linux"))
        {
          IsUnix = true;
          IsLinux = true;

          Os = Run("lsb_release", "-d");
          Os = Os.Substring(Os.IndexOf(":") + 1);
          Os = Os.Trim();

          string machine = Run("uname", "-m");
          if (machine.Contains("x86_64"))
            Is64bit = true;
          else
            Is32bit = true;

          Os += " " + (Is32bit ? "32bit" : "64bit");
        }
        else if (UnixName != "")
        {
          IsUnix = true;
        }
        else
        {
          IsUnknown = true;
        }
      }

      IsMono = (Type.GetType("Mono.Runtime") != null);
    }
    
    /// <summary>
    /// Run a command and return output
    /// </summary>
    /// <param name="name">command</param>
    /// <returns>output</returns>
    public static string Run(string name)
    {
      return Run(name, null);
    }

    /// <summary>
    /// Run a command and return output with args
    /// </summary>
    /// <param name="name">command</param>
    /// <param name="args">args</param>
    /// <returns>output</returns>
    public static string Run(string name, string args)
    {
      try
      {
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        if (args != null && args != "") p.StartInfo.Arguments = " " + args;
        p.StartInfo.FileName = name;
        p.Start();
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();
        if (output == null) output = "";
        output = output.Trim();
        return output;
      }
      catch
      {
        return "";
      }
    }

    /// <summary>
    /// Return total size of a directory
    /// </summary>
    /// <param name="path">Full path</param>
    /// <returns>total size of its all sub files/folders in bytes</returns>
    public static long GetDirectorySize(string path)
    {
      try
      {
        return Directory.EnumerateFiles(path).Sum(x => new FileInfo(x).Length) +
          Directory.EnumerateDirectories(path).Sum(x => GetDirectorySize(x));
      }
      catch
      {
        return 0L;
      }
    }
  }
}
