using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictSprintVelocity {
    class Program {
        static void Main(string[] args) {
            //PerformManualTest();
            PerformBulkTest();
        }

        public static void PerformBulkTest() {
            SprintDataReader dataReader = new SprintDataReader();
            IEnumerable<SprintDataRow> sprintDataset = dataReader.LoadRecords("Dataset/Sprint_Data.csv");
            IEnumerable<SprintDataRow> trainingSet = sprintDataset.Take(200);
            IEnumerable<SprintDataRow> testSet = sprintDataset.Skip(200);

            SprintVelocityPredictor velocityPredictor = new SprintVelocityPredictor(trainingSet);
            foreach (SprintDataRow sprintData in testSet) {
                double predictedNumberOfStorypoints = velocityPredictor.PredictVelocity(sprintData);
                Console.WriteLine($"For a sprint with the properties: Number: {sprintData.SprintNumber} - " +
                                  $"Programmer1 {sprintData.HoursProgrammer1} - " +
                                  $"Programmer2 {sprintData.HoursProgrammer2} - " +
                                  $"Programmer3 {sprintData.HoursProgrammer3} " +
                                  $"I predict {predictedNumberOfStorypoints} and " +
                                  $"the actual number of processed storypoints was {sprintData.NumberOfProcessedStoryPoints}");
            }

            Console.Read();
        }

        public static void PerformManualTest() {
            SprintDataReader dataReader = new SprintDataReader();
            IEnumerable<SprintDataRow> sprintDataset = dataReader.LoadRecords("Dataset/Sprint_Data.csv");
            SprintVelocityPredictor velocityPredictor = new SprintVelocityPredictor(sprintDataset);

            while (true) {
                Console.WriteLine("Enter new sprint details");
                int sprintNumber = PromptIntegerValue("Sprint number: ");
                int hoursMember1 = PromptIntegerValue("Hours programmer 1: ");
                int hoursMember2 = PromptIntegerValue("Hours programmer 2: ");
                int hoursMember3 = PromptIntegerValue("Hours programmer 3: ");
                SprintDataRow sprintDataRow = new SprintDataRow(sprintNumber, hoursMember1, hoursMember2, hoursMember3);
                double predictedNumberOfStoryPoints = velocityPredictor.PredictVelocity(sprintDataRow);
                Console.WriteLine($"I predict the team can process {predictedNumberOfStoryPoints} storypoints");
                Console.WriteLine(String.Empty);
            }
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
