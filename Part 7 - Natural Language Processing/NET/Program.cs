using System;
using System.Collections.Generic;

namespace LanguageProcessing {
    class Program {
        static void Main(string[] args) {
            RestaurantReviewReader dataReader = new RestaurantReviewReader();
            IEnumerable<RestaurantReview> salaryDataset = dataReader.LoadRecords("Dataset/Restaurant_Reviews.tsv");
        }
    }
}
