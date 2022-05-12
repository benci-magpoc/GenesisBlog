﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenesisBlog.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(40, ErrorMessage = "The {0} must be a minimum of {2} characters and a max of {1}.", MinimumLength =3)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(40, ErrorMessage = "The {0} must be a minimum of {2} characters and a max of {1}.", MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }





        //TODO - Add more user stuff...
        public virtual ICollection<BlogPostComment> BlogPostComments { get; set; } = new HashSet<BlogPostComment>();
    }
}
