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

    public override bool Equals(System.Object otherCategory)
    {
       if (!(otherCategory is Stylist))
       {
         return false;
       }
       else
       {
         Stylist newCategory = (Stylist) otherCategory;
         bool idEquality = this.GetId().Equals(newCategory.GetId());
         bool nameEquality = this.GetStylistName().Equals(newCategory.GetStylistName());
         return (idEquality && nameEquality);
       }
    }

    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
    }

    public string GetStylistName()
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
        cmd.CommandText = @"INSERT INTO stylists (stylist_name) VALUES (@stylist_name);";
        MySqlParameter stylist_name = new MySqlParameter();
        stylist_name.ParameterName = "@stylist_name";
        stylist_name.Value = this._name;
        cmd.Parameters.Add(stylist_name);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

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
        int StylistId = rdr.GetInt32(0);
        string StylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(StylistName);
        allStylists.Add(newStylist);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allStylists;
      }

      public static void ClearAll()
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

      public static Stylist Find(int searchId)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";
        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = searchId;
        cmd.Parameters.Add(thisId);
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string stylist_name = "";

        while (rdr.Read())
        {
          id = rdr.GetInt32(0);
          stylist_name = rdr.GetString(1);
        }
        Stylist foundStylist = new Stylist(stylist_name, id);
        conn.Close();
        if(conn != null)
        {
          conn.Dispose();
        }
        return foundStylist;
      }

  }
}
