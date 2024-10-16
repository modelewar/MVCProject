using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo.PL.viewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; } //PK
        [Required(ErrorMessage = "Name Is Required ")]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Is Required ")]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        //Navigational Property [Many]
    }
}
