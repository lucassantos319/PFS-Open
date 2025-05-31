using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities.Management;
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

        public int Create(Accounts obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Accounts obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Accounts> GetByFilter(object filter)
        {
            throw new NotImplementedException();
        }
    }
}