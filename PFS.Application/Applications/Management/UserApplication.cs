using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;
using PFS.Infrastructure.Repositories;
using PFS.Infrastrucuture.Services;

namespace PFS.Applications
{
    public class UserApplication : IApplication<Users>
    {
        private UserRepository _repository;

        public UserApplication(string connectionString)
        {
            if (_repository == null)
                _repository = new UserRepository(connectionString);
        }

        public IEnumerable<Users> GetByFilter(UserFilter filter)
        {
            try
            {
                var users = _repository.Get(filter);
                if (users == null)
                    return Enumerable.Empty<Users>();

                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Login(UserFilter filter)
        {
            try
            {
                var user = _repository.Get(filter);
                if (user == null || user.Count() == 0)
                    return null;

                return TokenService.Generate(user.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Create(Users obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Users obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

    }
}