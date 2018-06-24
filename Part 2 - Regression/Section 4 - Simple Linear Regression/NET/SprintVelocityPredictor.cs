using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Models.Regression.Linear;

namespace SimpleLinearRegressionModel {
    public class SprintVelocityPredictor {
        private SimpleLinearRegression _linearRegressionModel;

        public SprintVelocityPredictor(IEnumerable<SprintDataRow> trainingDataset) {
            this.Train(trainingDataset);
        }

        public double PredictVelocity(double yearsOfExperience) {
            return this._linearRegressionModel.Transform(yearsOfExperience);
        }

        private void Train(IEnumerable<SprintDataRow> trainingDataset) {
            // Our independant variable is the number of hours
            double[] inputs = trainingDataset.Select(x => Convert.ToDouble(x.NumberOfHours)).ToArray();

            // Our dependant variable is the number of processed story points
            double[] outputs = trainingDataset.Select(x => x.NumberOfProcessedStoryPoints).ToArray();

            // Train the model
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            this._linearRegressionModel = ols.Learn(inputs, outputs);
        }
    }
}