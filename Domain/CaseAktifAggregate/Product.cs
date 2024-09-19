namespace Domain.CaseAktifAggregate
{
    public class Product: Entity
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
