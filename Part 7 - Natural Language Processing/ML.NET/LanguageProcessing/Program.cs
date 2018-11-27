using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Transforms.Text;

namespace LanguageProcessing {
    class Program {
        static void Main(string[] args) {
            string pathToTrainingData = Path.Combine(Environment.CurrentDirectory, "Data", "Restaurant_Reviews.tsv");
            RestaurantReviewClassifier restaurantReviewClassifier = new RestaurantReviewClassifier(pathToTrainingData);

            SentimentPrediction prediction = restaurantReviewClassifier.Evaluate(new SentimentData() {
                SentimentText = "Crust is not good."
            });

            Console.WriteLine(prediction.Prediction);

            Console.ReadKey();
        }
    }
}
