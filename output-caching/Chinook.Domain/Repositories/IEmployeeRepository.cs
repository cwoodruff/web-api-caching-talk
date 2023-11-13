﻿using Chinook.Domain.Entities;

namespace Chinook.Domain.Repositories;

public interface IEmployeeRepository : IDisposable
{
    Task<List<Employee>> GetAll();
    Task<Employee> GetById(int id);
    Task<Employee> GetReportsTo(int id);
    Task<Employee> Add(Employee newEmployee);
    Task<bool> Update(Employee employee);
    Task<bool> Delete(int id);
    Task<List<Employee>> GetDirectReports(int id);
}