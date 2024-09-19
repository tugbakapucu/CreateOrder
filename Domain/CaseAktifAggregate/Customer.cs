using System.ComponentModel.DataAnnotations;

namespace Domain.CaseAktifAggregate
{
    public class Customer: Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
