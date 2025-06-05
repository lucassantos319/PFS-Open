using PFS.Domain.Extensions;

namespace PFS.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    abstract object ExecuteInsert(string sql, object obj);

    abstract void ExecuteSql(string sql, object values);
    
    abstract IEnumerable<T> ExecuteReturn<T>(string sql);

    abstract void Delete(int id);

    abstract int InsertDB(object obj);
   
    abstract IEnumerable<T> Get(bool orderBy = false,int limit = 0,params string[] values);

    void Update(object obj);
}