using System.ComponentModel.DataAnnotations;

namespace ReadLaterApi.Dto
{
    public class BookmarkUpdateDto
    {
        public string URL { get; set; }

        [StringLength(maximumLength: 500)]
        public string ShortDescription { get; set; }
    }
}
