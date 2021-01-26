using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoveAtFirstSightLashes.Models
{
    public class Meeting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String Name_Client { get; set; }

        public String DateRDV { get; set; }

        public String HourRDV { get; set; }

        public String TypePose { get; set; }

        public bool SheCame { get; set; }
    }
}
