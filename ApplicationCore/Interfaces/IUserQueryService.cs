using ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserQueryService
    {
        public List<User> GetUsers();
        public Task<User> GetUser(int id);
        public Task<User> Create(User user);
        public Task<string> Delete(int id);
        public Task<string> Edit(int id, User user);
    }
}
