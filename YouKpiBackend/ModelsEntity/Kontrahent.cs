﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace YouKpiBackend.ModelsEntity
{
    public partial class Kontrahent
    {
        public Kontrahent()
        {
            MagazynCzesci = new HashSet<MagazynCzesci>();
            MagazynKomponenty = new HashSet<MagazynKomponenty>();
            MagazynProdukty = new HashSet<MagazynProdukty>();
            MagazynProduktyNiezgodne = new HashSet<MagazynProduktyNiezgodne>();
            MagazynTowary = new HashSet<MagazynTowary>();
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

        public virtual ICollection<MagazynCzesci> MagazynCzesci { get; set; }
        public virtual ICollection<MagazynKomponenty> MagazynKomponenty { get; set; }
        public virtual ICollection<MagazynProdukty> MagazynProdukty { get; set; }
        public virtual ICollection<MagazynProduktyNiezgodne> MagazynProduktyNiezgodne { get; set; }
        public virtual ICollection<MagazynTowary> MagazynTowary { get; set; }
    }
}