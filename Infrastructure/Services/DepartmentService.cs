using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class DepartmentService
{
    private DapperContext _context;
    private readonly IWebHostEnvironment _web;

    public DepartmentService(DapperContext context,IWebHostEnvironment web)
    {
        _context = context;
        _web = web;
    }
    

    public async Task<Response<List<Department>>> GetDepartments()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"SELECT Departments.id as Id, Departments.department_name as Name From Departments";
            var result = await connection.QueryAsync<Department>(sql);
            return new Response<List<Department>>(result.ToList());
        }
    }
    public async Task<Response<Department>> AddDepartment(Department department)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var result  = await connection.ExecuteScalarAsync<int>($"INSERT INTO departments (department_name) VALUES ('{department.Name}') returning id");
                department.Id = result;
                return new Response<Department>(department);
            }
        }
        catch (Exception ex)
        {
            return new Response<Department>(HttpStatusCode.InternalServerError, ex.Message);
        }
      
    }

    public async Task<Response<int>> UpdateDepartment(Department department)
    {
        using (var connection = _context.CreateConnection())

        {
            var result  = await connection.ExecuteAsync($"Update departments Set department_name = '{department.Name}' where id = {department.Id};");
            return new Response<int>(result);
        } 
    }

    public async Task<Response<string>> DeleteDepartment(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var result = await connection.ExecuteAsync($"delete from departments where id= {id}");
            if(result > 0)
                return new Response<string>("Department deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "department not found");
        } 
    }    
}