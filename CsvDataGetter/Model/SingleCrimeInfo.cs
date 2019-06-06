﻿using System;
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

        private double GetVerticalGeoSide()
        {
            throw new NotImplementedException();
        }

        private double GetHorizontalGeoSide()
        {
            throw new NotImplementedException();
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
            double tangens = 1.0 * numberOfMatureParticipants / numberOfNonMatureParticipants;
            double fraction = (Math.Atan(tangens) * (180 / Math.PI)) / 100;
            if (fraction > 0.9 || fraction < 0.0)
            {
                throw new ArgumentException("Error in calculating participant type fraction");
            }
            //returns fraction between woman and man participants
            return fraction;
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
