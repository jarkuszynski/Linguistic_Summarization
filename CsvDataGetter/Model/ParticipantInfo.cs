using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataGetter.Model
{
    public class ParticipantInfo
    {
        public ParticipantInfo(int age, string ageGroup, string gender, List<string> status, string type)
        {
            Age = age;
            AgeGroup = ageGroup;
            Gender = gender;
            Statuses = status;
            Type = type;
        }

        public int Age { get; set; }
        public string AgeGroup { get; set; }
        public string Gender { get; set; }
        public List<string> Statuses { get; set; }
        public string Type { get; set; }
    }
}
