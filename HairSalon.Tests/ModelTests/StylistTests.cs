using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=stephen_trewick_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
    Stylist newStylist = new Stylist("Keanu", 1);
    Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
    {
      //Arrange, Act
      Stylist firstCategory = new Stylist("Household chores");
      Stylist secondCategory = new Stylist("Household chores");
      //Assert
      Assert.AreEqual(firstCategory, secondCategory);
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

    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Buzz", 1);

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
    }

    [TestMethod]
     public void Find_ReturnsCorrectStylist_CorrectStylist()
     {
       Stylist testStylist = new Stylist("Bubba");
       testStylist.Save();
       int testId = testStylist.GetId();
       Stylist foundStylist = Stylist.Find(testId);
       Assert.AreEqual(testStylist, foundStylist);
     }

  }
}
