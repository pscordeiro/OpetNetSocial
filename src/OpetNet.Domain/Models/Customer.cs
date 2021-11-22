using System;

namespace OpetNet.Domain.Models
{
    public class Customer
    {
        public Customer(Guid id, string name, string email, DateTime birthDate, string passWord)
        {
            Id = id;
            Name = name;
            Email = email;
            PassWord = passWord;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Customer() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }
        public string PassWord { get; private set; }

        public DateTime BirthDate { get; private set; }
        public string UrlImgProfile { get; set; }
        public string UrlProfile { get; set; }
    }
}
