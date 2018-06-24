using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace PredictSprintVelocity {
    public class SprintDataReader {
        public IEnumerable<SprintDataRow> LoadRecords(string path) {
            using (TextReader textReader = File.OpenText(path)) {
                var csv = new CsvReader(textReader);
                return csv.GetRecords<SprintDataRow>().ToList();
            }
        }
    }
}