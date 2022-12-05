using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class EmployeeService
{
    private DapperContext _context;
    private readonly IWebHostEnvironment _web;

    public EmployeeService(DapperContext context,IWebHostEnvironment web)
    {
        _context = context;
        _web = web;
    }
    

    public async Task<Response<List<Employee>>> GetEmployees()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"SELECT Employees.id as Id, Employees.first_name as FirstName, Employees.last_name as LastName, Employees.phone_number as Phone From Employees";
            var result = await connection.QueryAsync<Employee>(sql);
            return new Response<List<Employee>>(result.ToList());
        }
    }
    public async Task<Response<Employee>> AddEmployee(Employee employee)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var result  = await connection.ExecuteScalarAsync<int>($"INSERT INTO employees (first_name, last_name, phone_number) VALUES ('{employee.FirstName}'), ('{employee.LastName}'), ('{employee.Phone}') returning id");
                employee.Id = result;
                return new Response<Employee>(employee);
            }
        }
        catch (Exception ex)
        {
            return new Response<Employee>(HttpStatusCode.InternalServerError, ex.Message);
        }
      
    }

    public async Task<Response<int>> UpdateEmployee(Employee employee)
    {
        using (var connection = _context.CreateConnection())

        {
            var result  = await connection.ExecuteAsync($"Update employees Set first_name = '{employee.FirstName}', last_name = '{employee.LastName}', phone_number = '{employee.Phone}' where id = {employee.Id};");
            return new Response<int>(result);
        } 
    }

    public async Task<Response<string>> DeleteEmployee(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var result = await connection.ExecuteAsync($"delete from Employees where id= {id}");
            if(result > 0)
                return new Response<string>("Employee deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "Employee not found");
        } 
    }    
}