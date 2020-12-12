using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class CustomerSimpleViewModel
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Nip { get; set; }
    }
}
