using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{

  public class Specialty
  {

    private string _type;
    private int _id;

    public Specialty(string type, int id = 0)
    {
      _type = type;
      _id = id;
    }

    public string GetType()
    {
        return _type;
    }

    public int GetId()
    {
        return _id;
    }

    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM specialtys;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public override bool Equals(System.Object otherSpecialty)
    {
        if (!(otherSpecialty is Specialty))
        {
            return false;
        }
        else
        {
            Specialty newSpecialty = (Specialty) otherSpecialty;
            bool idEquality = this.GetId().Equals(newSpecialty.GetId());
            bool typeEquality = this.GetType().Equals(newSpecialty.GetType());
            return (idEquality && typeEquality);
        }
    }

    public static List<Specialty> GetAll()
    {
        List<Specialty> allSpecialtys = new List<Specialty> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialtys;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
        string SpecialtyType = rdr.GetString(1);
        int SpecialtyId = rdr.GetInt32(0);
        Specialty newSpecialty = new Specialty(SpecialtyType, SpecialtyId);
        allSpecialtys.Add(newSpecialty);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allSpecialtys;
    }

    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO specialtys (type, id) VALUES (@type, @id);";
        MySqlParameter type = new MySqlParameter();
        type.ParameterName = "@type";
        type.Value = this._type;
        cmd.Parameters.Add(type);
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

    public static Specialty Find(int id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialtys WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int SpecialtyId = 0;
        string SpecialtyType = "";
        while(rdr.Read())
        {
          SpecialtyId = rdr.GetInt32(0);
          SpecialtyType = rdr.GetString(1);
        }
        Specialty newSpecialty = new Specialty(SpecialtyType, SpecialtyId);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return newSpecialty;
    }

    public void AddStylist(Stylist newStylist)
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO appointments (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";
       MySqlParameter stylist_id = new MySqlParameter();
       stylist_id.ParameterName = "@StylistId";
       stylist_id.Value = newStylist.GetId();
       cmd.Parameters.Add(stylist_id);
       MySqlParameter specialty_id = new MySqlParameter();
       specialty_id.ParameterName = "@SpecialtyId";
       specialty_id.Value = _id;
       cmd.Parameters.Add(specialty_id);
       cmd.ExecuteNonQuery();
       conn.Close();
       if(conn != null)
       {
         conn.Dispose();
       }
     }

    public List<Stylist> GetStylists()
       {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT stylists.* FROM specialtys
       JOIN appointments ON (specialtys.id = appointments.specialty_id)
       JOIN stylists ON (appointments.specialty_id = stylists.id)
       WHERE specialtys.id = @SpecialtyId;";
       MySqlParameter specialtyIdParameter = new MySqlParameter();
       specialtyIdParameter.ParameterName = "@SpecialtyId";
       specialtyIdParameter.Value = _id;
       cmd.Parameters.Add(specialtyIdParameter);
       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
       List<Stylist> stylists = new List<Stylist>{};
       while(rdr.Read())
       {
         int stylistId = rdr.GetInt32(1);
         string stylistName = rdr.GetString(0);
         Stylist newStylist = new Stylist(stylistName, stylistId);
         stylists.Add(newStylist);
       }
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
       return stylists;
       }

  }
}
