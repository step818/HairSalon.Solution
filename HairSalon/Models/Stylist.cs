using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{

    public class Stylist
    {
        private string _name;
        private int _id;

        public Stylist(string name, int id)
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

    }

}
