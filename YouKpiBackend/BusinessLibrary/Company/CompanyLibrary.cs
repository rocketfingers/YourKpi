using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.BusinessLibrary.Company
{
    public class CompanyLibrary
    {
        private readonly IMapper _mapper;
        readonly YoukpiContext _context;

        public CompanyLibrary(YoukpiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyBasicInfoViewModel> GetUserCompanyInfo()
        {
            var companyBasicInfo = await _context.CompanyBasicInfo.FirstOrDefaultAsync();

            return _mapper.Map<CompanyBasicInfoViewModel>(companyBasicInfo);
        }
    }
}
