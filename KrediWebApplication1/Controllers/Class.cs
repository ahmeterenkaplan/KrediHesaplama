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
    [HttpPost("register")]
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
    // hesabı aktifleştiren ya da pasifleştiren (approval) 
    [HttpPost("approval")]
    public async Task<IActionResult> AccountStatus(AccountStatusDto accountStatusDto)
    {
        try
        {
            var account = await _userRepository.AccountStatusAsync(accountStatusDto.UserName, accountStatusDto.Account);
            return Ok(new { Account = account });
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
//hesap durumu
public class AccountStatusDto
{
    public string UserName { get; set; }
    public string Account { get; set; }
}


