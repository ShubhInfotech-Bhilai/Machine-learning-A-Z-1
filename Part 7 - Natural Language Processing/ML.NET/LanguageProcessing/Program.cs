using System;
using System.IO;

namespace LanguageProcessing {
    class Program {
        static void Main(string[] args) {
            string pathToTrainingData = Path.Combine(Environment.CurrentDirectory, "Data", "Restaurant_Reviews_training.tsv");
            string pathToTestData = Path.Combine(Environment.CurrentDirectory, "Data", "Restaurant_Reviews_test.tsv");
            RestaurantReviewClassifier restaurantReviewClassifier = new RestaurantReviewClassifier(pathToTrainingData);

            restaurantReviewClassifier.Evaluate(pathToTestData);

            SentimentPrediction prediction = restaurantReviewClassifier.Predict(new SentimentData() {
                SentimentText = "Crust is not good."
            });

            Console.WriteLine(prediction.Prediction);

            Console.ReadKey();
        }
    }
}
