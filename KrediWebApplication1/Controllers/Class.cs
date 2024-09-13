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
    // aktif kullanıcıları listele
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveUsers()
    {

        var users = await _userRepository.GetActiveUsersAsync();
        return Ok(users);
    }
    // onay bekleyen kullanıcıları listele
    [HttpGet("inapproval")]
    public async Task<IActionResult> GetInAppUsers()
    {
        var users = await _userRepository.GetInAppUsersAsync();
        return Ok(users);
    }

    [HttpGet("credit-taksit-details/{userId}")]
    public async Task<IActionResult> GetCreditAndTaksitDetails(Guid userId)
    {
        try
        {
            var result = await _userRepository.GetCreditAndTaksitDetailsAsync(userId);
            if (result == null || result.Count == 0)
            {
                return NotFound("No credit or taksit details found for the user.");
            }
            return Ok(result);  // Başarılı durumda 200 ve veriler gönderilir
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }



    // yeni kredi ekleyen 
    [HttpPost("addcredit")]
    public async Task<IActionResult> AddCredit(CreditDto creditDto)
    {
        var creditId = await _userRepository.AddCreditAsync(creditDto.UserName, creditDto.SpentMoney, creditDto.FirstTimeDate,creditDto.TimeCount);
        return Ok(new { CreditId = creditId });
    }
    //yeni taksit ekleyen
    [HttpPost("taksitEkle")]
    public async Task<IActionResult> AddTaksit(TaksitDto taksitDto)
    {
        // Mevcut krediID kullanılarak taksit ekleniyor
        await _userRepository.AddTaksitAsync(taksitDto.KrediID ,taksitDto.TaksitTarihi, taksitDto.Tutar, taksitDto.TaksitAyi,taksitDto.Aciklama);

        return Ok(new { Message = "Taksit başarıyla eklendi" });
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
//kredi ekleme
public class CreditDto
{
    public string UserName { get; set; }
    public decimal SpentMoney { get; set; }
    public DateTime FirstTimeDate { get; set; }
    public int TimeCount { get; set; }
}
//taksit ekleme 
public class TaksitDto
{
    
    public DateTime TaksitTarihi { get; set; }
    public decimal Tutar { get; set; }
    public int TaksitAyi { get; set; }
    public Guid KrediID { get; set; }
    public string Aciklama { get; set; }
}
public class CreditTaksitDetailsDto
{
    public Guid UserID { get; set; }            // uniqueidentifier veri tipi için Guid kullanıyoruz
    public decimal Spent_Money { get; set; }
    public DateTime First_Time_Date { get; set; }
    public int Time_Count { get; set; }
    public DateTime TaksitTarihi { get; set; }
    public decimal Tutar { get; set; }
    public int TaksitAyi { get; set; }
    public string Aciklama { get; set; }
    public Guid KrediID { get; set; }           // uniqueidentifier veri tipi için Guid kullanıyoruz
}

