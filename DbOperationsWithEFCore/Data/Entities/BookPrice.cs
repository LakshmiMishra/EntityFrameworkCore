namespace DbOperationsWithEFCore.Data.Entities
{
    public class BookPrice
    {
        public int Id{ get; set; } 

        public int BookId  { get; set; }
        public int CurrencyId { get; set; }

        public int Price { get; set; }

        public  Book Book { get; set; }
        public Currency Currency { get; set; }

    }
}
