using DataTransferObject;
using DataTransferObject.DTO;
using Entities;
using System.Diagnostics.Metrics;

namespace Services
{
    public class TenantService : TenantIService
    {

        private readonly List<WhiteLabelTenant> _tenants;
        public TenantService(bool initialize = true)
        {
            _tenants = new List<WhiteLabelTenant>();
            if (initialize) {
                _tenants.AddRange(new List<WhiteLabelTenant>()
                {
                new WhiteLabelTenant()
                {
                    TenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
                    TenantName = "Tenant A"
                },
                new WhiteLabelTenant()
                {
                    TenantID = Guid.Parse("1E83383A-8816-4DF8-A6D3-6C336CEF4CC4"),
                    TenantName = "Tenant B"
                },
                new WhiteLabelTenant()
                {
                    TenantID = Guid.Parse("CBB21EA5-9C63-4E6E-9F02-37918E3BDBF2"),
                    TenantName = "Tenant C"
                },
                new WhiteLabelTenant()
                {
                    TenantID = Guid.Parse("2431659E-879F-4EEB-B172-81C6776B686B"),
                    TenantName = "Tenant D"
                },
                new WhiteLabelTenant()
                {
                    TenantID = Guid.Parse("427E3E9C-1254-48D0-B0C1-50FC4BB936C8"),
                    TenantName = "Tenant E"
                }
                });
            }
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
                throw new ArgumentException("Given Tenant name already exists");
            }
            tenant.TenantID = Guid.NewGuid();
            _tenants.Add(tenant);
            return tenant.ToTenantResponse2();
        }

        public List<TenantResponse> ListAllTenant()
        {
            return _tenants.Select(tenant => tenant.ToTenantResponse2()).ToList();
        }
        public TenantResponse? GetTenantByID(Guid? TenantID)
        {
            if (TenantID == null)
                return null;

            WhiteLabelTenant? tenant_response_from_list = _tenants.FirstOrDefault(temp => temp.TenantID == TenantID);

            if (tenant_response_from_list == null)
                return null;

            return tenant_response_from_list.ToTenantResponse2();
        }

    }
}
