using DataTransferObject;
using DataTransferObject.DTO;
using Services;
using Xunit;
namespace TestDriven
{
    public class TenantServiceTest
    {
        private readonly TenantIService _tenantService;
        public TenantServiceTest()
        {
            _tenantService = new TenantService();
        }
        #region AddTenant
        //When TenantAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddTenant_NullTenant()
        {
            //Arrange
            TenantAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _tenantService.AddTenant(request);
            });
        }

        //When the TenantName is null, it should throw ArgumentException
        [Fact]
        public void AddTenant_TenantNameIsNull()
        {
            //Arrange
            TenantAddRequest? request = new TenantAddRequest() { TenantName = null };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _tenantService.AddTenant(request);
            });
        }


        //When the TenantName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddTenant_DuplicateTenantName()
        {
            //Arrange
            TenantAddRequest? request1 = new TenantAddRequest() { TenantName = "SomeTenant" };
            TenantAddRequest? request2 = new TenantAddRequest() { TenantName = "SomeTenant" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _tenantService.AddTenant(request1);
                _tenantService.AddTenant(request2);
            });
        }

        //When you supply proper Tenant name, it should insert (add) the Tenant to the existing list of Tenants
        [Fact]
        public void AddTenant_ProperTenantDetails()
        {
            //Arrange
            TenantAddRequest? request = new TenantAddRequest() { TenantName = "SomeTenant3" };

            //Act
            TenantResponse response = _tenantService.AddTenant(request);
            List<TenantResponse> tenants_from_ListAllTenants = _tenantService.ListAllTenant();


            //Assert
            Assert.True(response.TenantID != Guid.Empty);
            Assert.Contains(response, tenants_from_ListAllTenants);
        }
        #endregion
        #region GetAllTenants

        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<TenantResponse> actual_Tenant_response_list = _tenantService.ListAllTenant();

            //Assert
            Assert.Empty(actual_Tenant_response_list);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<TenantAddRequest> tenant_request_list = new List<TenantAddRequest>() {
            new TenantAddRequest() { TenantName = "Tenant1" },
            new TenantAddRequest() { TenantName = "Tenant2" }
      };

            //Act
            List<TenantResponse> tenants_list_from_add_tenant = new List<TenantResponse>();

            foreach (TenantAddRequest tenant_request in tenant_request_list)
            {
                tenants_list_from_add_tenant.Add(_tenantService.AddTenant(tenant_request));
            }

            List<TenantResponse> actualTenantResponseList = _tenantService.ListAllTenant();

            //read each element from tenants_list_from_add_tenant
            foreach (TenantResponse expected_Tenant in tenants_list_from_add_tenant)
            {
                Assert.Contains(expected_Tenant, actualTenantResponseList);
            }
        }
        #endregion
        #region GetTenantByTenantID

    [Fact]
    //If we supply null as TenantID, it should return null as TenantResponse
    public void GetTenantByTenantID_NullTenantID()
    {
      //Arrange
      Guid? tID = null;

      //Act
      TenantResponse? Tenant_response_from_get_method = _tenantService.GetTenantByID(tID);


      //Assert
      Assert.Null(Tenant_response_from_get_method);
    }


    [Fact]
    //If we supply a valid Tenant id, it should return the matching Tenant details as TenantResponse object
    public void GetTenantByTenantID_ValidTenantID()
    {
      //Arrange
      TenantAddRequest? Tenant_add_request = new TenantAddRequest() { TenantName = "Example Tenant" };
      TenantResponse Tenant_response_from_add = _tenantService.AddTenant(Tenant_add_request);

      //Act
      TenantResponse? Tenant_response_from_get = _tenantService.GetTenantByID(Tenant_response_from_add.TenantID);

      //Assert
      Assert.Equal(Tenant_response_from_add, Tenant_response_from_get);
    }
    #endregion
    }
}
