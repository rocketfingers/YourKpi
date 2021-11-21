using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.ModelsEntity
{
    public partial class ProcessArea
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string NazwaEn { get; set; }
        public int Group { get; set; }
        public int? SubjectAreaGroup { get; set; }
    }
}
