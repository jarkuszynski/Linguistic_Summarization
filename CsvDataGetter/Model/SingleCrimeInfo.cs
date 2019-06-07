using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataGetter.Model
{
    public class SingleCrimeInfo
    {
        private const double _southEndCoord = 24.520833;
        private const double _westEndCoord = -124.771667;
        private const double _northEndCoord = 49.384358;
        private const double _eastEndCoord = -66.946944;
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public int Killed { get; set; }
        public int Injured { get; set; }
        public List<GunInfo> GunInfos { get; set; }
        public List<CharacteristicsInfo> CharacteristicsInfos { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public List<ParticipantInfo> ParticipantsInfo { get; set; }
        public static DateTime BeginDate { get; set; }
        public static DateTime EndDate { get; set; }
        public SingleCrimeInfo()
        {
            GunInfos = new List<GunInfo>();
            CharacteristicsInfos = new List<CharacteristicsInfo>();
            ParticipantsInfo = new List<ParticipantInfo>();
        }

        public double GetAttributeValue(string property)
        {
            switch (property)
            {
                case "Number of Killed":
                    return Killed;
                case "Number of Injured":
                    return Injured;
                case "Gun status":
                    return GetKnownGunStatusFraction();
                case "Average Age":
                    return GetAverageParticipantsAge();
                case "Age Group":
                    return GetAgeGroupFraction();
                case "Gender Fraction":
                    return GetGenderFraction();
                case "Participant Type":
                    return GetParticipantTypeFraction();
                case "Horizontal":
                    return GetHorizontalGeoSide();
                case "Vertical":
                    return GetVerticalGeoSide();
                case "Period":
                    return GetDatePeriodFraction();
                default:
                    throw new ArgumentNullException("property name must be provided");
            }
        }

        private double GetDatePeriodFraction()
        {
            return (Date - BeginDate).TotalDays;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns relative latitude from south side of USA - as bigger then norther</returns>
        private double GetVerticalGeoSide()
        {
            return 1.0 * Latitude - _southEndCoord;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns relative longitude from east side of USA - as bigger then wester</returns>
        private double GetHorizontalGeoSide()
        {
            return 1.0 * _eastEndCoord + Math.Abs(Longitude);
        }

        private double GetParticipantTypeFraction()
        {
            throw new NotImplementedException();
        }

        private double GetAgeGroupFraction()
        {
             
            throw new NotImplementedException();
        }

        private double GetAverageParticipantsAge()
        {
            double averageAge = 0.0;
            foreach (ParticipantInfo participantInfo in ParticipantsInfo)
            {
                averageAge += 1.0 * participantInfo.Age;
            }

            return averageAge / ParticipantsInfo.Count;
        }

        private double GetKnownGunTypeFraction()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Degrees/100 of Unknown/KnownOrigin</returns>
        private double GetKnownGunStatusFraction()
        {
            int numberOfKnownOrigin = 0;
            int numberOfUnknownOrigin = 0;
            foreach (GunInfo gun in GunInfos)
            {
                if (gun.Status == "Unknown")
                {
                    numberOfUnknownOrigin++;
                }
                else
                {
                    numberOfKnownOrigin++;
                }
            }
            double tangens = 1.0 * numberOfUnknownOrigin / numberOfKnownOrigin;
            double fraction = (Math.Atan(tangens) * (180 / Math.PI)) / 100;
            if (fraction > 0.9 || fraction < 0.0)
            {
                throw new ArgumentException("Error in calculating gender fraction");
            }
            //returns fraction between woman and man participants
            return fraction;
        }

        public double GetGenderFraction()
        {
            int numberOfMan = 0;
            int numberOfWoman = 0;
            foreach (ParticipantInfo participant in ParticipantsInfo)
            {
                if (participant.Gender == "Male")
                {
                    numberOfMan++;
                }
                else
                {
                    numberOfWoman++;
                }
            }

            double tangens = 1.0 * numberOfMan / numberOfWoman;
            double fraction = (Math.Atan(tangens) * (180 / Math.PI))/100;
            if (fraction >= 0.9 || fraction <= 0.0)
            {
                throw new ArgumentException("Error in calculating gender fraction");
            }
            //returns fraction between woman and man participants
            return fraction;
        }
    }
}
