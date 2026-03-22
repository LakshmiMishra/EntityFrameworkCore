namespace DbOperationsWithEFCore.Data.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Title  { get; set; }
        public string Description{ get; set; }
        public List<BookPrice> BookPrices { get; set; }
    }
}
