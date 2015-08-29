using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emmola.Helpers
{
  public static class WebHelper
  {
    /// <summary>
    /// Combine URLs
    /// </summary>
    public static string CombineUrl(params string[] segments)
    {
      return string.Join("/", segments.Select(s => s.Trim('/')).Where(s => s.IsValid()));
    }

    /// <summary>
    /// Return a HttpClient with Host/Headers setup.
    /// </summary>
    /// <param name="host">Host</param>
    /// <param name="format">Format for both ends</param>
    /// <returns>HttpClient</returns>
    public static HttpClient GetHttpClient(string host, string format)
    {
      var client = new HttpClient();
      client.BaseAddress = new Uri(host);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(format));
      return client;
    }

    /// <summary>
    /// Return a Json HttpClient
    /// </summary>
    public static HttpClient GetJsonHttpClient(string host)
    {
      return GetHttpClient(host, "application/json");
    }

    /// <summary>
    /// Return a Xml HttpClient
    /// </summary>
    public static HttpClient GetXmlHttpClient(string host)
    {
      return GetHttpClient(host, "application/xml");
    }

    /// <summary>
    /// Return string base on CHARSET in META tag or WebResponse.ContentType
    /// </summary>
    public static async Task<string> ReadAsHtmlAsync(this HttpContent self)
    {
      Encoding encoding = null;
      var stream = await self.ReadAsStreamAsync();
      var contentType = self.Headers.ContentType;
      if (contentType != null)
      {
        if (contentType.MediaType.IcEquals("text/html"))
        {
          var headBytes = new byte[1000];
          stream.Read(headBytes, 0, 1000);
          var head = Encoding.ASCII.GetString(headBytes);
          var match = Regex.Match(head, @"; charset=([\w-]+)");
          if (match.Success)
            encoding = Encoding.GetEncoding(match.Groups[1].Value);
          stream.Position = 0;
        }
        if (encoding == null && contentType.CharSet.IsValid())
          encoding = Encoding.GetEncoding(contentType.CharSet);

      }
      encoding = encoding ?? Encoding.UTF8;
      var streamReader = new StreamReader(stream, encoding);
      var text = await streamReader.ReadToEndAsync();
      stream.Close();
      return text;
    }

    /// <summary>
    /// Get client ip address from header collection
    /// </summary>
    /// <param name="headers"></param>
    /// <returns>Client IP</returns>
    public static string GetClientIP(NameValueCollection headers)
    {
      if (headers["HTTP_X_FORWARDED_FOR"].IsValid())
        return Regex.Split(headers["HTTP_X_FORWARDED_FOR"], @"\s*,\s*").Last();
      else if (headers["HTTP_CLIENT_IP"].IsValid())
        return headers["HTTP_CLIENT_IP"];
      else
        return headers["REMOTE_ADDR"];
    }
  }
}
