using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{

  public class Stylist
   {

       private string _name;
       private int _id;

       public Stylist(string name, int id = 0)
       {
           _name = name;
           _id = id;
       }

       public override bool Equals(System.Object otherStylist)
       {
           if (!(otherStylist is Stylist))
           {
               return false;
           }
           else
           {
               Stylist newStylist = (Stylist) otherStylist;
               bool idEquality = this.GetId().Equals(newStylist.GetId());
               bool nameEquality = this.GetName().Equals(newStylist.GetName());
               return (idEquality && nameEquality);
           }
       }

       public override int GetHashCode()
       {
           return this.GetId().GetHashCode();
       }

       public string GetName()
       {
           return _name;
       }

       public int GetId()
       {
           return _id;
       }

       public void Save()
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"INSERT INTO stylists (name, id) VALUES (@name, @id);";
           MySqlParameter name = new MySqlParameter();
           name.ParameterName = "@name";
           name.Value = this._name;
           cmd.Parameters.Add(name);
           MySqlParameter id = new MySqlParameter();
           id.ParameterName = "@id";
           id.Value = this._id;
           cmd.Parameters.Add(id);
           cmd.ExecuteNonQuery();
           _id = (int) cmd.LastInsertedId;
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
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

       public static List<Stylist> GetAll()
       {
           List<Stylist> allStylists = new List<Stylist> {};
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM stylists;";
           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           while(rdr.Read())
           {
           int StylistId = rdr.GetInt32(1);
           string StylistName = rdr.GetString(0);
           Stylist newStylist = new Stylist(StylistName, StylistId);
           allStylists.Add(newStylist);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return allStylists;
       }
       //
       public static Stylist Find(int id)
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
           MySqlParameter searchId = new MySqlParameter();
           searchId.ParameterName = "@searchId";
           searchId.Value = id;
           cmd.Parameters.Add(searchId);
           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           int StylistId = 0;
           string StylistName = "";
           while(rdr.Read())
           {
             StylistId = rdr.GetInt32(1);
             StylistName = rdr.GetString(0);
           }
           Stylist newStylist = new Stylist(StylistName, StylistId);
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return newStylist;
       }

       public static void DeleteAll()
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"DELETE FROM stylists;";
           cmd.ExecuteNonQuery();
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
       }

       public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand ("DELETE FROM stylists WHERE id = @StylistId;", conn);
        MySqlParameter stylistIdParameter = new MySqlParameter();
        stylistIdParameter.ParameterName = "@StylistId";
        stylistIdParameter.Value = this.GetId();
        cmd.Parameters.Add(stylistIdParameter);
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
      }

      public void Edit(string newName)
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _name = newName;
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
          }

   public void AddSpecialty(Specialty newSpecialty)
 {
   MySqlConnection conn = DB.Connection();
   conn.Open();
   var cmd = conn.CreateCommand() as MySqlCommand;
   cmd.CommandText = @"INSERT INTO salon (stylist_id, specialtys_id) VALUES (@StylistId, @SpecialtyId);";
   MySqlParameter stylist_id = new MySqlParameter();
   stylist_id.ParameterName = "@StylistId";
   stylist_id.Value = GetId();
   cmd.Parameters.Add(stylist_id);
   MySqlParameter specialtys_id = new MySqlParameter();
   specialtys_id.ParameterName = "@SpecialtyId";
   specialtys_id.Value = newSpecialty.GetId();
   cmd.Parameters.Add(specialtys_id);
   cmd.ExecuteNonQuery();
   conn.Close();
   if (conn != null)
   {
     conn.Dispose();
   }
 }

 public List<Specialty> GetSpecialtys()
  {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialtys.* FROM stylists
          JOIN stylists_specialtys ON (stylists.id = stylists_specialtys.stylist_id)
          JOIN specialtys ON (stylists_specialtys.item_id = specialtys.id)
          WHERE stylists.id = @StylistId;";
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = _id;
      cmd.Parameters.Add(stylistIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Specialty> specialtys = new List<Specialty>{};
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyType = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyType, specialtyId);
        specialtys.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return specialtys;
  }


   }

}
