using RoleAdvisor.Application.Algorithm;
using RoleAdvisor.Application.Services;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Tests;

public class RecommendationServiceTests
{
    [Fact]
    public void SelectBestEmployees_ShouldReturnCorrectResults()
    {
        // Arrange
        var skills = new List<Skill>
        {
            new Skill { Id = 1, Name = "Programming" },
            new Skill { Id = 2, Name = "Communication" },
        };

        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Developer" },
            new Role { Id = 2, Name = "Manager" },
        };

        var employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John",
                Skills = new List<Skill> { skills[0], skills[1] },
                RolesPreferred = new List<Role> { roles[0] },
                PositionsPickedFor = new List<Position>(),
            },
            new Employee
            {
                Id = 2,
                Name = "Jane",
                Skills = new List<Skill> { skills[0] },
                RolesPreferred = new List<Role> { roles[1] },
                PositionsPickedFor = new List<Position>(),
            },
        };

        var positions = new List<Position>
        {
            new Position
            {
                ProjectId = 1,
                RoleId = 1,
                SkillsRequired = new List<Skill> { skills[0] }
            },
            new Position
            {
                ProjectId = 1,
                RoleId = 2,
                SkillsRequired = new List<Skill> { skills[1] }
            },
        };

        var topsis = new TOPSIS();

        // Act
        var selectedEmployees = topsis.SelectBestEmployees(employees, positions);

        // Assert
        Assert.NotNull(selectedEmployees);
        Assert.Equal(2, selectedEmployees.Count);

        foreach (var position in positions)
        {
            var selectedEmployee = selectedEmployees.Find(e => e.Id == position.PickedEmployeeId);
            Assert.NotNull(selectedEmployee);
            Assert.Contains(position.SkillsRequired.ElementAt(0).Id, selectedEmployee.Skills.Select(s => s.Id));
        }
    }

    [Fact]
    public void SelectBestEmployees_ShouldReturnNonNullList()
    {
        // Arrange
        TOPSIS topsis = new TOPSIS();
        List<Employee> employees = new List<Employee>();
        List<Position> positions = new List<Position>();

        // Act
        List<Employee> result = topsis.SelectBestEmployees(employees, positions);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void SelectBestEmployees_ShouldNotChangeOriginalEmployeeList()
    {
        // Arrange
        TOPSIS topsis = new TOPSIS();
        List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John" },
                new Employee { Id = 2, Name = "Jane" }
            };
        List<Position> positions = new List<Position>();

        // Act
        List<Employee> result = topsis.SelectBestEmployees(employees, positions);

        // Assert
        Assert.Same(employees, result);
        Assert.Equal(2, employees.Count); // Original list should not be modified
    }

    [Fact]
    public void SelectBestEmployees_ShouldAssignPickedEmployeeIdsToPositions()
    {
        // Arrange
        TOPSIS topsis = new TOPSIS();
        List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John" },
                new Employee { Id = 2, Name = "Jane" }
            };
        List<Position> positions = new List<Position>
            {
                new Position(),
                new Position()
            };

        // Act
        List<Employee> result = topsis.SelectBestEmployees(employees, positions);

        // Assert
        foreach (Position position in positions)
        {
            Assert.NotNull(position.PickedEmployeeId);
        }
    }

    [Fact]
    public void SelectBestEmployees_ShouldReturnCorrectResults1()
    {
        // Arrange
        var skills = new List<Skill>
        {
            new Skill { Id = 1, Name = "Programming" },
            new Skill { Id = 2, Name = "Communication" },
            new Skill { Id = 3, Name = "Problem Solving" },
        };

        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Developer" },
            new Role { Id = 2, Name = "Manager" },
        };

        var employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John",
                Skills = new List<Skill> { skills[2] },
                RolesPreferred = new List<Role> { roles[0] }
            },
            new Employee
            {
                Id = 2,
                Name = "Jane",
                Skills = new List<Skill> { skills[0] },
                RolesPreferred = new List<Role> { roles[1] }
            },
            new Employee
            {
                Id = 3,
                Name = "Bob",
                Skills = new List<Skill> { skills[0], skills[2] },
                RolesPreferred = new List<Role> { roles[0] }
            },
            new Employee
            {
                Id = 4,
                Name = "Alice",
                Skills = new List<Skill> { skills[1], skills[2] },
                RolesPreferred = new List<Role> { roles[1] }
            },
        };

        var positions = new List<Position>
        {
            new Position
            {
                ProjectId = 1,
                RoleId = 1,
                SkillsRequired = new List<Skill> { skills[0] }
            },
            new Position
            {
                ProjectId = 1,
                RoleId = 2,
                SkillsRequired = new List<Skill> { skills[1] }
            },
            new Position
            {
                ProjectId = 2,
                RoleId = 1,
                SkillsRequired = new List<Skill> { skills[2] }
            },
        };

        var topsis = new TOPSIS();

        // Act
        var selectedEmployees = topsis.SelectBestEmployees(employees, positions);

        // Assert
        Assert.NotNull(selectedEmployees);

        foreach (Position position in positions)
        {
            // Ensure a valid employee is selected for each position
            Assert.NotNull(position.PickedEmployeeId);

            // Find the selected employee
            Employee selectedEmployee = employees.Find(e => e.Id == position.PickedEmployeeId);

            Assert.NotNull(selectedEmployee);
            Assert.Contains(position.SkillsRequired.ElementAt(0).Id, selectedEmployee.Skills.Select(s => s.Id));
        }
    }
}
