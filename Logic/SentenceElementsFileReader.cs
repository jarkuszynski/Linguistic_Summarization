using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.LinguisticSummarization;
using Logic.MembershipFunctions;
using Microsoft.VisualBasic.FileIO;

namespace CsvDataGetter
{
    public class SentenceTuple
    {
        public List<Qualifier> Qualifiers = new List<Qualifier>();
        public List<Quantifier> Quantifiers = new List<Quantifier>();
        public List<Summarizator> Summarizators = new List<Summarizator>();
        OperationType operation = OperationType.And;

        public SentenceTuple()
        {
        }

        public SentenceTuple(List<Qualifier> qualifiers, List<Quantifier> quantifiers, List<Summarizator> summarizators, OperationType op)
        {
            Qualifiers = qualifiers;
            Quantifiers = quantifiers;
            Summarizators = summarizators;
            operation = op;
        }
    }
    enum ElementType
    {
        Summarizator,
        Qualifier,
        Quantifier
    }

    public static class SentenceElementsFileReader
    {
        public static SentenceTuple getSentenceElementsFromFile()
        {
            SentenceTuple sentenceTuple = new SentenceTuple();
            List<Summarizator> summarizators = new List<Summarizator>();
            List<Qualifier> qualifiers = new List<Qualifier>();
            List<Quantifier> quantifiers = new List<Quantifier>();
            string workingDirectory = Environment.CurrentDirectory;
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\SummarizatorOrQualifier.csv";
            using (TextFieldParser csvParser = new TextFieldParser(filepath))
            {
                csvParser.CommentTokens = new[] { "#" };
                csvParser.SetDelimiters(";");
                csvParser.HasFieldsEnclosedInQuotes = false;
                csvParser.ReadLine();
                var parsingMode = ElementType.Summarizator;
                var currentLogicalOperator = OperationType.And;
                Quantifier.QuantifierType quantifierType = Quantifier.QuantifierType.Relative;

                while (!csvParser.EndOfData)
                {
                    IMembershipFunction membershipFunction;
                    string[] singleRow = csvParser.ReadFields();
                    if ((singleRow ?? throw new InvalidOperationException()).Contains(""))
                    {
                        continue;
                    }
                    if (singleRow[0].Contains("summarizators"))
                    {
                        parsingMode = ElementType.Summarizator;
                        currentLogicalOperator = (singleRow[1] == OperationType.And.ToString().ToLowerInvariant()) ? OperationType.And : OperationType.Or;
                        continue;
                    }
                    else if (singleRow[0] == "qualifiers")
                    {
                        parsingMode = ElementType.Qualifier;
                        continue;
                    }
                    else if (singleRow[0] == "quantifiers")
                    {
                        parsingMode = ElementType.Quantifier;
                        Console.WriteLine(singleRow[1] + Quantifier.QuantifierType.Relative.ToString().ToLowerInvariant());
                        quantifierType = singleRow[1] == Quantifier.QuantifierType.Relative.ToString().ToLowerInvariant() ? Quantifier.QuantifierType.Relative : Quantifier.QuantifierType.Absolute;
                        continue;
                    }
                    string description = singleRow[0];
                    string attributeName = singleRow[1];
                    string functionType = singleRow[2];
                    string[] functionParametres = singleRow[3].Split(',');
                    int minValue = int.Parse(singleRow[4]);
                    int maxValue = int.Parse(singleRow[5]);
                    if (functionType == "trian")
                    {
                        membershipFunction = new TriangleMembershipFunction(double.Parse(functionParametres[0], CultureInfo.InvariantCulture), double.Parse(functionParametres[1],CultureInfo.InvariantCulture), double.Parse(functionParametres[2], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        membershipFunction = new TrapezoidalMembershipFunction(double.Parse(functionParametres[0]), double.Parse(functionParametres[1]), double.Parse(functionParametres[2]), double.Parse(functionParametres[3]));
                    }
                    switch (parsingMode)
                    {
                        case ElementType.Summarizator:
                            summarizators.Add(new Summarizator(
                                description,
                                attributeName,
                                membershipFunction,
                                minValue,
                                maxValue));
                            break;
                        case ElementType.Qualifier:
                            qualifiers.Add(new Qualifier(
                                description,
                                attributeName,
                                membershipFunction,
                                minValue,
                                maxValue));
                            break;
                        case ElementType.Quantifier:
                            quantifiers.Add(new Quantifier(
                                description,
                                membershipFunction,
                                quantifierType,
                                minValue,
                                maxValue));
                            break;
                            
                    }

                }
            return new SentenceTuple(qualifiers, quantifiers, summarizators, currentLogicalOperator);
            }
        }
        public static void SaveFuzzySetsToFile(
            List<Quantifier> quants,
            List<Qualifier> quals,
            List<Summarizator> summs,
            OperationType op,
            string dest)
        {
            using (var fs = new StreamWriter(dest))
            {
                fs.WriteLine("QUANTIFIERS");
                foreach (var quan in quants)
                {
                    fs.WriteLine($"{quan.Description}:{(quan.IsAbsolute ? "abs" : "rel")}:{GetMembershipString(quan.MembershipFunction)}");
                }
                fs.WriteLine();


                fs.WriteLine("QUALIFIERS");
                foreach (var qual in quals)
                {
                    fs.WriteLine($"{qual.Description}:{qual.AttributeName}:{GetMembershipString(qual.MembershipFunction)}");
                }
                fs.WriteLine();

                fs.WriteLine($"SUMMARIZERS {op.ToString()}");
                foreach (var summ in summs)
                {
                    fs.WriteLine($"{summ.Description}:{summ.AttributeName}:{GetMembershipString(summ.MembershipFunction)}");
                }
                fs.WriteLine();
            }
        }
        public static string GetMembershipString(IMembershipFunction memFun)
        {
            var memFunName = memFun.Name;
            memFunName = memFunName.Replace("triangular", "tri");
            memFunName = memFunName.Replace("trapezoidal", "trap");
            return memFunName;
        }

    }
}
