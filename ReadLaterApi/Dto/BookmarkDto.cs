using System;

namespace ReadLaterApi.Dto
{
    public class BookmarkDto
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string ShortDescription { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
