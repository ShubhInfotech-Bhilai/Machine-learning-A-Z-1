using System;

namespace PredictSprintVelocity {
    class Program {
        static void Main(string[] args) {
            // Create the dataset
            Sprint[] trainingDataset = new Sprint[] {
                new Sprint() { Name = "Sprint 1", SprintNumber = 1, HoursMember1 = 60, HoursMember2 = 50, ProcessedStoryPoints = 80 },
                new Sprint() { Name = "Sprint 2", SprintNumber = 2, HoursMember1 = 50, HoursMember2 = 20, ProcessedStoryPoints = 56 },
                new Sprint() { Name = "Sprint 3", SprintNumber = 3, HoursMember1 = 55, HoursMember2 = 50, ProcessedStoryPoints = 94.5f },
                new Sprint() { Name = "Sprint 4", SprintNumber = 1, HoursMember1 = 60, HoursMember2 = 60, ProcessedStoryPoints = 84 },
                new Sprint() { Name = "Sprint 5", SprintNumber = 2, HoursMember1 = 50, HoursMember2 = 50, ProcessedStoryPoints = 80 }
            };

            SprintStorypointsPredictionModel predictionModel = new SprintStorypointsPredictionModel(trainingDataset);

            // Prompt the user for the sprint details
            while (true) {
                Console.WriteLine("Enter new sprint details");
                int sprintNumber = PromptIntegerValue("Sprint number: ");
                int hoursMember1 = PromptIntegerValue("Hours member 1: ");
                int hoursMember2 = PromptIntegerValue("Hours member 2: ");
                double predictedNumberOfStoryPoints = predictionModel.PredictNumberOfUserStories(sprintNumber, hoursMember1, hoursMember2);
                Console.WriteLine($"I predict the team can process {predictedNumberOfStoryPoints} storypoints");
                Console.WriteLine(String.Empty);
            }
        }

        public static int PromptIntegerValue(string message) {
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
