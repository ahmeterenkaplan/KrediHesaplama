using KrediWebApplication1;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UsersController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    //register
    [HttpPost]
    public async Task<IActionResult> AddUser(UserDto userDto)
    {
        var userId = await _userRepository.AddUserAsync(userDto.UserName, userDto.First_Name, userDto.Surname, userDto.Pass);
        return Ok(new { UserId = userId });
    }
    //login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        try
        {
            var auth = await _userRepository.LoginUserAsync(userLoginDto.UserName, userLoginDto.Pass);
            return Ok(new { Auth = auth });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
//register
public class UserDto
{
    public string UserName { get; set; }
    public string First_Name { get; set; }
    public string Surname { get; set; }
    public string Pass { get; set; }
}
//login
public class UserLoginDto
{
    public string UserName { get; set; }
    public string Pass { get; set; }
}

