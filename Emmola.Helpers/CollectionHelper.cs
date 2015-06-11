using Emmola.Helpers.Classes;
using Emmola.Helpers.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace Emmola.Helpers
{
  public static class CollectionHelper
  {
    /// <summary>
    /// Fill up array with specified value。 new int[4].Fill(1, 0, 3) => [1, 2, 3, ?];
    /// </summary>
    /// <param name="value">Value to fill</param>
    /// <param name="start">Fill starts from this(included)</param>
    /// <param name="end">Fill ends before this(excluded)</param>
    public static T[] Fill<T>(this T[] self, T value, int start = 0, int end = -1)
    {
      if (end == -1) end = self.Length;
      for (int i = start; i < end; i++)
        self[i] = value;
      return self;
    }

    /// <summary>
    /// Shortcut to String.Join
    /// </summary>
    /// <param name="separator"></param>
    public static string Implode<T>(this IEnumerable<T> self, string separator = "")
    {
      return string.Join(separator, self);
    }

    /// <summary>
    /// AddRang for HashSet<T>
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static HashSet<T> AddRange<T>(this HashSet<T> self, IEnumerable<T> values)
    {
      foreach (var value in values)
        self.Add(value);
      return self;
    }

    /// <summary>
    /// AddRang for HashSet<T>
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static ICollection<T> AddIfNotNull<T>(this ICollection<T> self, T value) where T : class
    {
      if (value != null)
        self.Add(value);
      return self;
    }

    /// <summary>
    /// Add value to pair.Value
    /// </summary>
    /// <param name="key">Pair.Key</param>
    /// <param name="value">Add to Pair.Value</param>
    /// <returns></returns>
    public static IDictionary<TK, TC> AddToValue<TK, TC, TV>(this IDictionary<TK, TC> self, TK key, TV value)
      where TC: ICollection<TV>, new()
    {
      if (!self.ContainsKey(key))
        self.Add(key, new TC());
      self[key].Add(value);
      return self;
    }

    public static IDictionary<TK, TC> AddToValueIfNotNull<TK, TC, TV>(this IDictionary<TK, TC> self, TK key, TV value)
      where TC : ICollection<TV>, new()
    {
      if (value != null) self.AddToValue(key, value);
      return self;
    }

    /// <summary>
    /// FirstOrDefault if not found, then create one add to collection then return it;
    /// </summary>
    /// <param name="find">FirstOrDefault arg</param>
    /// <param name="create">Create a new one</param>
    /// <returns></returns>
    public static T FindOrAdd<T>(this ICollection<T> self, Func<T, bool> find, Func<T> create)
    {
      var first = self.FirstOrDefault(find);
      if (first == null)
      {
        first = create();
        self.Add(first);
      }
      return first;
    }

    /// <summary>
    /// Return a urlencoded QueryString, support duplicated key
    /// </summary>
    public static string ToQueryString(this NameValueCollection self)
    {
      var allKeys = self.AllKeys;
      if (allKeys == null)
        return null;

      return allKeys
        .SelectMany(k =>
        {
          var t = self.GetValues(k);
          return t == null ? new string[0] : t.Select(v => WebUtility.UrlEncode(k) + "=" + WebUtility.UrlEncode(v));
        }).Where(p => p != null).Implode("&");
    }

    /// <summary>
    /// Get Nullable
    /// </summary>
    /// <param name="key"></param>
    public static Nullable<T> GetNullabe<T>(this NameValueCollection self, string key)
      where T : struct
    {
      if (!self.AllKeys.Contains(key))
        return null;

      try
      {
        if (typeof(T).IsEnum)
          return (T)Enum.Parse(typeof(T), self[key]);
        return (T)Convert.ChangeType(self[key], typeof(T));
      }
      catch { }
      return null;
    }

    /// <summary>
    /// Return a urlencoded QueryString
    /// </summary>
    public static string ToQueryString(this IDictionary<string, string> self)
    {
      if (self == null || !self.Any())
        return null;
      return self.Select(p => WebUtility.UrlEncode(p.Key) + "=" + WebUtility.UrlEncode(p.Value)).Implode("&");
    }

    /// <summary>
    /// Check if any duplicated element in collection
    /// </summary>
    public static bool AnySame<T>(this IEnumerable<T> self)
    {
      var e1 = self.GetEnumerator();
      if (e1.MoveNext() == false)
        return false; // only one element;
      var index1 = 1;
      while (e1.MoveNext())
      {
        var e2 = self.GetEnumerator();
        var index2 = 0;
        while (e2.MoveNext() && index2 < index1)
        {
          if (Object.Equals(e1.Current, e2.Current))
            return true;
          index2++;
        }
        index1++;
      }
      return false;
    }

    /// <summary>
    /// Calculate similarity of two Collection
    /// </summary>
    /// <returns>float type similarity between 0 and 1</returns>
    public static float SimilarityTo<T>(this IEnumerable<T> self, IEnumerable<T> other)
    {
      return (float)self.Intersect(other).Count() / (float)Math.Max(self.Count(), other.Count());
    }

    /// <summary>
    /// Find the most similar to target instance 
    /// </summary>
    /// <param name="target">Similar to</param>
    /// <param name="baseline">Only element's similarity above baseline are examinated</param>
    /// <returns>Most similar instance in list, return null if all belong baseline</returns>
    public static T FindMostSimilar<T>(this IEnumerable<T> self, T target, float baseline = 0.5f)
      where T : ISimilarity<T>
    {
      return self.Select(e => new
      {
        Element = e,
        Similarity = e.CalculateSimilarity(target)
      })
      .Where(t => t.Similarity > baseline)
      .OrderByDescending(t => t.Similarity)
      .Select(t => t.Element)
      .FirstOrDefault();
    }

    public static Difference<T> Diff<T>(this IEnumerable<T> self, IEnumerable<T> to)
      where T : ISimilarity<T>
    {
      var comparison = new Dictionary<T, T>();
      var creation = new List<T>();
      foreach (var t in to)
      {
        var s = self.FirstOrDefault(e => e.Equals(t));
        if (s != null)
        {
          if (s.CalculateSimilarity(t) < 1f)
            comparison.Add(s, t); // not exactly same
        }
        else
        {
          creation.Add(t);
        }
      }
      var deletion = self.Where(s => !to.Any(t => t.Equals(s))).ToList();

      foreach (var creating in creation.ToArray())
      {
        var deleting = deletion.FindMostSimilar(creating);
        if (deleting != null)
        {
          creation.Remove(creating);
          deletion.Remove(deleting);
          comparison.Add(deleting, creating);
        }
      }

      return new Difference<T>() { Comparison = comparison, Creation = creation, Deletion = deletion };
    }
  }
}
