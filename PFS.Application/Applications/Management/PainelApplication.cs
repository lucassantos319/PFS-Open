using PFS.Application.Applications.Relations;
using PFS.Domain.Extensions;
using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Entities.Relations;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Application.Applications.Management;

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
        var result = new ResponseResult<Painel>();
        var existedUser = _userApplication.Get(new()
        {
            email = email,
            status = [1]
            
        }).FirstOrDefault();

        if (existedUser == null)
        {
            result.GenerateErrorStatus($"Usuário {email} não existe/está ativo");
            return result;
        }
        
        var existedPainel = _painelUserApplication.GetByEmail(email, role);
        if (!existedPainel.obj.Any())
        {
            
            var createdResult = Create(obj);
            if ( !createdResult.success)
            {
                result.GenerateErrorStatus($"Não foi possivel realizar o processo de criação do painel !");
                return result;     
            }
            
            int painelId = createdResult.obj.FirstOrDefault();
            var createdRelation = _painelUserApplication.Create(new PainelUsers
            {
                painel_id = painelId,
                user_id = existedUser.id,
                role_id = (int) role
            });

            if (!createdRelation.success)
            {
                result.GenerateErrorStatus($"Não foi possivel realizar o processo de vinculação.");
                return result;
            }
            
            obj.id = painelId;
            result.obj = new[] {obj};
        }   
        else
        {
            var mainPainel =  existedPainel.obj.FirstOrDefault();
            if (mainPainel != null)
            {
                var exist = _repository.Get(new()
                {
                    ids = new[] { existedPainel.obj.FirstOrDefault()!.painel_id }
                }).FirstOrDefault();

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

    public ResponseResult<int> Create(Painel obj)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(obj);

            obj.status_id = (int)EStatus.Ativo;
            var creation = _repository.Create(obj);
            return creation;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
        var painelUsersEnumerable = painelUserResult as PainelUsers[] ?? painelUserResult.ToArray();
        if ( !painelUsersEnumerable.Any() )
        {
            result.GenerateErrorStatus("Não encontrado nenhum painel ativo !");
            return result;
        }
        
        var painelsResult = _repository.Get(new()
        {
            ids = painelUsersEnumerable.Select(x=>x.painel_id)
        });

        var resultObj = painelsResult as Painel[] ?? painelsResult.ToArray();
        if (painelsResult == null && !resultObj.Any())
        {
            result.GenerateErrorStatus("Não encontrado nenhum painel ativo !");
            return result;
        }
        
        
        result.obj = resultObj;
        return result;
        
    }
}