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

    [TestMethod]
    public void GetStylistName_ReturnsName_String()
    {
      //Arrange
      string name = "Keanu";
      Stylist newStylist = new Stylist(name, 1);
      //Act
      string result = newStylist.GetStylistName();
      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsId_Int()
    {
      //Arrange
      int id = 1;
      Stylist newStylist = new Stylist("", id);
      //Act
      int result = newStylist.GetId();
      //Assert
      Assert.AreEqual(id, result);
    }

    // [TestMethod]
    // public void Save_SavesToDatabase_StylistList()
    // {
    //   //Arrange
    //   Stylist testStylist = new Stylist("Dwight", 6);
    //   //Act
    //   testStylist.Save();
    //   List<Stylist> result = Stylist.GetAll();
    //   List<Stylist> testList = new List<Stylist>{testStylist};
    //   //Assert
    //   CollectionAssert.AreEqual(testList, result);
    // }
  }
}
