namespace Domain.CaseAktifAggregate
{
    public class CustomerOrder:Entity
    {
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
