using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using CsvDataGetter.Model;

namespace CsvDataGetter
{
    public class ReadAllData
    {
        public static List<SingleCrimeInfo> ReadData()
        {
            CrimeInfoDBContext dBContext = new CrimeInfoDBContext();
            DataTable dataSet = dBContext.OpenConnection();
            List<SingleCrimeInfo> crimeInfoCollection = new List<SingleCrimeInfo>();
            foreach (DataRow dataSetRow in dataSet.Rows)
            {
                SingleCrimeInfo simpleCrime = new SingleCrimeInfo();
                string[] singleRow = new string[dataSetRow.ItemArray.Length];

                for (int i = 0; i < singleRow.Length; i++)
                {
                    singleRow[i] = dataSetRow.ItemArray[i].ToString();
                }

                if (singleRow.Contains(""))
                {
                    continue;
                }
                simpleCrime.Id = int.Parse(singleRow[0]);
                simpleCrime.Date = DateTime.Parse(singleRow[1]).Date;
                simpleCrime.State = singleRow[2];
                simpleCrime.Killed = int.Parse(singleRow[3]);
                simpleCrime.Injured = int.Parse(singleRow[4]);
                string[] gunStatus = SplitString(singleRow[5]);
                string[] gunType = SplitString(singleRow[6]);

                if (gunStatus.Length != gunType.Length || gunStatus.Contains("") || gunStatus.Contains(""))
                {
                    continue;
                }

                for (int i = 0; i < gunStatus.Length; i++)
                {
                    simpleCrime.GunInfos.Add(new GunInfo(gunStatus[i], gunType[i]));
                }

                string[] incidents = SplitString(singleRow[7]);

                if (incidents.Contains(""))
                {
                    continue;
                }

                for (int i = 0; i < incidents.Length; i++)
                {
                    simpleCrime.CharacteristicsInfos.Add(new CharacteristicsInfo(incidents[i]));
                }

                simpleCrime.Latitude = float.Parse(singleRow[8]);
                simpleCrime.Longitude = float.Parse(singleRow[9]);
                string[] partAge = SplitString(singleRow[11]);
                string[] partAgeGroup = SplitString(singleRow[12]);
                string[] partGender = SplitString(singleRow[13]);
                string[] partStatus = SplitString(singleRow[14]);
                string[] partType = SplitString(singleRow[15]);

                if (partAge.Length != partAgeGroup.Length || partAgeGroup.Length != partGender.Length ||
                    partGender.Length != partStatus.Length || partStatus.Length != partType.Length ||
                    partAge.Contains("") || partAgeGroup.Contains("") || partGender.Contains("") ||
                    partStatus.Contains("") || partType.Contains(""))
                {
                    continue;
                }

                List<List<string>> partStatusList = new List<List<string>>();
                string[] splitSeparators = new string[] { "," };
                for (int i = 0; i < partStatus.Length; i++)
                {
                    partStatusList.Add(new List<string>());
                    if (partStatus[i].Contains(","))
                    {
                        string[] splittedPartStatusItem =
                            partStatus[i].Split(splitSeparators, StringSplitOptions.None);
                        foreach (string item in splittedPartStatusItem)
                        {

                            partStatusList[i].Add(item.Replace(" ", String.Empty));
                        }
                    }
                    else
                    {
                        partStatusList[i].Add(partStatus[i]);
                    }
                }

                bool IsAgeAbove = false;
                foreach (string item in partAge)
                {
                    if (int.Parse(item) > 80)
                    {
                        IsAgeAbove = true;
                    }
                }
                if (IsAgeAbove)
                {
                    continue;
                }

                for (int i = 0; i < partAge.Length; i++)
                {
                    simpleCrime.ParticipantsInfo.Add(new ParticipantInfo(int.Parse(partAge[i]), partAgeGroup[i], partGender[i], partStatusList[i], partType[i]));
                }

                crimeInfoCollection.Add(simpleCrime);
            }

            List<SingleCrimeInfo> sortedCollection = crimeInfoCollection.OrderBy(d => d.Date).ToList();
            SingleCrimeInfo.BeginDate = sortedCollection.First().Date;
            SingleCrimeInfo.EndDate = sortedCollection.Last().Date;
            return crimeInfoCollection;
        }

        public static string[] SplitString(string dataToSplit)
        {
            string[] splitSeparators = new string[] {"||"};
            string[] splittedData = dataToSplit.Split(splitSeparators, StringSplitOptions.None);
            int position = 3;
            if (!Char.IsLetter(splittedData[0].FirstOrDefault()))
            {
                for (int i = 0; i < splittedData.Length; i++)
                {
                    string toBeSearched = "::";
                    splittedData[i] = splittedData[i].Substring(splittedData[i].IndexOf(toBeSearched) + toBeSearched.Length);
                }
            }

            return splittedData;
        }
    }
}
