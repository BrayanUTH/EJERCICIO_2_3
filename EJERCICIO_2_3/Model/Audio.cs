using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJERCICIO_2_3.Model
{
    public class Audio
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public DateTime dateCreation { get; set; }
        public string description { get; set; }
        public string pathAudio { get; set; }

    }
}
