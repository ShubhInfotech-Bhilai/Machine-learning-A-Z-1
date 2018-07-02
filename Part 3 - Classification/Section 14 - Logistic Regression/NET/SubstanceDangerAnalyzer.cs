using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning.Bayes;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Models.Regression;

namespace LogisticRegressionExample {
    public class SubstanceDangerAnalyzer {
        private NaiveBayes<NormalDistribution> _naiveBayesModel;
        private LogisticRegression _logisticRegression;

        public SubstanceDangerAnalyzer(IEnumerable<SubstanceData> trainingDataset) {
            this.Train(trainingDataset);
        }

        private void Train(IEnumerable<SubstanceData> trainingDataset) {
            double[][] inputs = trainingDataset.Select(x => new double[] { x.Type, x.Concentration }).ToArray();

            int[] outputs = trainingDataset.Select(x => x.IsDangerous ? 1 : 0).ToArray();
            
            NaiveBayesLearning<NormalDistribution> naiveBayesTeacher = new NaiveBayesLearning<NormalDistribution>();
            naiveBayesTeacher.Options.InnerOption = new NormalOptions() {
                Regularization = 1e-5 // to avoid zero variances exceptions
            };
            this._naiveBayesModel = naiveBayesTeacher.Learn(inputs, outputs);
        }

        public bool PredictIsDangerous(SubstanceData data) {
            double[] inputs = new double[] {data.Type, data.Concentration};
            int result = this._naiveBayesModel.Transform(inputs);

            return result == 1;
        }
    }
}