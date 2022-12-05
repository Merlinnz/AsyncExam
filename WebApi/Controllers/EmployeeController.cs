using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController
{
    private EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    

    [HttpGet("GetEmployee")]
    public async Task<Response<List<Employee>>> GetEmployees() 
    {
        return await _employeeService.GetEmployees();
    }

    [HttpPost("AddEmployee")]
    public async Task<Response<Employee>> AddEmployee([FromForm]Employee employee)
    {
        return await _employeeService.AddEmployee(employee);
    } 
    
    [HttpPut("UpdateEmployee")]
    public async Task<Response<int>> UpdateEmployee(Employee employee)
    {
        return await _employeeService.UpdateEmployee(employee);
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<Response<string>> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}