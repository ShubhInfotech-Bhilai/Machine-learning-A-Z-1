using System.Collections.Generic;
using System;

namespace SimpleLinearRegressionModel {
    public class Program {
        static void Main(string[] args) {
            // Get the dataset
            SprintDataReader dataReader = new SprintDataReader();
            IEnumerable<SprintDataRow> sprintDataset = dataReader.LoadRecords("Dataset/Sprint_Data.csv");

            // Create the velocity predictor
            SprintVelocityPredictor sprintVelocityPredictor = new SprintVelocityPredictor(sprintDataset);

            // Prompt the user for the number of available hours
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
    }
}
