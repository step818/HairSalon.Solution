using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{

  public class Client
 {
   private string _name;
   private int _id;
   private int _stylistId;


   public Client (string name, int stylistId, int id = 0)
   {
     _name = name;
     _stylistId = stylistId;
     _id = id;
   }

   public string GetName()
   {
     return _name;
   }

   public void SetName(string newName)
   {
     _name = newName;
   }

   public int GetId()
   {
     return _id;
   }

   public int GetStylistId()
   {
     return _stylistId;
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
       bool nameEquality = this.GetName() == newClient.GetName();
       bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
       return (idEquality && nameEquality && stylistEquality);
     }
   }

   public static List<Client> GetAll()
   {
     List<Client> allClients = new List<Client> {};
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients;";
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       string clientName = rdr.GetString(0);
       int clientId = rdr.GetInt32(1);
       int clientStylistId = rdr.GetInt32(2);
       Client newClient = new Client(clientName, clientStylistId, clientId);
       allClients.Add(newClient);
     }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return allClients;
   }

   public void Save()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylistId);";
     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@name";
     name.Value = this._name;
     cmd.Parameters.Add(name);

     MySqlParameter stylistId = new MySqlParameter();
     stylistId.ParameterName = "@stylistId";
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
     cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     int clientId = 0;
     string clientName = "";
     int clientStylistId = 0;
     while(rdr.Read())
     {
       clientName = rdr.GetString(0);
       clientId = rdr.GetInt32(1);
       clientStylistId = rdr.GetInt32(2);
     }
     Client foundClient = new Client(clientName, clientStylistId, clientId);
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return foundClient;
   }

   public void DeleteClient()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     MySqlCommand cmd = new MySqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);
     MySqlParameter clientIdParameter = new MySqlParameter();
     clientIdParameter.ParameterName = "@ClientId";
     clientIdParameter.Value = this.GetId();
     cmd.Parameters.Add(clientIdParameter);
     cmd.ExecuteNonQuery();
     if (conn != null)
     {
       conn.Close();
     }
   }

  public List<Client> GetClients()
  {
    List<Client> allStylistClients = new List<Client> {};
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";
    MySqlParameter stylistId = new MySqlParameter();
    stylistId.ParameterName = "@stylist_id";
    stylistId.Value = this._id;
    cmd.Parameters.Add(stylistId);
    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
        int clientId = rdr.GetInt32(1);
        string clientName = rdr.GetString(0);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        allStylistClients.Add(newClient);
    }
    conn.Close();
    if (conn != null)
    {
        conn.Dispose();
    }
    return allStylistClients;
  }




 }
}
