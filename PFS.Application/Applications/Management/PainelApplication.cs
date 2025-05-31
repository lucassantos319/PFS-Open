using PFS.Domain.Interfaces;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Entities.Relations;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;
using PFS.Domain.Extensions;
using PFS.Domain.Models.Filters;

namespace PFS.Applications;

public class PainelApplication : IApplication<Painel>
{
    private readonly PainelRepository _repository;

    private readonly PainelUserApplication _painelUserApplication;
    private readonly UserApplication _userApplication;
    
    public PainelApplication(string connectionString)
    {
        _repository = new(connectionString);
        _painelUserApplication = new(connectionString);
        _userApplication = new(connectionString);
    }

    public ResponseResult<Painel> AddPainel(Painel obj, string email,ERole role)
    {
        var result = new ResponseResult<Painel>()
        {
            success = true
        };
        
        var existedUser = _userApplication.GetByFilter(new()
        {
            email = email,
            status = new[]{ 1 }
            
        }).FirstOrDefault();

        if (existedUser == null)
        {
            result.GenerateErrorStatus($"Usuário {email} não existe/está ativo");
            return result;
        }
        
        var existedPainel = _painelUserApplication.GetByEmail(email, role);
        if (!existedPainel.obj.Any())
        {
            var painelId = Create(obj);
            var createdRelation = _painelUserApplication.Create(new PainelUsers
            {
                painel_id = painelId,
                user_id = existedUser.id,
                role_id = (int) role
            });

            if (createdRelation == 0)
            {
                result.GenerateErrorStatus($"Não foi possivel realizar o processo de vinculação.");
                return result;
            }
            
            obj.id = painelId;
            result.obj = new[] {obj};
            
            return result;
        }   
        else
        {
            var mainPainel =  existedPainel.obj?.FirstOrDefault();
            if (mainPainel != null)
            {
                var exist = _repository.Get(new()
                {
                    ids = new[] { existedPainel.obj.FirstOrDefault().painel_id }
                })?.FirstOrDefault();

                if (exist == null)
                {
                    result.GenerateErrorStatus($"Não encontrado o painel existe.");
                    return result;
                }

                result.obj = new[] { exist };
                return result;
            }
        }

        return result;
    }

    public int Create(Painel obj)
    {
        try
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            obj.status_id = (int)EStatus.Ativo;
            var id = _repository.Create(obj);
            if (id != 0)
                return id;
                // _painelUsersRepository.Insert();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return 0;
    }

    public void Update(Painel obj)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public ResponseResult<Painel> Get(PainelUsersFilter filter)
    {
        var result = new ResponseResult<Painel>();
        
        var painelUserResult = _painelUserApplication.Get(filter);
        if ( painelUserResult == null || painelUserResult.Count() == 0 )
        {
            result.GenerateErrorStatus("Não encontrado nenhum painel ativo !");
            return result;
        }
        
        var painelsResult = _repository.Get(new()
        {
            ids = painelUserResult.Select(x=>x.painel_id)
        });
        
        if (painelsResult == null && painelsResult.Count() == 0)
        {
            result.GenerateErrorStatus("Não encontrado nenhum painel ativo !");
            return result;
        }
        
        
        result.obj = painelsResult;
        return result;
        
    }
}