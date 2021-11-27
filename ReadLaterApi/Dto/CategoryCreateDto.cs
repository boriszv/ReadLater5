using System.ComponentModel.DataAnnotations;

namespace ReadLaterApi.Dto
{
    public class CategoryCreateDto
    {
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
