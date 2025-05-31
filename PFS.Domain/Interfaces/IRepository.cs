using PFS.Domain.Extensions;

namespace PFS.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    abstract object ExecuteInsert(string sql, object obj);

    abstract void ExecuteSql(string sql, object values);
    
    abstract IEnumerable<T> ExecuteReturn(string sql);

    abstract void Delete(int id);

    abstract int Insert(object obj);
   
    abstract IEnumerable<T> Get(bool orderBy = false,params string[] values);

    void Update(object obj);
}