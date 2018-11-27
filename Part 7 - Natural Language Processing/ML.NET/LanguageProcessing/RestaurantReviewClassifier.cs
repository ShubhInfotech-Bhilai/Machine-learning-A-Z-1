using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;


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
            var pipeline = this._mlContext.Transforms.Text.FeaturizeText(nameof(SentimentData.SentimentText), "Features")
                .Append(this._mlContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeafs: 20));

            return pipeline.Fit(dataView);
        }

        public SentimentPrediction Evaluate(SentimentData sentimentData) {
            var predictionFunction = this._model.MakePredictionFunction<SentimentData, SentimentPrediction>(this._mlContext);
            return predictionFunction.Predict(sentimentData);
        }
    }
}