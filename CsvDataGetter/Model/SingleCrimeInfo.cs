using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataGetter.Model
{
    public class SingleCrimeInfo
    {
        private const double _southEndCoord = 19.1127;
        private const double _eastEndCoord = -67.2711;
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
                    throw new ArgumentNullException("property name must be provided or given is invalid");
            }
        }

        public static List<string> GetAvailableProperties()
        {
            List<string> availableProperties = new List<string>();
            availableProperties.Add("Number of Killed");
            availableProperties.Add("Number of Injured");
            availableProperties.Add("Gun status");
            availableProperties.Add("Average Age");
            availableProperties.Add("Age Group");
            availableProperties.Add("Gender Fraction");
            availableProperties.Add("Participant Type");
            availableProperties.Add("Horizontal");
            availableProperties.Add("Vertical");
            availableProperties.Add("Period");
            return availableProperties;
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
            return 1.0 * -_southEndCoord + Latitude;
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
            int numberOfVictims = 0;
            int numberOfSubjectSuspects = 0;
            foreach (ParticipantInfo participant in ParticipantsInfo)
            {
                if (participant.Type == "Victim")
                {
                    numberOfVictims++;
                }
                else
                {
                    numberOfSubjectSuspects++;
                }
            }
            if (numberOfVictims == 0)
            {
                return 0.0;
            }
            else if (numberOfSubjectSuspects == 0)
            {
                return 0.9;
            }
            double tangens = 1.0 * numberOfVictims / numberOfSubjectSuspects;
            double fraction = (Math.Atan(tangens) * (180 / Math.PI)) / 100;
            if (fraction > 0.9 || fraction < 0.0)
            {
                throw new ArgumentException("Error in calculating participant type fraction");
            }
            //returns fraction between woman and man participants	
            return fraction;
        }

        private double GetAgeGroupFraction()
        {
            int numberOfMatureParticipants = 0;
            int numberOfNonMatureParticipants = 0;
            foreach (ParticipantInfo participant in ParticipantsInfo)
            {
                if (participant.AgeGroup == "Adult 18+")
                {
                    numberOfMatureParticipants++;
                }
                else
                {
                    numberOfNonMatureParticipants++;
                }
            }
            if (numberOfMatureParticipants == 0)
            {
                return 0.0;
            }
            else if (numberOfNonMatureParticipants == 0)
            {
                return 0.9;
            }
            double tangens = 1.0 * numberOfMatureParticipants / numberOfNonMatureParticipants;
            double fraction = (Math.Atan(tangens) * (180 / Math.PI)) / 100;
            if (fraction > 0.9 || fraction < 0.0)
            {
                throw new ArgumentException("Error in calculating participant type fraction");
            }
            //returns fraction between woman and man participants	
            return fraction;
        }

        public double GetAverageParticipantsAge()
        {
            double averageAge = 0.0;
            foreach (ParticipantInfo participantInfo in ParticipantsInfo)
            {
                averageAge += 1.0 * participantInfo.Age;
            }
            if (ParticipantsInfo.Count == 0)
            {
                throw new ArgumentException("no participians in cell");
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
            if (numberOfKnownOrigin == 0)
            {
                return 0.9;
            }
            else if (numberOfUnknownOrigin == 0)
            {
                return 0.0;
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
            if (numberOfMan == 0)
            {
                return 0.0;
            }
            else if (numberOfWoman == 0)
            {
                return 0.9;
            }

            double tangens = 1.0 * numberOfMan / numberOfWoman;
            double fraction = (Math.Atan(tangens) * (180 / Math.PI)) / 100;
            if (fraction >= 0.9 || fraction <= 0.0)
            {
                throw new ArgumentException("Error in calculating gender fraction");
            }
            //returns fraction between woman and man participants
            return fraction;
        }
    }
}
