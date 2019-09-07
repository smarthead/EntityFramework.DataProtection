using System.Linq;
using EF.DataProtection.Sample.Dal;
using EF.DataProtection.Sample.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EF.DataProtection.Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SampleDbContext _dbContext;

        public UsersController(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{userId}")]
        public IActionResult Get(long userId)
            => Ok(_dbContext
                    .Users
                    .FirstOrDefault(x => x.Id == userId));
        
        [HttpGet]
        public IActionResult GetAll() 
            => Ok(_dbContext.Users.ToList());

        [HttpPost]
        public IActionResult Post(UserCreateModel createModel)
        {
            var data = new PersonalData
            {
                Email = createModel.Email,
                PhoneNumber = createModel.PhoneNumber,
                SensitiveData = createModel.SensitiveData
            };
            
            var user = new User(data);

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return Ok(user.Id);
        }
    }

    public class UserCreateModel
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string SensitiveData { get; set; }
    }
}