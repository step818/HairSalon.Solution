
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalonTests
{

  [TestClass]
  public class ClientTest
  {

    // public void Dispose()
    // {
    //   Client.ClearAll();
    // }
    //
    // public ClientTest()
    // {
    //   DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=stephen_trewick_test;";
    // }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      //Arrange
      string clientName = "Veronika";
      //Act
      Client newClient = new Client(clientName);
      //Assert
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetClientName_ReturnsClientName_String()
    {
      //Arrange
      string clientName = "Veronika";
      Client newClient = new Client(clientName);
      //Act
      string result = newClient.GetClientName();
      //Assert
      Assert.AreEqual(clientName, result);
    }


  }
}
