using InvoicingApp.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Entities
{
    /// <summary>
    /// Klient
    /// </summary>
    public class Client : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? VatNumber { get; set; }

        public Client()
        {
            Id = 0;
            Name = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
            Country = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            IdentificationNumber = string.Empty;
            VatNumber = string.Empty;
        }

        public Client(int id, string name, string address, string city, string postalCode, string country, string email, string phone, string? identificationNumber, string? vatNumber)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Email = email;
            Phone = phone;
            IdentificationNumber = identificationNumber;
            VatNumber = vatNumber;
        }

        public override string ToString()
        {
            return $"ID klienta: {Id}\n" +
                   $"Jméno: {Name}\n" +
                   $"Adresa: {Address}, {PostalCode}, {City}, {Country}\n" +
                   $"Email: {Email}\n" +
                   $"Telefon: {Phone}\n" +
                   $"IČO: {IdentificationNumber}\n" +
                   $"DIČ: {VatNumber}";
        }
    }
}
