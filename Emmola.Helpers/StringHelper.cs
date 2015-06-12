using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Emmola.Helpers
{
  public static class StringHelper
  {
    static Random _random = new Random();
    static readonly char[] _asciiTable = new char[62];

    static StringHelper()
    { // 用于生成随机字符
      byte index = 0;
      Action<byte, byte> fillAsciiTable = (start, end) =>
      {
        for (var i = start; i < end; i++)
          _asciiTable[index++] = (char)i;
      };
      fillAsciiTable(48, 58); // 0-9 0, 10
      fillAsciiTable(65, 91); // A-Z 10, 27
      fillAsciiTable(97, 123); // a-z 27, 62
    }

    /// <summary>
    /// Return its MD5 digest text
    /// </summary>
    public static string DigestMD5(this string self)
    {
      using (var md5 = MD5.Create())
        return md5.ComputeHash(Encoding.UTF8.GetBytes(self)).ToHexString();
    }

    /// <summary>
    /// Return its SHA1 digest text
    /// </summary>
    public static string DigestSHA1(this string self)
    {
      using (var sha1 = SHA1.Create())
        return sha1.ComputeHash(Encoding.UTF8.GetBytes(self)).ToHexString();
    }

    /// <summary>
    /// Generate specified legnth of random characters
    /// </summary>
    /// <param name="length">Length</param>
    /// <param name="start">Start</param>
    /// <param name="end">End</param>
    public static string RandomText(int length = 6, byte start = 0, byte end = 62)
    {
      var chars = new char[length];
      for (var i = 0; i < length; i++)
        chars[i] = _asciiTable[_random.Next(start, end)];
      return new string(chars);
    }

    /// <summary>
    /// Generate specified legnth of Digits [0-9]
    /// </summary>
    /// <param name="length">Length</param>
    public static string RandomDigits(int length = 6)
    {
      return RandomText(length, 0, 10);
    }

    /// <summary>
    /// Generate specified legnth of Letters [A-Za-z]
    /// </summary>
    /// <param name="length">Length</param>
    public static string RandomLetters(int length = 8)
    {
      return RandomText(length, 10);
    }

    /// <summary>
    /// Generate specifed captcha code [A-Z0-9]
    /// </summary>
    /// <param name="length">Length</param>
    public static string RandomCaptcha(int length = 4)
    {
      return RandomText(length, 0, 27);
    }

    /// <summary>
    /// Convert Hashed byte[] to Hex String
    /// </summary>
    /// <param name="bytes">Hashed byte[]</param>
    public static string ToHexString(this byte[] bytes)
    {
      Func<int, char> toChar = (int i) => (char)(i < 10 ? i + '0' : i - 10 + 'A');
      var chars = new char[bytes.Length * 2];
      for (int i = 0, j = bytes.Length; i < j; i++)
      {
        chars[2 * i] = toChar(bytes[i] / 16);
        chars[2 * i + 1] = toChar(bytes[i] % 16);
      }
      return new string(chars);
    }


    /// <summary>
    /// Check if equals to String.Empty
    /// </summary>
    public static bool IsEmpty(this string self)
    {
      return self == string.Empty;
    }

    /// <summary>
    /// Check if is null
    /// </summary>
    public static bool IsNull(this string self)
    {
      return self == null;
    }

    /// <summary>
    /// Check if is valid string, not null not empty.
    /// </summary>
    public static bool IsValid(this string self)
    {
      return !string.IsNullOrWhiteSpace(self);
    }

    public static bool NotValid(this string self)
    {
      return string.IsNullOrWhiteSpace(self);
    }

    /// <summary>
    /// Return itself repeated in specified times: "a".Repeat(3) = "aaa";
    /// </summary>
    /// <param name="times">How many times to repeat</param>
    public static string Repeat(this string self, int times)
    {
      if (times == 1)
        return self;
      return new string[times].Fill(self).Implode();
    }

    /// <summary>
    /// Return Title Case Format "foo bar".ToTitleCase() = "Foo Bar"
    /// </summary>
    static TextInfo _textInfo = CultureInfo.InvariantCulture.TextInfo;
    public static string ToTitleCase(this string self)
    {
      return self.IsValid() ? _textInfo.ToTitleCase(self) : string.Empty;
    }


    /// <summary>
    /// Return a truncated string ends with "…" when its length exceeds specified length
    /// Otherwise return itself only.
    /// </summary>
    /// <param name="length">Max Length</param>
    public static string Ellipsis(this string self, int length)
    {
      return self.IsValid() && self.Length > length ? self.Substring(0, length - 1) + "…" : self;
    }

    /// <summary>
    /// Truncate string when it exceeds specified length when length > 0;
    /// Truncate trailing length of character when length < 0;
    /// </summary>
    /// <param name="length">Max Length</param>
    public static string Cut(this string self, int length)
    {
      if (length < 0)
        return self.Substring(0, self.Length + length);
      return self.IsValid() && self.Length > length ? self.Substring(0, length) : self;
    }

    /// <summary>
    /// Return defaultValue while itself is not valid
    /// </summary>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static string OrDefault(this string self, string defaultValue, params object[] args)
    {
      return self.IsValid() ? self : defaultValue.FormatMe(args);
    }

    /// <summary>
    /// Return a masked string, useful for Email/Phone masking
    /// </summary>
    /// <param name="mask">Mask character</param>
    public static string Mask(this string self, char mask = '*')
    {
      var atIndex = self.IndexOf('@');
      if (atIndex < 0)
        atIndex = self.Length - 3;
      var dotIndex = self.LastIndexOf('.');
      var beforeIndex = Math.Max(1, atIndex / 2);
      var afterBegin = atIndex + 2;
      var afterEnd = Math.Max(afterBegin, dotIndex);

      var chars = self.ToCharArray();
      chars.Fill(mask, beforeIndex, atIndex);
      chars.Fill(mask, afterBegin, afterEnd);
      return new string(chars);
    }

    /// <summary>
    /// Check if it's valid and consist by digits only
    /// </summary>
    public static bool IsDigitOnly(this string self)
    {
      return self.IsValid() && !self.Any(c => c < '0' || c > '9');
    }

    public static StringBuilder AppendWith(this StringBuilder self, char chr, int length, string text, params object[] args)
    {
      for (var i = 0; i < length; i++)
        self.Append(chr);
      return args.Any() ? self.AppendFormat(text, args) : self.Append(text);
    }

    /// <summary>
    /// Append a newline and insert specified length of tabs in the beginning, then append text and args as String.Format
    /// </summary>
    /// <param name="tabs">How many tabs to be inserted</param>
    /// <param name="text">Text to append</param>
    /// <param name="args">Supply args when text is FORMAT</param>
    public static StringBuilder AppendTabbedLine(this StringBuilder self, int length, string text, params object[] args)
    {
      return self.AppendLine().AppendWith('\t', length, text, args);
    }

    public static StringBuilder AppendTabbedLine(this StringBuilder self, string text, params object[] args)
    {
      return self.AppendTabbedLine(1, text, args);
    }

    /// <summary>
    /// Append specified length of space before text.
    /// </summary>
    /// <param name="length">Length of Spaces</param>
    /// <param name="text">Text to append</param>
    /// <param name="args">Format args</param>
    public static StringBuilder AppendSpaced(this StringBuilder self, int length, string text, params object[] args)
    {
      return self.AppendWith(' ', length, text, args);
    }

    /// <summary>
    /// Shortcut to .AppendSpaced(1, text, args);
    /// </summary>
    public static StringBuilder AppendSpaced(this StringBuilder self, string text, params object[] args)
    {
      return self.AppendSpaced(1, text, args);
    }

    /// <summary>
    /// Return all valid string as a new string[]
    /// </summary>
    public static string[] AllValid(this string[] self)
    {
      return self.Select(s => s.Trim()).Where(s => s.IsValid()).ToArray();
    }

    /// <summary>
    /// Shortcut of String.Equals InvariantCultureIgnoreCase
    /// </summary>
    public static bool IcEquals(this string self, string other)
    {
      return string.Equals(self, other, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Shortcut of String.Format
    /// </summary>
    public static string FormatMe(this string self, params object[] args)
    {
      return string.Format(self, args);
    }

    /// <summary>
    /// Return as Html Format
    /// </summary>
    public static string ToHtmlString(this string self)
    {
      return self.IsValid() ? WebUtility.HtmlEncode(self).Replace("  ", " &nbsp;").Replace(Environment.NewLine, "<br />") : null; 
    }
  }
}
