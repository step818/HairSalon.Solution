using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistControllerTest
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      //Arrange
      StylistController controller = new StylistController();
      //Act
      ActionResult indexView = controller.Index();
      //Assert
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

    [TestMethod]
    public void Index_HasCorrectModelType_StylistList()
    {
      //Arrange
      StylistController controller = new StylistController();
      ViewResult indexView = controller.Index() as ViewResult;
      //Act
      var result = indexView.ViewData.Model;
      //Assert
      Assert.IsInstanceOfType(result, typeof(List<Stylist>));
    }

    [TestMethod]
    public void Show_ReturnsCorrectView_True()
    {
      //Arrange
      StylistController controller = new StylistController();
      //Act
      ActionResult indexView = controller.Show(1);
      //Assert
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

    [TestMethod]
    public void New_ReturnsCorrectView_True()
    {
      //Arrange
      StylistController controller = new StylistController();
      //Act
      ActionResult indexView = controller.New();
      //Assert
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

  }
}
