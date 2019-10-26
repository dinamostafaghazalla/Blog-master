using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Article
    {
        [Key] public int Id { get; set; }
        [Required] public string Author { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
    }
}