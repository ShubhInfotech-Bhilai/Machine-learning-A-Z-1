// ******************************************************************************
//  © 2018 Ernst & Young Accountants LLP - www.ey.com
// 
//  Author          : EY - Climate Change and Sustainability Services
//  File:           : SubstanceDangerResult.cs
//  Project         : LogisticRegressionExample
// ******************************************************************************

namespace LogisticRegressionExample {

    public class SubstanceDangerResult {
        public bool ActualIsDangerous { get; set; }
        public bool PredictedIsDangerous { get; set; }
        public double PredictionProbability { get; set; }

        public SubstanceDangerResult(bool actualIsDangerous, bool predictedIsDangerous, double predictionProbability) {
            this.ActualIsDangerous = actualIsDangerous;
            this.PredictedIsDangerous = predictedIsDangerous;
            this.PredictionProbability = predictionProbability;
        }
    }
}
