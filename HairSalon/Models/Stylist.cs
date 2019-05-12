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

    public string GetStylistName()
    {
        return _name;
    }

    public int GetId()
    {
        return _id;
    }


    //
    // public void Save()
    // {
    //    MySqlConnection conn = DB.Connection();
    //    conn.Open();
    //    var cmd = conn.CreateCommand() as MySqlCommand;
    //    cmd.CommandText = @"INSERT INTO stylists (id, stylist_name) VALUES (@id, @name);";
    //    MySqlParameter name = new MySqlParameter();
    //    name.ParameterName = "@name";
    //    name.Value = this._name;
    //    cmd.Parameters.Add(name);
    //    MySqlParameter id = new MySqlParameter();
    //    id.ParameterName = "@id";
    //    id.Value = this._id;
    //    cmd.Parameters.Add(id);
    //    cmd.ExecuteNonQuery();
    //    _id = (int) cmd.LastInsertedId;
    //    conn.Close();
    //    if (conn != null)
    //    {
    //        conn.Dispose();
    //    }
    // }

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
  }
}
