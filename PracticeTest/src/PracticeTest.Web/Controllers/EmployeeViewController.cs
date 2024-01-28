using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using PracticeTest.Core.model;
using PracticeTest.SharedKernel.Interfaces;
using PracticeTest.Web.ViewModels;
using static Humanizer.In;

namespace PracticeTest.Web.Controllers;
public class EmployeeViewController : Controller
{
  private readonly IRepository<Employee> _employeeRepository;
  public EmployeeViewController(IRepository<Employee> employeeRepository)
  {
    _employeeRepository = employeeRepository;
  }
  public async Task<IActionResult> Employee()
  {
    EmployeeViewModel model = new EmployeeViewModel();
    List<EmployeeViewInherited> empmodelList = new List<EmployeeViewInherited>();
    try
    {
      var employees = await _employeeRepository.ListAsync();
      foreach (var employee in employees)
      {
        EmployeeViewInherited empmodel = new EmployeeViewInherited();
        empmodel.Id = employee.Id;
        empmodel.EmployeeName = employee.EmployeeName;
        empmodel.EmployeeCode = employee.EmployeeCode;
        empmodel.DateOfBirth = employee.DateOfBirth;
        empmodel.Gender = employee.Gender;
        empmodel.Department = employee.Department;
        empmodel.BasicSalary = employee.BasicSalary;
        empmodel.Designation = employee.Designation;
        empmodel.DearnessAllowance = (employee.BasicSalary * 40) / 100;
        empmodel.ConveyanceAllowance = ((empmodel.DearnessAllowance * 10) / 100)<250? ((empmodel.DearnessAllowance * 10) / 100):250; // or 250(which ever is lower)
        empmodel.HouseRentAllowance = (employee.BasicSalary * 25) / 100<1500? (employee.BasicSalary * 25) / 100 : 1500; //or 1500 (which ever is higher)
        empmodel.GrossSalary = employee.BasicSalary + empmodel.DearnessAllowance + empmodel.ConveyanceAllowance + empmodel.HouseRentAllowance;//(Do not display Gross Salary)
        empmodel.PT = (empmodel.GrossSalary <= 3000 )? 100: (empmodel.GrossSalary > 3000 && empmodel.GrossSalary <= 6000) ? 150 :200;
        empmodel.TotalSalary = empmodel.BasicSalary + empmodel.DearnessAllowance + empmodel.ConveyanceAllowance + empmodel.HouseRentAllowance - empmodel.PT;
        empmodelList.Add(empmodel );
      }
      model._employeeViewInheriteds = empmodelList;
    }
    catch (Exception ex) {
    
    }
    return View(model);
  }

  [HttpPost]
  public async Task<IActionResult> AddOrUpdateEmployee(Employee model)
  {
    try
    {
      if(model.Id == 0)
      {
        await _employeeRepository.AddAsync(model);
      }
      else
      {
        Employee employee = await _employeeRepository.GetByIdAsync(model.Id);
        if (employee != null)
        {
          employee.EmployeeCode = model.EmployeeCode;
          employee.EmployeeName = model.EmployeeName;
          employee.DateOfBirth = model.DateOfBirth;
          employee.Department = model.Department;
          employee.Designation = model.Designation;
          employee.BasicSalary = model.BasicSalary;
          employee.Gender = model.Gender;

          await _employeeRepository.UpdateAsync(employee);
        }
      }
      
    }

    catch (Exception ex)
    {

    }

    return RedirectToAction("Employee");
  }

  
  public async Task<IActionResult> AddOrEditEmployee(Employee model)
  {
    Employee employee = new Employee();
    try
    {
      if(model.Id != 0)
      {
        employee = await _employeeRepository.GetByIdAsync(model.Id);
      }
      
    }
    catch (Exception ex)
    {

    }

    return View(employee);
  }

  
  public async Task<IActionResult> EmployeeDelete(Employee model)
  {
    try
    {
      Employee deleteEmployee = await _employeeRepository.GetByIdAsync(model.Id);
      await _employeeRepository.DeleteAsync(deleteEmployee);
    }
    catch (Exception ex)
    {

    }

    return RedirectToAction("Employee");
  }
  
}
