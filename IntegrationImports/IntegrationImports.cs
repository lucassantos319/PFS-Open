using PFS.Applications;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.RequestBody;

namespace IntegrationImports;

public class IntegrationImports
{
    private readonly string _connectionStringMG;
    private readonly string _connectionStringCore;
    protected AccountApplication _accountApplication { get; set;}
    
    public IntegrationImports(string connectionStringMG, string connectionStringCore)
    {
        _connectionStringMG = connectionStringMG;
        _connectionStringCore = connectionStringCore;
        _accountApplication = new(_connectionStringCore);
    } 
    
    public void Run()
    {
        var accountPendingUpdate = GetPendingUpdateAccounts();
    }

    private ResponseResult<Accounts> GetPendingUpdateAccounts()
    {
           
        var accounts = _accountApplication.Get(new()
        {
            updated_integration = true
        });
        
        return null;

    }
}