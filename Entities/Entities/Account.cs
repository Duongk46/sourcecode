using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int ID { set; get; }
        [StringLength(50)]
        public string Username { set; get; }
        [StringLength(50)]
        public string Password { set; get; }
        public virtual ICollection<Category> Categories { set; get; }
        public virtual ICollection<Product> Products { set; get; }
    }
}
