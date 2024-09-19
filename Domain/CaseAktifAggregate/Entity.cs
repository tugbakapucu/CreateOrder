using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.CaseAktifAggregate
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

        [Column(Order = 200, TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 201, TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        public int Status { get; set; }

    }
}
