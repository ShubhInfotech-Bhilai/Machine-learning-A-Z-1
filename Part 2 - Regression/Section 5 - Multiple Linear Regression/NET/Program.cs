using System;
using System.Linq;
using Accord.Statistics.Models.Regression.Linear;

namespace PredictSprintVelocity {
    class Program {
        static void Main(string[] args) {
            // Create the dataset
            Sprint[] dataset = new Sprint[] {
                new Sprint() { Name = "Sprint 1", SprintNumber = 1, HoursMember1 = 60, HoursMember2 = 50, ProcessedStoryPoints = 80 },
                new Sprint() { Name = "Sprint 2", SprintNumber = 2, HoursMember1 = 50, HoursMember2 = 20, ProcessedStoryPoints = 56 },
                new Sprint() { Name = "Sprint 3", SprintNumber = 3, HoursMember1 = 55, HoursMember2 = 50, ProcessedStoryPoints = 94.5f },
                new Sprint() { Name = "Sprint 4", SprintNumber = 1, HoursMember1 = 60, HoursMember2 = 60, ProcessedStoryPoints = 84 },
                new Sprint() { Name = "Sprint 5", SprintNumber = 2, HoursMember1 = 50, HoursMember2 = 50, ProcessedStoryPoints = 80 }
            };

            // Set the independant variables
            double[][] inputs = dataset.Select(x => new double[] { x.SprintNumber, x.HoursMember1, x.HoursMember2 }).ToArray();
            // Set the dependant variables
            double[] outputs = dataset.Select(x => x.ProcessedStoryPoints).ToArray();

            // Train the model
            var ols = new OrdinaryLeastSquares();
            MultipleLinearRegression regression = ols.Learn(inputs, outputs);

            // Predict the value for a second sprint, where both members work 50 hours
            double predicted = regression.Transform(new double[] { 2, 50, 50 });

            Console.WriteLine(predicted);
            Console.Read();
        }
    }

    public class Sprint {
        public string Name { get; set; }
        public int SprintNumber { get; set; }
        public int HoursMember1 { get; set; }
        public int HoursMember2 { get; set; }
        public double ProcessedStoryPoints { get; set; }
    }
}
