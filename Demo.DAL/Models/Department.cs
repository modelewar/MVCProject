using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public  class Department
    {
        public  int  Id { get; set; } //PK
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; } 
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; }= new HashSet<Employee>();
        //Navigational Property [Many]
    }
}
