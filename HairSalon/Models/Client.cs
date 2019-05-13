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

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetClientName() == newClient.GetClientName();
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return (idEquality && nameEquality && stylistEquality);
      }
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (cName, stylistId) VALUES (@client_name, @stylist_id);";
      MySqlParameter cName = new MySqlParameter();
      cName.ParameterName = "@client_name";
      cName.Value = this._clientName;
      cmd.Parameters.Add(cName);
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._stylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      int stylistId = 0;

      while (rdr.Read())
      {
         clientId = rdr.GetInt32(0);
         clientName = rdr.GetString(1);
         stylistId = rdr.GetInt32(2);
      }
      Client foundClient= new Client(clientName, stylistId, clientId);
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
      return foundClient;
    }

  }
}
