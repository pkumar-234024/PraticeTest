using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTest.SharedKernel;

namespace PracticeTest.Core.model;

partial class Employee : BaseEntity
{
  // Properties
  [Column(TypeName = "int")]
  public int? EmployeeCode { get; set; }

  [Column(TypeName = "varchar(50)")]
  [StringLength(50)]
  public string? EmployeeName { get; set; }

  [Column(TypeName = "datetime")]
  public DateTime DateOfBirth { get; set; }

  [Column(TypeName = "bit")]
  public bool? Gender { get; set; }

  [Column(TypeName = "varchar(20)")]
  [StringLength(20)]
  public string? Department { get; set; }

  [Column(TypeName = "varchar(20)")]
  [StringLength(20)]
  public string? Designation { get; set; }

  [Column(TypeName = "float")]
  public float? BasicSalary { get; set; }

}
