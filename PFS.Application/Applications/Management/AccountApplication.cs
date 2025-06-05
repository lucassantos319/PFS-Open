using PFS.Domain.Extensions;
using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;

namespace PFS.Applications
{
    public class AccountApplication : IApplication<Accounts>
    {
        public AccountRepository _repository;

        public AccountApplication(string connectionString)
        {
            if (_repository == null)
                _repository = new(connectionString);
        }


        public ResponseResult<int> Create(Accounts obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            obj.status_id = (int)EStatus.Ativo;
            var creation = _repository.Create(obj);
            return creation;
        }

        public void Update(Accounts obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<Accounts> Get(AccountFilter filter)
        {
            var responseResult = new ResponseResult<Accounts>();
            
            var accounts = _repository.Get(filter);
            if (accounts == null || accounts.Count() > 0)
            {
                responseResult.GenerateErrorStatus();
                return responseResult;
            }

            responseResult.obj = accounts;
            return responseResult;
        }
    }
}