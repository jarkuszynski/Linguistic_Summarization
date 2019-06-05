using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataGetter.Model
{
    public class CharacteristicsInfo
    {
        public CharacteristicsInfo(string info)
        {
            Info = info;
        }

        public string Info { get; set; }
    }
}
