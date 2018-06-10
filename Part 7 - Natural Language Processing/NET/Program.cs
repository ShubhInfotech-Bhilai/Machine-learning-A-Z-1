using System;
using System.Linq;
using System.Collections.Generic;
using Accord.MachineLearning;
using Accord.MachineLearning.Bayes;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;

namespace LanguageProcessing {
    class Program {
        static void Main(string[] args) {
            RestaurantReviewReader dataReader = new RestaurantReviewReader();
            IEnumerable<RestaurantReview> restaurantReviewsTrainingSet = dataReader.LoadRecords("Dataset/Restaurant_Reviews.tsv");

            string[] inputs = restaurantReviewsTrainingSet.Select(x => x.Review).ToArray();
            int[] outputs = restaurantReviewsTrainingSet.Select(x => x.IsPositive).ToArray();
            string[][] words = inputs.Tokenize();

            BagOfWords bagOfWords = new BagOfWords();
            bagOfWords.Learn(words);
            var bagOfWordsResult = bagOfWords.Transform(words);

            var teacher = new NaiveBayesLearning<NormalDistribution>();
            teacher.Options.InnerOption = new NormalOptions() {
                Regularization = 1e-5 // to avoid zero variances exceptions
            };
            var nb = teacher.Learn(bagOfWordsResult, outputs);

            string[] reviewTest = {"Loved this restaurant, it was truly amazing!"};
            string[][] reviewTestWords = reviewTest.Tokenize();
            double[][] reviewTestResult = bagOfWords.Transform(reviewTestWords);
            int[] output = nb.Transform(reviewTestResult);
        }
    }
}
