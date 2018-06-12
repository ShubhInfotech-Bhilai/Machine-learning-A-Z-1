using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace LanguageProcessing {
    public class RestaurantReviewReader {
        public IEnumerable<RestaurantReview> LoadRecords(string path) {
            using (TextReader textReader = File.OpenText(path)) {
                Configuration config = new Configuration();
                config.Delimiter = "\t";
                config.BadDataFound = null;
                config.HeaderValidated = null;
                config.MissingFieldFound = null;

                var csv = new CsvReader(textReader, config);
                return csv.GetRecords<RestaurantReview>().ToList();
            }
        }
    }
}