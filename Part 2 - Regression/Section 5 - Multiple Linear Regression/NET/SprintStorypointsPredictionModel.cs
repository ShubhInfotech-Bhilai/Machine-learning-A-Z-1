using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Models.Regression.Linear;

namespace PredictSprintVelocity {
    public class SprintStorypointsPredictionModel {
        private MultipleLinearRegression _multipleLinearRegressionModel;

        public SprintStorypointsPredictionModel(IEnumerable<Sprint> trainingDataset) {
            this.Train(trainingDataset);
        }

        public double PredictNumberOfUserStories(int sprintNumber, int hoursMember1, int hoursMember2) {
            double[] input = { sprintNumber, hoursMember1, hoursMember2 };
            return this._multipleLinearRegressionModel.Transform(input);
        }

        private void Train(IEnumerable<Sprint> trainingDataset) {
            // Set the independant variables
            double[][] inputs = trainingDataset.Select(x => new double[] { x.SprintNumber, x.HoursMember1, x.HoursMember2 }).ToArray();

            // Set the dependant variables
            double[] outputs = trainingDataset.Select(x => x.ProcessedStoryPoints).ToArray();

            // Train the model
            var ols = new OrdinaryLeastSquares();
            this._multipleLinearRegressionModel = ols.Learn(inputs, outputs);
        }
    }
}