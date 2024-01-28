using PracticeTest.Core.model;

namespace PracticeTest.Web.ViewModels;

public class EmployeeViewModel
{
 public  List<EmployeeViewInherited>? _employeeViewInheriteds { get; set; }
}

public class EmployeeViewInherited : Employee
{
  public float? DearnessAllowance { get; set; }
  public float? ConveyanceAllowance { get; set; }
  public float? HouseRentAllowance { get; set; }
  public float? GrossSalary { get; set; }
  public float? PT { get; set; }
  public float? TotalSalary { get; set; }
}
