using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticRegressionExample {
    public class Program {
        public static void Main(string[] args) {
            PerformBulkTest();
        }

        public static void PerformBulkTest() {
            SubstanceDataReader dataReader = new SubstanceDataReader();
            IEnumerable<SubstanceData> sprintDataset = dataReader.LoadRecords("Dataset/Substances.csv");
            IEnumerable<SubstanceData> trainingSet = sprintDataset.Take(9500);
            IEnumerable<SubstanceData> testSet = sprintDataset.Skip(trainingSet.Count());

            SubstanceDangerAnalyzer velocityPredictor = new SubstanceDangerAnalyzer(trainingSet);
            int numberOfMistakes = 0;
            foreach (SubstanceData substanceData in testSet) {
                SubstanceDangerResult result = velocityPredictor.PredictIsDangerous(substanceData);
                Console.WriteLine($"For a substance of type {substanceData.Type} and a concentration of {substanceData.Concentration} I think IsDangerious = {result.PredictedIsDangerous}");
                Console.WriteLine($"The probability of this prediction is {result.PredictionProbability}%");

                if (substanceData.IsDangerous != result.PredictedIsDangerous) {
                    numberOfMistakes++;
                }
            }

            Console.WriteLine($"I predicted {testSet.Count()} substances.");
            Console.WriteLine($"I was right {testSet.Count() - numberOfMistakes} times.");
            Console.WriteLine($"I was wrong {numberOfMistakes} times.");
            Console.WriteLine($"I was right {100 - ((float)numberOfMistakes / testSet.Count() * 100)}% of the time.");

            Console.Read();
        }
    }
}