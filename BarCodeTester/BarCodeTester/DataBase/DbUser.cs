using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarCodeTester
{
    public class DbUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
