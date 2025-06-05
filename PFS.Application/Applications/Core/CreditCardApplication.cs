using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Application.Applications.Core;

public class CreditCardApplication(string connectionString) : IApplication<CreditCard>
{
    private readonly CreditCardRepository _repository = new(connectionString);
    
    public ResponseResult<int> Create(CreditCard obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.status_id = (int)EStatus.Ativo;
        var creation = _repository.Create(obj);
        return creation;
    }

    public void Update(CreditCard obj)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}