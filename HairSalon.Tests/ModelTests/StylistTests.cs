using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest
  {

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
    Stylist newStylist = new Stylist("Keanu", 1);
    Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

  }
}
