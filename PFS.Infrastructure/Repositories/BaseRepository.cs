using PFS.Domain.Interfaces;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using PFS.Domain.Extensions;

namespace PFS.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private NpgsqlConnection _connection;

        protected BaseRepository(string connection)
        {
            _connection = new(connection);
        }

        public abstract string[] _columns { get; }
        
        private string[] _baseColumns = new[]{"id","created_at","updated_at"};
        public abstract string _tableName { get; }
        public abstract string[] _innerJoinTable { get; }
        public abstract string _abvTable {get;} 
        public abstract string[] _columnsInnerJoin { get; }
        
        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertDB(object obj)
        {
            try
            {
                var query = $"insert into {_tableName} ({_columns.SelectQueryInsert()}) values ({_columns.ValuesQueryInsert()})";
                var id = int.Parse(ExecuteInsert(query,obj).ToString());

                if (id != 0)
                    return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
    
            return 0;
        }

        public virtual object ExecuteInsert(string sql,object obj)
        {
            object insertId = new();
            
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Execute(sql,obj);
                    insertId = _connection.ExecuteScalar<int>("select lastval();");
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            _connection.Close();
            return insertId;
        }

        public virtual IEnumerable<T> ExecuteReturn<T>(string sql)
        {
            try
            {
                var data = _connection.Query<T>(sql);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual void ExecuteSql(string sql, object values)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Execute(sql, values);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            _connection.Close();
        }

        public IEnumerable<T> Get(bool orderBy,int limit = 0, params string[] values)
        {
            string innerJoinTablesValues = string.Empty;
            foreach (var value in _innerJoinTable)
                if (!string.IsNullOrEmpty(value))
                    innerJoinTablesValues += $"inner join {value} ";

            var allColumns = _baseColumns.InsertPrefixTableAbv(_abvTable) + "," + string.Join(",", _columns.InsertPrefixTableAbv(_abvTable));
            if (_columnsInnerJoin != null)
                allColumns += "," + string.Join(",", _columnsInnerJoin);
                
            var query = $"select {allColumns} from {_tableName} {_abvTable} [innerJoinTables] [whereList]";
                
            if (values != null && values.Length > 0 && !values.Any(x => string.IsNullOrEmpty(x)))
            {
                query = query.Replace("[whereList]", "where ");
                query += string.Join("and ", values);
            }
            else
                query = query.Replace("[whereList]", "");

            query = query.Replace("[innerJoinTables]", innerJoinTablesValues);
            if (orderBy)
                query += $" order by {_tableName}.id";

            if (limit > 0)
                query += $" limit {limit}";
            
            return ExecuteReturn<T>(query);
        }

        public void Update(object obj)
        {
            try
            {
                var query = $"update into {_tableName} ({_columns.SelectQueryInsert()}) values ({_columns.ValuesQueryInsert()})";
                ExecuteSql(query,obj);
            }
            catch (Exception ex)
            {
            }
        }
    }
}