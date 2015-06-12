using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emmola.Helpers.Interfaces
{
  /// <summary>
  /// Able to calcuate similarity to ther
  /// </summary>
  public interface ISimilarity<T>
  {
    float CalculateSimilarity(T other);
  }
}
