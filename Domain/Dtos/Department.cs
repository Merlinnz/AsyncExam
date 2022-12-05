namespace Domain.Dtos;

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ManagerId { get; set; }
    public int LocationId { get; set; }
}
