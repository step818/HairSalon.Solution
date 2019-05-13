using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientsControllerTests
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      ClientController controller = new ClientController();
      ActionResult indexView = controller.Index();
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }
    
  }
}
