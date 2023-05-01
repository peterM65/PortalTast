using Microsoft.AspNetCore.Http;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Models
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            IsActive = true;
            IsDeleted = false;
            IsUpdated = false;
            CreatedOn = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string? Notes { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }
        public int? DistrictId { get; set; }
        public District? District { get; set; }
        public string? ImageName { get; set; }
        public string? CVName { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? CV { get; set; }
    }
}
