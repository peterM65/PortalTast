using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required , StringLength(50)]
        public string Name { get; set; }
        
        [Required , StringLength(50)]
        public string Code { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
