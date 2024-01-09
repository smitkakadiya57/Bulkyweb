using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulkyweb.Models
{
    public class Category
    {
        [Key]   // explicitly define primary key 
        public int Id { get; set; }
        [Required] // this field is required 
        [MaxLength(30)]
		[DisplayName("Category Name")]
		public string Name { get; set; }
        [DisplayName("Display Order")]  // this will display in label tag
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")]  //validation
        public int DisplayOrder { get; set; }

    }
}
