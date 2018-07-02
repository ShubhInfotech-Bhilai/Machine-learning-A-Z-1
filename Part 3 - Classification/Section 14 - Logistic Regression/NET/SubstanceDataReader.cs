using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace LogisticRegressionExample {
    public class SubstanceDataReader {
        public IEnumerable<SubstanceData> LoadRecords(string path) {
            using (TextReader textReader = File.OpenText(path)) {
                var csv = new CsvReader(textReader);
                return csv.GetRecords<SubstanceData>().ToList();
            }
        }
    }
}