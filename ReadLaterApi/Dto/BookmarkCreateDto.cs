using System.ComponentModel.DataAnnotations;

namespace ReadLaterApi.Dto
{
    public class BookmarkCreateDto
    {
        [Url]
        public string URL { get; set; }

        [StringLength(maximumLength: 500)]
        public string ShortDescription { get; set; }
        
        public int? CategoryId { get; set; }
    }
}
