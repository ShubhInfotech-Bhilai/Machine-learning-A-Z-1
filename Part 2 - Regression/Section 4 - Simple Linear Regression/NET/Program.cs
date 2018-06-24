using System.Collections.Generic;
using System;
using System.Linq;

namespace SimpleLinearRegressionModel {
    public class Program {
        static void Main(string[] args) {
            //PerformManualTest();
            PerformBulkTest();
        }

        public static void PerformManualTest() {
            SprintDataReader dataReader = new SprintDataReader();
            IEnumerable<SprintDataRow> sprintDataset = dataReader.LoadRecords("Dataset/Sprint_Data.csv");

            SprintVelocityPredictor sprintVelocityPredictor = new SprintVelocityPredictor(sprintDataset);
            while (true) {
                Console.WriteLine("Enter the number of hours: ");
                string userInput = Console.ReadLine();
                double numberOfHours;
                if (double.TryParse(userInput, out numberOfHours)) {
                    double predictedNumberOfStorypoints = sprintVelocityPredictor.PredictVelocity(numberOfHours);
                    Console.WriteLine($"I predict the number of completed story points will be: {predictedNumberOfStorypoints}");
                }
                else {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }

        public static void PerformBulkTest() {
            SprintDataReader dataReader = new SprintDataReader();
            IEnumerable<SprintDataRow> sprintDataset = dataReader.LoadRecords("Dataset/Sprint_Data.csv");
            IEnumerable<SprintDataRow> trainingSet = sprintDataset.Take(200);
            IEnumerable<SprintDataRow> testSet = sprintDataset.Skip(200);

            SprintVelocityPredictor velocityPredictor = new SprintVelocityPredictor(trainingSet);
            foreach (SprintDataRow sprintData in testSet) {
                double predictedNumberOfStorypoints = velocityPredictor.PredictVelocity(sprintData.NumberOfHours);
                Console.WriteLine($"For a sprint with {sprintData.NumberOfHours} I predict {predictedNumberOfStorypoints} and " +
                                  $"the actual number of processed storypoints was {sprintData.NumberOfProcessedStoryPoints}");
            }

            Console.Read();
        }
    }
}
