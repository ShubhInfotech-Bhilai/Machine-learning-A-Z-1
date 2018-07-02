using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LogisticRegressionExample {
    public class Program {
        public static void Main(string[] args) {
            //PerformManualTest();
            PerformBulkTest();
        }

        public static void PerformManualTest() {
            SubstanceDataReader dataReader = new SubstanceDataReader();
            IEnumerable<SubstanceData> sprintDataset = dataReader.LoadRecords("Dataset/Substances.csv");
            //IEnumerable<SubstanceData> trainingSet = sprintDataset.Take(85000);

            SubstanceDangerAnalyzer substanceDangerAnalyzer = new SubstanceDangerAnalyzer(sprintDataset);
            while (true) {
                Console.WriteLine("Enter substance details");
                int type = PromptIntegerValue("Type: ");
                int concentration = PromptIntegerValue("Concentration: ");
                SubstanceData substance = new SubstanceData() {
                    Type = type,
                    Concentration = concentration
                };

                bool isDangerous = substanceDangerAnalyzer.PredictIsDangerous(substance);
                string result = isDangerous ? "dangerous" : "not dangerous";
                Console.WriteLine($"I predict this substance is {result}");
            }
        }

        public static void PerformBulkTest() {
            SubstanceDataReader dataReader = new SubstanceDataReader();
            IEnumerable<SubstanceData> sprintDataset = dataReader.LoadRecords("Dataset/Substances.csv");
            IEnumerable<SubstanceData> trainingSet = sprintDataset.Take(9750);
            IEnumerable<SubstanceData> testSet = sprintDataset.Skip(9750);

            SubstanceDangerAnalyzer velocityPredictor = new SubstanceDangerAnalyzer(trainingSet);
            foreach (SubstanceData substanceData in testSet) {
                bool predictedIsDangerous = velocityPredictor.PredictIsDangerous(substanceData);
                Console.WriteLine($"For a substance of type {substanceData.Type} and a concentration of {substanceData.Concentration} I think IsDangerious = {predictedIsDangerous}");
            }

            Console.Read();
        }

        private static int PromptIntegerValue(string message) {
            Console.WriteLine(message);
            string userInput = Console.ReadLine();
            int outputValue;
            if (int.TryParse(userInput, out outputValue)) {
                return outputValue;
            }
            Console.WriteLine("Please enter a valid number");
            return PromptIntegerValue(message);
        }
    }
}