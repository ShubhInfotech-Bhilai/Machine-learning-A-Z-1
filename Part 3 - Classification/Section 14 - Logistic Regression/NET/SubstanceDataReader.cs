using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace LogisticRegressionExample {

    using CsvHelper.Configuration;

    public class SubstanceDataReader {
        public IEnumerable<SubstanceData> LoadRecords(string path) {
            using (TextReader textReader = File.OpenText(path)) {
                Configuration config = new Configuration();
                config.Delimiter = ";";

                var csv = new CsvReader(textReader, config);
                return csv.GetRecords<SubstanceData>().ToList();
            }
        }
    }
}