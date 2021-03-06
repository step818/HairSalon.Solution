
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{

  [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=stephen_trewick_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      //Arrange
      string clientName = "Veronika";
      //Act
      Client newClient = new Client(clientName, 1, 1);
      //Assert
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetClientName_ReturnsClientName_String()
    {
      //Arrange
      string clientName = "Veronika";
      Client newClient = new Client(clientName, 1, 1);
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
      Client newClient = new Client(clientName, 1, 1);
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
      Client newClient = new Client("", clientId, 1);
      //Act
      int result = newClient.GetId();
      //Assert
      Assert.AreEqual(clientId, result);
    }

    [TestMethod]
    public void GetStylistId_ReturnsStylistId_Int()
    {
      //Arrange
      int stylistId = 1;
      Client newClient = new Client("", 1, stylistId);
      //Act
      int result = newClient.GetStylistId();
      //Assert
      Assert.AreEqual(stylistId, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ClientList()
    {
      //Arrange
      List<Client> newList = new List<Client>{};
      //Act
      List<Client> result = Client.GetAll();
      // Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("Keanu", 1);
      Client secondClient = new Client("Keanu", 1);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
     public void Save_SavesToDatabase_ClientList()
     {
       //Arrange
       Client testClient = new Client("Curly", 1);
       //Act
       testClient.Save();
       List<Client> result = Client.GetAll();
       List<Client> testList = new List<Client>{testClient};
       //Assert
       CollectionAssert.AreEqual(testList, result);
     }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Hairy", 1);
      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];
      int result = savedClient.GetId();
      int testId = testClient.GetId();
      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Moe", 1);
      testClient.Save();
      //Act
      Client foundClient = Client.Find(testClient.GetId());
      //Assert
      Assert.AreEqual(testClient, foundClient);
    }


  }
}
