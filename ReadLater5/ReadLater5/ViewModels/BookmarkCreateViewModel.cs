using System.ComponentModel.DataAnnotations;

namespace ReadLater5.ViewModels
{
    public class BookmarkCreateViewModel
    {
        [Required]
        [Url]
        public string URL { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public bool CreateCategory { get; set; }

        public string CategoryName { get; set; }
    }
}
