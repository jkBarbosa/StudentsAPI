using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;

namespace Domain.Models
{
    [Table("Address")]
    public class Address : BaseEntity
    {
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(15)]
        public string PostalCode { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Line1 { get; set; }
        [MaxLength(50)]
        public string Line2 { get; set; }

        public static Address Create(string country, string state, string postalCode, string city, string line1, string line2, long id = 0)
        {
            return new Address()
            {
                State = state,
                City = city,
                Country = country,
                Line1 = line1,
                Line2 = line2,
                PostalCode = postalCode,
                Id = id
            };
        }
    }
}
