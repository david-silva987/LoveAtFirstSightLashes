﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoveAtFirstSightLashes.Models
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Prenom { get; set; }

        public String DateBirth { get; set; }


        public int NbRDV { get; set; }



    }
}