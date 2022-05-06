using GenesisBlog.Enums;
using System.ComponentModel.DataAnnotations;

namespace GenesisBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage ="The {0} must be at least {2} and at most {1} characters long",MinimumLength=5)]
        public string Title { get; set; } = "";

        [Required]
        public string Abstract { get; set; } = "";

        [Required]
        public string Content { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime? Updated { get; set; }

        //This property is derived from the Title. This will eventually be used in some cases INSTEAD of the Primary Key (Id)              
        public string Slug { get; set; } = "";

        public bool IsDeleted { get; set; }

        public BlogPostState BlogPostState { get; set; }

        //What if I wanted to record an image with a blog post
        public byte[] ImageData { get; set; } = Array.Empty<byte>();
        public string ImageType { get; set; } = string.Empty;

        //Nav props
        public virtual ICollection<BlogPostComment> BlogPostComments { get; set; } = new HashSet<BlogPostComment>();
  

    
    
    }
}
