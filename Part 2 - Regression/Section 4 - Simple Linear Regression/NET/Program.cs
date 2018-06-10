using System.Collections.Generic;
using System;

namespace SimpleLinearRegressionModel {
    public class Program {
        static void Main(string[] args) {
            // Get the dataset
            SalaryDataReader dataReader = new SalaryDataReader();
            IEnumerable<SalaryDataRow> salaryDataset = dataReader.LoadRecords("Dataset/Salary_Data.csv");

            // Create the salary predictor
            SalaryPredictionModel salaryPredictionModel = new SalaryPredictionModel(salaryDataset);

            // Prompt the user for the years of experience
            while (true) {
                Console.WriteLine("Enter the years of experience: ");
                string userInput = Console.ReadLine();
                double yearsOfExperience;
                if (double.TryParse(userInput, out yearsOfExperience)) {
                    double predictedSalary = salaryPredictionModel.PredictSalary(yearsOfExperience);
                    Console.WriteLine($"I predict the salary will be: {predictedSalary}");
                }
                else {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }
    }
}
