using PFS.Domain.Interfaces;
using PFS.Domain.Models.Entities;
using PFS.Domain.Models.Filters;
using PFS.Domain.Models.RequestBody;
using PFS.Infrastructure.Repositories;
using PFS.Infrastrucuture.Services;

namespace PFS.Application.Applications.Management
{
    public class UserApplication : IApplication<Users>
    {
        private UserRepository _repository;

        public UserApplication(string connectionString)
        {
            if (_repository == null)
                _repository = new UserRepository(connectionString);
        }

        public IEnumerable<Users> Get(UserFilter filter)
        {
            return _repository.Get(filter);
        }

        public string Login(UserFilter filter)
        {
            var user = _repository.Get(filter);
            var usersEnumerable = user as Users[] ?? user.ToArray();
            if (usersEnumerable.Count() == 0)
                return string.Empty;

            return TokenService.Generate(usersEnumerable.FirstOrDefault() ?? throw new InvalidOperationException());
        }

        public ResponseResult<int> Create(Users obj)
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