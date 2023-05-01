using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*department name required")]
        [MinLength(2, ErrorMessage = "*min len 2")]
        [MaxLength(50, ErrorMessage = "*Max len 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*department name required")]
        [MinLength(2, ErrorMessage = "*min len 2")]
        [MaxLength(50, ErrorMessage = "*Max len 50")]
        public string Code { get; set; }
    }
}
