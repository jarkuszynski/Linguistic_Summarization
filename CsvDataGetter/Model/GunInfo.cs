using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataGetter.Model
{
    public class GunInfo
    {
        public GunInfo(string status, string type)
        {
            Status = status;
            Type = type;
        }

        public string Status { get; set; }
        public string Type { get; set; }
    }
}
