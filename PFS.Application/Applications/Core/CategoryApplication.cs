using PFS.Domain.Extensions;
using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Application.Applications.Core;

public class CategoryApplication(string connectionString) : IApplication<Categories>
{
    private readonly CategoryRepository _repository = new(connectionString);

    public ResponseResult<int> Create(Categories obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        obj.status_id = (int)EStatus.Ativo;
        var creation = _repository.Create(obj);
        return creation;
    }

    public ResponseResult<Categories> AddCategory(Categories obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        
        var result = new ResponseResult<Categories>();
        var existedCategory = _repository.Get(new()
        {
            name = obj.name
        });
        
        return result;
    }
    
    public void Update(Categories obj)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public ResponseResult<Categories> Get(CategoriesFilter filter, int limit = 0)
    {
        var responseResult = new ResponseResult<Categories>();
            
        var categories = _repository.Get(filter);
        var responseResultObj = categories as Categories[] ?? categories.ToArray();
        if (!responseResultObj.Any())
        {
            responseResult.GenerateErrorStatus();
            return responseResult;
        }

        responseResult.obj = responseResultObj;
        return responseResult;
    }
}