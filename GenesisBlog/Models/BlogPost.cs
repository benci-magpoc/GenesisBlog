using System.ComponentModel.DataAnnotations;

namespace GenesisBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage ="The {0} must be at leasty {2} and at most {1} characters long",MinimumLength=5)]
        public string Title { get; set; } = "";

        [Required]
        public string Abstract { get; set; } = "";

        [Required]
        public string Content { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime? Updated { get; set; }

    }
}
