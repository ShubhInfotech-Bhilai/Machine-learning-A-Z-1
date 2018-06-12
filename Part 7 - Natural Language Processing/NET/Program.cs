using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageProcessing {
    class Program {
        static void Main(string[] args) {
            //StartManualReviewMode();
            PerformBulkTest();
        }

        static void PerformBulkTest() {
            RestaurantReviewReader dataReader = new RestaurantReviewReader();
            IEnumerable<RestaurantReview> allRestaurantReviews = dataReader.LoadRecords("Dataset/Restaurant_Reviews.tsv");
            IEnumerable<RestaurantReview> trainingSet = allRestaurantReviews.Take(900);
            IEnumerable<RestaurantReview> testSet = allRestaurantReviews.Skip(trainingSet.Count());

            RestaurantReviewClassifier restaurantReviewClassifier = new RestaurantReviewClassifier(trainingSet);
            foreach (RestaurantReview restaurantReview in testSet) {
                restaurantReview.Result = restaurantReviewClassifier.PredictIsPositiveReview(restaurantReview.Review);
            }

            IEnumerable<RestaurantReview> incorrectPrediction = testSet.Where(x => x.Result != Convert.ToBoolean(x.IsPositive));
            Console.WriteLine($"Number of correct predictions: {testSet.Count() - incorrectPrediction.Count()}");
            Console.WriteLine($"Number of incorrect predictions: {incorrectPrediction.Count()}");
            Console.Read();
        }

        static void StartManualReviewMode() {
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
