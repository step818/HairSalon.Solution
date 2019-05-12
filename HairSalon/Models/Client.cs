using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{

  public class Client
  {
    private string _clientName;
    private int _id;
    private int _stylistId;

    public Client (string clientName, int stylistId, int id = 0)
    {
      _clientName = clientName;
      _id = id;
      _stylistId = stylistId;
    }

    public string GetClientName()
    {
      return _clientName;
    }

    public void SetClientName(string newName)
    {
      _clientName = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, stylistId, clientId);
        allClients.Add(newClient);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allClients;
    }

  


    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
