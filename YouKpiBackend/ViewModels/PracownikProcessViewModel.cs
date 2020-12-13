using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ViewModels
{
    public class PracownikProcessViewModel
    {
        public string ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int UserId { get; set; }
        public bool UserAssigned { get; set; }
    }
}
