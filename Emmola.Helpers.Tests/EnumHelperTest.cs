using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emmola.Helpers.Tests
{
  [TestClass]
  public class EnumHelperTest
  {
    [TestMethod]
    public void ToReadableTest()
    {
      Assert.AreEqual("Processing", Progress.Ongoing.ToReadable());
      Assert.AreEqual("Failure", Progress.Failure.ToReadable());
      Assert.AreEqual("Current stataus: Finished", Progress.Success.GetDescription());
    }
  }

  public enum Progress
  {
    [Display(Name = "Processing", Description = "Current stataus: Processing")]
    Ongoing,
    [Display(Name = "Finished", Description = "Current stataus: Finished")]
    Success,
    Failure
  }
}
