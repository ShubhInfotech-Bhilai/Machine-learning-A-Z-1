using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Models.Regression.Linear;

namespace SimpleLinearRegressionModel {
    public class SalaryPredictionModel {
        private SimpleLinearRegression _linearRegressionModel;

        public SalaryPredictionModel(IEnumerable<SalaryDataRow> trainingDataset) {
            this.Train(trainingDataset);
        }

        public double PredictSalary(double yearsOfExperience) {
            return this._linearRegressionModel.Transform(yearsOfExperience);
        }

        private void Train(IEnumerable<SalaryDataRow> trainingDataset) {
            // Our independant variable of the years of experience
            double[] inputs = trainingDataset.Select(x => x.YearsExperience).ToArray();

            // Our dependant variable is the salary
            double[] outputs = trainingDataset.Select(x => x.Salary).ToArray();

            // Train the model
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            this._linearRegressionModel = ols.Learn(inputs, outputs);
        }
    }
}