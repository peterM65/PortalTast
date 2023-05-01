using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Entities
{
    [Table("City")]
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public int CountryId { get; set; }
        public Country country { get; set; }
    }
}
