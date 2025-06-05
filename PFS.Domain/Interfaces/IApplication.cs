using PFS.Domain.Models.RequestBody;

namespace PFS.Domain.Interfaces;

public interface IApplication<T> where T : class
{
    ResponseResult<int> Create(T obj);
    void Update(T obj);
    void DeleteById(int id);

}