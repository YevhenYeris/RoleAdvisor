using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Algorithm;

public class TOPSIS
{
    public List<Employee> SelectBestEmployees(List<Employee> employees, List<Position> positions)
    {
        // Step 1: Identify Criteria
        // Combine all unique skills required for positions
        List<Skill> criteria = positions.SelectMany(p => p.SkillsRequired).Distinct().ToList();

        // Step 2: Normalize the Data
        NormalizeData(employees, criteria);

        // Step 3: Determine Weights
        // For simplicity, let's assign equal weights to all criteria
        List<double> weights = Enumerable.Repeat(1.0 / criteria.Count, criteria.Count).ToList();

        // Step 8: Select the Best Employees for Each Position
        foreach (Position position in positions)
        {
            // Filter employees who are advised for this position
            List<Employee> advisedEmployees = employees
                .Where(e => e.PositionsPickedFor == null || !e.PositionsPickedFor.Contains(position))
                .ToList();

            if (advisedEmployees.Any())
            {
                // Create a decision matrix for the current position
                double[,] decisionMatrix = CreateDecisionMatrix(advisedEmployees, position, criteria);

                // Identify Ideal and Anti-Ideal Solutions for the current position
                double[] idealSolution = CalculateIdealSolution(decisionMatrix, weights, true);
                double[] antiIdealSolution = CalculateIdealSolution(decisionMatrix, weights, false);

                // Calculate Similarity Scores for the current position
                List<double> similarityScores = CalculateSimilarityScores(decisionMatrix, idealSolution, antiIdealSolution, weights);

                // Rank the employees for the current position
                List<Employee> rankedEmployees = RankEmployees(advisedEmployees, similarityScores);

                // Select the top-ranked employee for the current position
                Employee selectedEmployee = rankedEmployees.First();

                // Assign the selected employee to the position
                position.PickedEmployeeId = selectedEmployee.Id;
                selectedEmployee.PositionsPickedFor.Add(position);
            }
        }

        // Return the selected employees
        return employees;
    }

    private double[,] CreateDecisionMatrix(List<Employee> employees, Position position, List<Skill> criteria)
    {
        // Create a decision matrix based on employee skills and position requirements
        double[,] decisionMatrix = new double[employees.Count, criteria.Count];

        for (int i = 0; i < employees.Count; i++)
        {
            for (int j = 0; j < criteria.Count; j++)
            {
                // For simplicity, let's assume each skill has a score (normalized)
                double skillScore = employees[i].Skills.Any(s => s.Id == criteria[j].Id) ? 1.0 : 0.0;
                decisionMatrix[i, j] = skillScore;
            }
        }

        return decisionMatrix;
    }

    private void NormalizeData(List<Employee> employees, List<Skill> criteria)
    {
        // Perform normalization based on your specific requirements
        // This can include Min-Max normalization or Z-score normalization
        // For simplicity, we'll assume skills are already normalized
        //foreach (Skill criterion in criteria)
        //{
        //    // Extract scores for the current criterion for all employees
        //    List<double> scores = employees.Select(employee =>
        //    {
        //        // For simplicity, assume each skill has a score (you may need to customize this based on your actual data)
        //        double skillScore = employee.Skills.Any(s => s.Id == criterion.Id) ? 1.0 : 0.0;
        //        return skillScore;
        //    }).ToList();

        //    // Find the minimum and maximum scores for the current criterion
        //    double minScore = scores.Min();
        //    double maxScore = scores.Max();

        //    // Normalize the scores for the current criterion for all employees
        //    for (int i = 0; i < employees.Count; i++)
        //    {
        //        double normalizedScore = (scores[i] - minScore) / (maxScore - minScore);
        //        // Update the normalized score for the current criterion for the current employee
        //        // (You may need to customize this based on your actual data structure)
        //        employees[i].Skills.First(s => s.Id == criterion.Id).Score = normalizedScore;
        //    }
        //}
    }

    private double[,] CreateDecisionMatrix(List<Employee> employees, List<Position> positions, List<Skill> criteria)
    {
        // Create a decision matrix based on employee skills and position requirements
        double[,] decisionMatrix = new double[employees.Count, criteria.Count];

        for (int i = 0; i < employees.Count; i++)
        {
            for (int j = 0; j < criteria.Count; j++)
            {
                // For simplicity, let's assume each skill has a score (normalized)
                double skillScore = employees[i].Skills.Any(s => s.Id == criteria[j].Id) ? 1.0 : 0.0;
                decisionMatrix[i, j] = skillScore;
            }
        }

        return decisionMatrix;
    }

    private double[] CalculateIdealSolution(double[,] decisionMatrix, List<double> weights, bool isIdeal)
    {
        int numCriteria = decisionMatrix.GetLength(1);
        double[] idealSolution = new double[numCriteria];

        for (int j = 0; j < numCriteria; j++)
        {
            double aggregate = 0;
            for (int i = 0; i < decisionMatrix.GetLength(0); i++)
            {
                double weightedValue = decisionMatrix[i, j] * weights[j];
                aggregate += isIdeal ? Math.Pow(weightedValue, 2) : Math.Pow(1 - weightedValue, 2);
            }

            idealSolution[j] = isIdeal ? Math.Sqrt(aggregate) : 1 / Math.Sqrt(aggregate);
        }

        return idealSolution;
    }

    private List<double> CalculateSimilarityScores(double[,] decisionMatrix, double[] idealSolution, double[] antiIdealSolution, List<double> weights)
    {
        List<double> similarityScores = new List<double>();

        for (int i = 0; i < decisionMatrix.GetLength(0); i++)
        {
            double idealDistance = CalculateEuclideanDistance(decisionMatrix, i, idealSolution, weights);
            double antiIdealDistance = CalculateEuclideanDistance(decisionMatrix, i, antiIdealSolution, weights);

            // The TOPSIS score is the ratio of the anti-ideal distance to the sum of ideal and anti-ideal distances
            double topsisScore = antiIdealDistance / (idealDistance + antiIdealDistance);

            similarityScores.Add(topsisScore);
        }

        return similarityScores;
    }

    private double CalculateEuclideanDistance(double[,] decisionMatrix, int row, double[] targetSolution, List<double> weights)
    {
        double distance = 0;

        for (int j = 0; j < decisionMatrix.GetLength(1); j++)
        {
            double weightedValue = decisionMatrix[row, j] * weights[j];
            distance += Math.Pow(weightedValue - targetSolution[j], 2);
        }

        return Math.Sqrt(distance);
    }

    private List<Employee> RankEmployees(List<Employee> employees, List<double> similarityScores)
    {
        // Combine employees with their similarity scores and sort in descending order
        var employeeRankings = employees
            .Select((employee, index) => new { Employee = employee, SimilarityScore = similarityScores[index] })
            .OrderByDescending(entry => entry.SimilarityScore)
            .ToList();

        // Return the ranked employees
        return employeeRankings.Select(entry => entry.Employee).ToList();
    }
}
