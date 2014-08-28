using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCStore.Data.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter Category Name.")]
        [StringLength(16, ErrorMessage = "Invalid Category Name. Please try again.")]
        [Display(Name = "Category name")]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}
