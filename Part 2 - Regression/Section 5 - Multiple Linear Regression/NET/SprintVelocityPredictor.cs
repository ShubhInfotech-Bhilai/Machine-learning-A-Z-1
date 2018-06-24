using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Models.Regression.Linear;

namespace PredictSprintVelocity {
    public class SprintVelocityPredictor {
        private MultipleLinearRegression _multipleLinearRegressionModel;

        public SprintVelocityPredictor(IEnumerable<SprintDataRow> trainingDataset) {
            this.Train(trainingDataset);
        }

        public double PredictVelocity(SprintDataRow sprintDataRow) {
            double[] input = { sprintDataRow.SprintNumber, sprintDataRow.HoursProgrammer1, sprintDataRow.HoursProgrammer2, sprintDataRow.HoursProgrammer3 };
            return this._multipleLinearRegressionModel.Transform(input);
        }

        private void Train(IEnumerable<SprintDataRow> trainingDataset) {
            // Set the independant variables
            double[][] inputs = trainingDataset.Select(x => new double[] { x.SprintNumber, x.HoursProgrammer1, x.HoursProgrammer2, x.HoursProgrammer3 }).ToArray();

            // Set the dependant variables
            double[] outputs = trainingDataset.Select(x => x.NumberOfProcessedStoryPoints).ToArray();

            // Train the model
            var ols = new OrdinaryLeastSquares();
            this._multipleLinearRegressionModel = ols.Learn(inputs, outputs);
        }
    }
}