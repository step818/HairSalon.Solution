
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
      Client newClient = new Client(clientName, 1);
      //Assert
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetClientName_ReturnsClientName_String()
    {
      //Arrange
      string clientName = "Veronika";
      Client newClient = new Client(clientName, 1);
      //Act
      string result = newClient.GetClientName();
      //Assert
      Assert.AreEqual(clientName, result);
    }

    [TestMethod]
    public void SetClientName_RenameClientName_String()
    {
      //Arrange
      string clientName = "Veronika";
      Client newClient = new Client(clientName, 1);
      //Act
      string updatedName = "Angelina";
      newClient.SetClientName(updatedName);
      string result = newClient.GetClientName();
      //Assert
      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetId_ReturnsClientId_Int()
    {
      //Arrange
      int clientId = 1;
      Client newClient = new Client("", clientId);
      //Act
      int result = newClient.GetId();
      //Assert
      Assert.AreEqual(clientId, result);
    }

    // [TestMethod]
    // public void GetAll_ReturnsEmptyListFromDatabase_ClientList()
    // {
    //   //Arrange
    //   List<Client> newList = new List<Client>{};
    //   //Act
    //   List<Client> result = Client.GetAll();
    // }


  }
}
