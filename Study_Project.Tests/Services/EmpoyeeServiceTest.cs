using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Study_Project.Context;
using Study_Project.Models;
using Study_Project.Services;
using Xunit;

public class EmployeeServiceTests
{
    private JwtContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<JwtContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB per test
            .Options;
        return new JwtContext(options);
    }

    [Fact]
    public void AddEmployee_Should_Add_Employee_Successfully()
    {
        // Arrange
        var context = GetInMemoryContext();
        var service = new EmployeeService(context);
        var employee = new Employee { Name = "John Doe", Age = 30 };

        // Act
        var result = service.AddEmployee(employee);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        Assert.Single(context.Employees); // DB now has 1 employee
    }

    [Fact]
    public void GetEmployeeDetails_Should_Return_All_Employees()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Employees.Add(new Employee { Name = "Emp1", Age = 25 });
        context.Employees.Add(new Employee { Name = "Emp2", Age = 30 });
        context.SaveChanges();

        var service = new EmployeeService(context);

        // Act
        var employees = service.GetEmployeeDetails();

        // Assert
        Assert.Equal(2, employees.Count);
    }

    [Fact]
    public void GetEmployeeById_Should_Return_Correct_Employee()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Employees.Add(new Employee { Id = 1, Name = "Emp1", Age = 25 });
        context.SaveChanges();
        var service = new EmployeeService(context);

        // Act
        var employee = service.GetEmployeeById(1);

        // Assert
        Assert.NotNull(employee);
        Assert.Equal("Emp1", employee.Name);
    }

    [Fact]
    public void DeleteEmployee_Should_Remove_Employee()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Employees.Add(new Employee { Id = 1, Name = "ToDelete", Age = 35 });
        context.SaveChanges();
        var service = new EmployeeService(context);

        // Act
        var result = service.DeleteEmployee(1);

        // Assert
        Assert.True(result);
        Assert.Empty(context.Employees);
    }

    [Fact]
    public void UpdateEmployee_Should_Modify_Employee()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Employees.Add(new Employee { Id = 1, Name = "Old Name", Age = 25 });
        context.SaveChanges();
        var service = new EmployeeService(context);
        var updatedEmployee = new Employee { Name = "New Name", Age = 28 };

        // Act
        var result = service.UpdateEmployee(1, updatedEmployee);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Name", result.Name);
        Assert.Equal(28, result.Age);
    }
}
