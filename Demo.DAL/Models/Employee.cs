using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; } // PK

        [Required]
        [MaxLength(50)]

        public string Name { get; set; }


        public int? Age { get; set; }


        public string Address { get; set; }

        public decimal Salary { get; set; }
        public bool Isctive { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime HirDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string ImageName { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        //FK Optional => OnDelete:Restrict 
        //FK Required => OnDelete:Cascade

        [InverseProperty("Employees")]
        public Department Department { get; set; }

    }
}
