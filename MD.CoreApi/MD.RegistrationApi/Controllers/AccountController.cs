using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MD.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MD.RegistrationApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> Get()
        {
            return _userManager.Users.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<AppUser> Get(string id)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("Registered successfully");
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }
    }

    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Please, check password min length", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
