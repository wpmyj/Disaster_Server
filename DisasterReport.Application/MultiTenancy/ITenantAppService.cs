using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DisasterReport.MultiTenancy.Dto;

namespace DisasterReport.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
