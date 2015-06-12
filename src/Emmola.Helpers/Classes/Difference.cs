using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emmola.Helpers.Classes
{
  public class Difference<T>
  {
    public IEnumerable<T> Creation { get; set; }

    public IEnumerable<T> Deletion { get; set; }

    public IDictionary<T, T> Comparison { get; set; }
  }
}
