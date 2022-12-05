using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController
{
    private DepartmentService _departmentService;
    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    

    [HttpGet("GetDepartment")]
    public async Task<Response<List<Department>>> GetDepartments() 
    {
        return await _departmentService.GetDepartments();
    }

    [HttpPost("AddDepartment")]
    public async Task<Response<Department>> AddDepartment(Department department)
    {
        return await _departmentService.AddDepartment(department);
    } 
    
    [HttpPut("UpdateDepartment")]
    public async Task<Response<int>> UpdateDepartment(Department department)
    {
        return await _departmentService.UpdateDepartment(department);
    }

    [HttpDelete("DeleteDepartment")]
    public async Task<Response<string>> DeleteDepartment(int id)
    {
        return await _departmentService.DeleteDepartment(id);
    }
}