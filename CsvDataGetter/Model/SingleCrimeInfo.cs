using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataGetter.Model
{
    public class SingleCrimeInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public string CityOrCounty { get; set; }
        public int Killed { get; set; }
        public int Injured { get; set; }
        public List<GunInfo> GunInfos { get; set; }
        public List<CharacteristicsInfo> CharacteristicsInfos { get; set; }
        public List<ParticipantInfo> ParticipantInfos { get; set; }
    }
}
