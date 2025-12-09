using DataTransferObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public interface TenantIService
    {
        TenantResponse AddTenant(TenantAddRequest? countryAddRequest);
        List<TenantResponse> ListAllTenant();
        TenantResponse? GetTenantByID(Guid? tenantID);

    }
}
