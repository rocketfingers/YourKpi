﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class Client
    {
        public Client()
        {
            Offer = new HashSet<Offer>();
        }

        public int Id { get; set; }
        public string Nip { get; set; }
        public string Name { get; set; }
        public string KodPocztowy { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string NrBudynku { get; set; }
        public string NrTelefonu { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Offer> Offer { get; set; }
    }
}