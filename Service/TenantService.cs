using DataTransferObject;
using DataTransferObject.DTO;
using Entities;
using System.Diagnostics.Metrics;

namespace Services
{
    public class TenantService : TenantIService
    {

        private readonly List<WhiteLabelTenant> _tenants;
        public TenantService()
        {
            _tenants = new List<WhiteLabelTenant>();
        }
        public TenantResponse AddTenant(TenantAddRequest? TenantAddRequest)
        {
            if (TenantAddRequest is null)
            {
                throw new ArgumentNullException(nameof(TenantAddRequest));
            }
            WhiteLabelTenant tenant = TenantAddRequest.ToTenant();
            if (tenant.TenantName is null)
            {
                throw new ArgumentException(nameof(tenant.TenantName));
            }
            //Validation: check for duplicated Name
            if (_tenants.Where(temp => temp.TenantName == TenantAddRequest.TenantName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }
            tenant.TenantID = Guid.NewGuid();
            _tenants.Add(tenant);
            return tenant.ToTenantResponse();
        }

        public List<TenantResponse> ListAllTenant()
        {
            return _tenants.Select(tenant => tenant.ToTenantResponse()).ToList();
        }
        public TenantResponse? GetTenantByID(Guid? TenantID)
        {
            if (TenantID == null)
                return null;

            WhiteLabelTenant? tenant_response_from_list = _tenants.FirstOrDefault(temp => temp.TenantID == TenantID);

            if (tenant_response_from_list == null)
                return null;

            return tenant_response_from_list.ToTenantResponse();
        }
    }
}
