using System;

namespace OpetNet.Domain.Models
{
    public class Customer
    {
        public Customer(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Customer() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
