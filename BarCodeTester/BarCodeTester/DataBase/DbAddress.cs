using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarCodeTester
{
    public class DbAddress
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
    }
}
