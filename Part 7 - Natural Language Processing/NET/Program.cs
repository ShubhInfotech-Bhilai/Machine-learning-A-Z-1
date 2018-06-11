using System;
using System.Collections.Generic;
using Accord.Statistics.Filters;

namespace LanguageProcessing {
    class Program {
        static void Main(string[] args) {
            RestaurantReviewReader dataReader = new RestaurantReviewReader();
            IEnumerable<RestaurantReview> restaurantReviewsTrainingSet = dataReader.LoadRecords("Dataset/Restaurant_Reviews.tsv");

            RestaurantReviewClassifier restaurantReviewClassifier = new RestaurantReviewClassifier(restaurantReviewsTrainingSet);

            while (true) {
                Console.WriteLine("Type a review:");
                string review = Console.ReadLine();
                bool isPositive = restaurantReviewClassifier.PredictIsPositiveReview(review);
                Console.WriteLine("I think this is a " + (isPositive ? "positive" : "negative") + " review");
                Console.WriteLine(string.Empty);
            }
        }
    }
}
