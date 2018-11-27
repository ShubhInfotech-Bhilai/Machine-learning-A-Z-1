using System;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Transforms.Text;


namespace LanguageProcessing {
    public class RestaurantReviewClassifier {
        private readonly string _trainingDataPath;
        private readonly MLContext _mlContext;
        private readonly TextLoader _textLoader;
        private ITransformer _model;

        public RestaurantReviewClassifier(string trainingDataPath) {
            this._trainingDataPath = trainingDataPath;
            this._mlContext = new MLContext(seed: 0);
            this._textLoader = this._mlContext.Data.TextReader(new TextLoader.Arguments() {
                    Separator = "tab",
                    HasHeader = true,
                    Column = new[] {
                        new TextLoader.Column("Label", DataKind.Bool, 1),
                        new TextLoader.Column("SentimentText", DataKind.Text, 0)
                    }
                }
            );

            this._model = this.Train();
        }

        private ITransformer Train() {
            IDataView dataView = this._textLoader.Read(this._trainingDataPath);

            var pipeline = this._mlContext.Transforms.Text.FeaturizeText(nameof(SentimentData.SentimentText), "Features", x => {
                    x.KeepPunctuations = false;
                    x.KeepNumbers = false;
                    x.TextCase = TextNormalizingEstimator.CaseNormalizationMode.Lower;
                    x.KeepDiacritics = false;
                    x.TextLanguage = TextFeaturizingEstimator.Language.English;
                })
                .Append(this._mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeafs: 20));

            var model = pipeline.Fit(dataView);
            return model;
        }

        public void Evaluate(string testDataPath) {
            IDataView dataView = this._textLoader.Read(testDataPath);
            var predictions = this._model.Transform(dataView);
            var metrics = this._mlContext.BinaryClassification.Evaluate(predictions, "Label");

            Console.WriteLine();
            Console.WriteLine("Model quality metrics evaluation");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.Auc:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("=============== End of model evaluation ===============");
        }

        public SentimentPrediction Predict(SentimentData sentimentData) {
            var predictionFunction = this._model.MakePredictionFunction<SentimentData, SentimentPrediction>(this._mlContext);
            return predictionFunction.Predict(sentimentData);
        }
    }
}