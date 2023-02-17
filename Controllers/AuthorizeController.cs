using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DTOs.Auth;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Controllers;

[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
public class AuthorizeController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthorizeController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return $"AuthorizeController :: acessed in: {DateTime.Now.ToLongDateString()}"; 
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> ResisterUser([FromBody] UserDTO userDTO)
    {
        var user = new IdentityUser
        {
            UserName = userDTO.Email,
            Email = userDTO.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, userDTO.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _signInManager.SignInAsync(user, false);

        return Ok(GenerateToken(userDTO));
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Login([FromBody] UserDTO userDTO)
    {
        var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "invalid login");
            return BadRequest(ModelState);
        }
            
        return Ok(GenerateToken(userDTO));  
                    
    } 

    private UserToken GenerateToken(UserDTO userDTO)
    {
        var Claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userDTO.Email),
            new Claim("permission", "user"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var timeExpiration = _configuration["TokenConfiguration:ExpirateHours"];
        var expiration = DateTime.UtcNow.AddHours(double.Parse(timeExpiration));


        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenConfiguration:Issuer"],
            audience: _configuration["TokenConfiguration:Audience"],
            claims: Claims,
            expires: expiration,
            signingCredentials: credentials);

        return new UserToken()
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "successfully generated token"
        };
    }

}
