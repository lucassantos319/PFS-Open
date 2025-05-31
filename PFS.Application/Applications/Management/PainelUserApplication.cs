using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities.Relations;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Applications;

public class PainelUserApplication : IApplication<PainelUsers>
{
    private readonly PainelUsersRepository _repository;

    public PainelUserApplication(string connectionString)
    {
        _repository = new (connectionString);
    }

    public ResponseResult<PainelUsers> GetByEmail(string email,ERole role)
    {
        var result = new ResponseResult<PainelUsers>();
        var painelUserExist = _repository.Get(new()
        {
            emails = new string[]{email},
            roles = new int[] {(int)  role },
            status = new int[]{(int) EStatus.Ativo}
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
    
    public int Create(PainelUsers obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        obj.status_id = (int)EStatus.Ativo;
        var id = _repository.Create(obj);
        if (id != 0)
            return id;
    
        return 0;
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