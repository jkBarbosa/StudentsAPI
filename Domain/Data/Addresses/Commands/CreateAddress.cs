﻿namespace Domain.Data.Addresses.Commands
{
    public class CreateAddress
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
}
