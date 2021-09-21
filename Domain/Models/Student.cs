using Core.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Students")]
    public class Student : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        [Column(TypeName = "DATE")]
        public DateTime BirthDate { get; set; }

        public long AddressId { get; set; }

        public virtual Address Address { get; set; }

        public static Student Create(string name, DateTime birthDate, long addressId, long id)
        {
            return new Student()
            {
                Name = name,
                BirthDate = birthDate,
                AddressId = addressId,
                Id = id
            };
        }

    }
}
