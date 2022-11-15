using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Model;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;
        public UserController(AppDbContext _db)
        {
            db = _db;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                return await db.users.ToListAsync();
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetDetalis")]
        public async Task<ActionResult<User>> Detalis(int id)
        {
            try
            {
                var user = await db.users.FindAsync(id);
                return user;

            }
            catch (System.Exception)
            { 
                return NotFound();
            }
          
        }

        [Authorize]
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                db.users.Add(user);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}


