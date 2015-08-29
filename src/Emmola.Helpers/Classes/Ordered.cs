using System;

namespace Emmola.Helpers.Classes
{
  /// <summary>
  /// To hold object with Order index
  /// </summary>
  [Serializable]
  public class Ordered<T> : IComparable<Ordered<T>>
  {
    public Ordered(T value, int order)
    {
      Value = value;
      Order = order;
    }

    /// <summary>
    /// Object to hold
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Order index
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// compare
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Ordered<T> other)
    {
      var eq = this.Order.CompareTo(other.Order);
      if (eq == 0)
        return this.Value.GetHashCode().CompareTo(other.Value.GetHashCode());
      return eq;
    }
  }
}
