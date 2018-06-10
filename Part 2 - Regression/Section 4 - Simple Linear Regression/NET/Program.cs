using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.Statistics.Models.Regression.Linear;
using CsvHelper;
using System;

namespace SimpleLinearRegression {
    public class Program {
        static void Main(string[] args) {
            IEnumerable<SalaryDataRow> dataset = ReadDataset();

            // Set the independant variable
            double[] inputs = dataset.Select(x => x.YearsExperience).ToArray();

            // Set the dependant variables
            double[] outputs = dataset.Select(x => x.Salary).ToArray();

            // Train the model
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();
            Accord.Statistics.Models.Regression.Linear.SimpleLinearRegression regression = ols.Learn(inputs, outputs);

            // Ask the user for a prediction
            while (true) {
                Console.WriteLine("Enter the years of experience: ");
                string userInput = Console.ReadLine();
                double yearsOfExperience;
                if (double.TryParse(userInput, out yearsOfExperience)) {
                    double predictedSalary = regression.Transform(yearsOfExperience);
                    Console.WriteLine($"I predict the salary will be: {predictedSalary}");
                }
                else {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }

        public static IEnumerable<SalaryDataRow> ReadDataset() {
            using (TextReader textReader = File.OpenText("Dataset/Salary_Data.csv")) {
                var csv = new CsvReader(textReader);
                return csv.GetRecords<SalaryDataRow>().ToList();
            }
        }
    }

    public class SalaryDataRow {
        public double YearsExperience { get; set; }
        public double Salary { get; set; }
    }
}
