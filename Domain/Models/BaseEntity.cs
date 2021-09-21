using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class BaseEntity : IdEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "TIMESTAMP(6)")]
        public DateTime CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "TIMESTAMP(6)")]
        public DateTime ModifiedOn { get; set; }
    }
}
