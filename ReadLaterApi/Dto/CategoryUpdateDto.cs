using System.ComponentModel.DataAnnotations;

namespace ReadLaterApi.Dto
{
    public class CategoryUpdateDto
    {
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
