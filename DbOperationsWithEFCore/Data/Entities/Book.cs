namespace DbOperationsWithEFCore.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }

        public DateOnly PublishedDate { get; set; }

        public  int  LanguageId { get; set; }
        public Language? Language { get; set; }
    }
   
}
