using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Addresses.Commands
{
    public class UpdateAddress
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
}
