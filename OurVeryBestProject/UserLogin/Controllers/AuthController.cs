using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UserLogin.Data;
using UserLogin.Dtos;
using UserLogin.Helpers;
using UserLogin.Helpers.EmailService;
using UserLogin.Models;

namespace UserLogin.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepository,IEmailService emailService,JwtService jwtService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _jwtService = jwtService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(DtoRegister dto)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<string> errors=ModelState.Values.SelectMany(v=>v.Errors).Select(x=>x.ErrorMessage);
                return BadRequest(new {message=errors });
            }
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                IsAdmin = dto.IsAdmin,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CodeForPassword = null
                
            };
            try
            {
                if(dto.Email != null && await _userRepository.FindByEmailAsync(dto.Email)!=null)
                    return BadRequest(new { message = "the email is exist" });
                return Created("sucseed", await _userRepository.CreateAsync(user));
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error server" });
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(DtoLogin dto)
        {
            User? user;
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                BadRequest(errors);
            }
            if (dto.Email != null)
            {
                try
                {
                    user = await _userRepository.FindByEmailAsync(dto.Email);
                }
                catch (Exception) {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error server" });
                }
                if (user == null)
                {
                    return BadRequest(new { message = "invalid credentials" });
                }
                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    return BadRequest(new { message = "invalid credentials" });
                }
            }
            else
            {
                return BadRequest(new { message = "the email is empty" });
            }
            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new
            {
                message="success"
            });
        }
        [HttpPut("user/{id}")]
        public async Task<IActionResult> UserUpdate(int id,DtoEdit dto )
        {
            var user = new User
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            try
            {
                if (id != user.Id)
                {
                    return BadRequest(new { message = "User ID mistach" });
                }
                var userToUpdate = await _userRepository.FindByIdAsync(id);
                if (userToUpdate == null)
                {
                    return NotFound($"user with id={id} not found");
                }
                await _userRepository.UpdateAsync(user);
                return Ok("succeed");
                
            }catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error updating data");
            }

        }
        [HttpGet("user")]
        public async Task<IActionResult> UserLogin()
        {
            try { 
            var jwt = Request.Cookies["jwt"];
                if (jwt == null)
                {
                    throw new Exception();
                }
                    var token = _jwtService.Verify(jwt);
                    var userId = int.Parse(token.Issuer);

                    return Ok(await _userRepository.FindByIdAsync(userId));

            }catch (Exception)
            {
                return Unauthorized();
            }
        }
  
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message= "sucseed"
            });
        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(DtoSendEmail dto)
        {
            Random random = new Random();
            try
            {
                string code = random.Next(100000, 999999).ToString();
                if(dto.Email != null) { 
                await _emailService.SendEmail(dto.Email,code);
                await _userRepository.SendCodeForPassword(BCrypt.Net.BCrypt.HashPassword(code), dto.Email);
                return Ok(new
                {
                    message = "sucseed"
                });
                }
                else
                {
                    return BadRequest(new { message = "dont send email, Email is NULL" });
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error server" });
            }
        }
        [HttpPost("verifyCode")]
        public async Task<IActionResult> verifyCode(DtoVerifyCode dto)
        {
            string? code;
            try
            {
                if(dto.Email == null)
                {
                    return BadRequest(new { message = "the email is Empty" });
                }
                code = await _userRepository.FindCodeForPassword(dto.Email);
                if (BCrypt.Net.BCrypt.Verify(dto.code, code))
                {
                    await _userRepository.SendCodeForPassword(null, dto.Email);
                    return Ok(new
                    {
                        message = "sucseed"
                    });
                }
                else
                    return BadRequest(new { message = "the code is wrong" });
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error server" });
            }
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(DtoChangePassword dto)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                return BadRequest(new { message = errors });
            }
            string password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            try
            {
                if (dto.Email != null)
                {
                    await _userRepository.UpdatePassworrd(dto.Email, password);
                    return Ok(new
                    {
                        message = "sucseed"
                    });
                }
                else
                    return BadRequest(new { message = "the email is Empty" });
            }
            catch(Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error server" });
            }

        }

    }
}
