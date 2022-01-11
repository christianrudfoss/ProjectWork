using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserQueryService UserQueryService;
        public UserController(IUserQueryService _userQueryService)
        {
            this.UserQueryService = _userQueryService;
        }
        [Route("Api/Users/GetAll")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return UserQueryService.GetUsers();
        }


        [Route("Api/Users/Get/{id:int}")]
        [HttpGet]
        public async Task<User> GetUser(int id)
        {
            return await UserQueryService.GetUser(id);
        }


        [Route("Api/Users/Create/")]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([Bind("Name,Email,CreatedDate,CreatedBy,Password,Gender")] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();
                var createdUser = await UserQueryService.Create(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("Api/Users/Edit/{id:int}")]
        [HttpPost]
        public async Task<ActionResult<string>> EditUser(int id, [Bind("Name,Email,CreatedDate,CreatedBy,Password,Gender,Id")] User user)
        {
            return await UserQueryService.Edit(id, user);
        }
        [Route("Api/Users/Delete/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            try
            {
                return await UserQueryService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
