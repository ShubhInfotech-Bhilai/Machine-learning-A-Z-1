using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using SimpleLinearRegressionModel;

namespace SimpleLinearRegressionModel {
    public class SalaryDataReader {
        public IEnumerable<SalaryDataRow> LoadRecords(string path) {
            using (TextReader textReader = File.OpenText(path)) {
                var csv = new CsvReader(textReader);
                return csv.GetRecords<SalaryDataRow>().ToList();
            }
        }
    }
}