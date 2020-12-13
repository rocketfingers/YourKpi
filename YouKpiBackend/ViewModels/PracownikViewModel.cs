using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class PracownikViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrybPracy { get; set; }
        public int CzasDostepuMin { get; set; }
        public bool IsAdministrator { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
