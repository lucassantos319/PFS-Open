using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities.Relations;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Application.Applications.Relations;

public class PainelUserApplication(string connectionString) : IApplication<PainelUsers>
{
    private readonly PainelUsersRepository _repository = new (connectionString);

    public ResponseResult<PainelUsers> GetByEmail(string email,ERole role)
    {
        var result = new ResponseResult<PainelUsers>();
        var painelUserExist = _repository.Get(new()
        {
            emails = [email],
            roles = [(int)  role],
            status = [(int) EStatus.Ativo]
        });

        result.obj = painelUserExist;
        return result;
    }
    
    // public ResponseResult<PainelUsers> GetByUserId(int userId)
    //     {
    //         var result = new ResponseResult<PainelUsers>();
    //         var painelUserExist = _repository.Get(new()
    //         {
    //             users = new int[]{ userId },
    //             status = new int[] {(int) EStatus.Ativo}
    //         });
    //
    //         result.obj = painelUserExist;
    //         return result;
    //     } 
    
    public IEnumerable<PainelUsers> Get(PainelUsersFilter filter)
    {
        return _repository.Get(filter);
    }
    
    public ResponseResult<int> Create(PainelUsers obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.status_id = (int)EStatus.Ativo;
        var creation = _repository.Create(obj);
        return creation;
        
    }

    public void Update(PainelUsers obj)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}