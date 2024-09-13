using KrediWebApplication1.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace KrediWebApplication1
{
    public class UserRepository
    {
        private readonly DataContext _context;

        // DataContext bağımlılığı, Dependency Injection tarafından sağlanacak
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        // Yeni bir kullanıcı eklemek için bir saklı yordamı çağıran asenkron metod
        public async Task<Guid> AddUserAsync(string userName, string firstName, string surname, string pass)
        {
            var userId = Guid.NewGuid();

            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "dbo.spAddUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@UserName", userName));
                    command.Parameters.Add(new SqlParameter("@First_Name", firstName));
                    command.Parameters.Add(new SqlParameter("@Surname", surname));
                    command.Parameters.Add(new SqlParameter("@Pass", pass));

                    var outputParam = new SqlParameter("@UserID", System.Data.SqlDbType.UniqueIdentifier)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                    await _context.Database.CloseConnectionAsync();

                    userId = (Guid)outputParam.Value;
                }
            }
            catch (SqlException ex)
            {
                // Log or handle the exception as needed
                throw new Exception("An error occurred while adding the user: " + ex.Message);
            }

            return userId;
        }
        // Log-In işlemi için bir saklı yordamı çağıran asenkron metod  
        public async Task<string> LoginUserAsync(string userName, string pass)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "dbo.spLogin";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@UserName", userName));
                    command.Parameters.Add(new SqlParameter("@Pass", pass));

                    var authParam = new SqlParameter("@Auth", System.Data.SqlDbType.NVarChar, 50)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(authParam);

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                    await _context.Database.CloseConnectionAsync();

                    var authValue = authParam.Value as string;

                    if (string.IsNullOrEmpty(authValue))
                    {
                        throw new Exception("Login failed. Username or password is incorrect.");
                    }

                    return authValue; // Kullanıcı yetkisini döndür.
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred during login: " + ex.Message);
            }
        }
        // Hesabı aktifleştiren ya da pasifleştiren bir saklı yordamı çağıran asenkron metod    
        public async Task<string> AccountStatusAsync(string userName, string accountStatus)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "dbo.spAccountApproval";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Input parametreleri ekle
                    command.Parameters.Add(new SqlParameter("@UserName", userName));
                    command.Parameters.Add(new SqlParameter("@AccountStatus", accountStatus));

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                    await _context.Database.CloseConnectionAsync();

                    // Durum başarıyla güncellenmişse herhangi bir çıktı yoksa bir mesaj döndürebilirsiniz
                    return "Account status updated successfully.";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while updating the account status: " + ex.Message);
            }
        }
        // onay bekleyen getir
        public async Task<String> GetActiveUsersAsync()
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "dbo.spGetApprovedUsers";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var users = string.Empty;
                    await _context.Database.OpenConnectionAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var userName = reader["UserName"].ToString();
                            var firstName = reader["First_Name"].ToString();
                            var surname = reader["Surname"].ToString();

                            users += $"{userName} {firstName} {surname} , ";
                        }
                    }

                    await _context.Database.CloseConnectionAsync();

                    return users.TrimEnd(',', ' '); // Onayda bekleyen kullanıcıları döndür
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while getting active users: " + ex.Message);
            }
        }

        // onay bekleyen getir
        public async Task<String> GetInAppUsersAsync()
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "dbo.spGetInApprovedUsers";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var users = string.Empty;
                    await _context.Database.OpenConnectionAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var userName = reader["UserName"].ToString();
                            var firstName = reader["First_Name"].ToString();
                            var surname = reader["Surname"].ToString();

                            users += $"{userName} {firstName} {surname}  ";
                        }
                    }

                    await _context.Database.CloseConnectionAsync();

                    return users.TrimEnd(' ', ','); // Onayda bekleyen kullanıcıları döndür
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while getting active users: " + ex.Message);
            }
        }
        public async Task<List<CreditTaksitDetailsDto>> GetCreditAndTaksitDetailsAsync(Guid userId)
        {
            var result = new List<CreditTaksitDetailsDto>();

            // SQL bağlantısını kullanarak stored procedure çağırıyoruz
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "spGetCreditAndTaksitDetails";  // Stored Procedure adı
                command.CommandType = CommandType.StoredProcedure;

                // UserID parametresini ekliyoruz
                var userIdParam = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier)
                {
                    Value = userId
                };
                command.Parameters.Add(userIdParam);

                _context.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var details = new CreditTaksitDetailsDto
                        {
                            UserID = reader.GetGuid(reader.GetOrdinal("UserID")),
                            Spent_Money = reader.GetDecimal(reader.GetOrdinal("Spent_Money")),
                            First_Time_Date = reader.GetDateTime(reader.GetOrdinal("First_Time_Date")),
                            Time_Count = reader.GetInt32(reader.GetOrdinal("Time_Count")),
                            TaksitTarihi = reader.GetDateTime(reader.GetOrdinal("TaksitTarihi")),
                            Tutar = reader.GetDecimal(reader.GetOrdinal("Tutar")),
                            TaksitAyi = reader.GetInt32(reader.GetOrdinal("TaksitAyi")),
                            Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                            KrediID = reader.GetGuid(reader.GetOrdinal("KrediID"))
                        };

                        result.Add(details);
                    }
                }
            }

            return result;
        }


        // Kredi eklemek için bir saklı yordamı çağıran asenkron metod
        public async Task<Guid> AddCreditAsync(string userName, decimal spentMoney, DateTime firstTimeDate, int timeCount)
{
    try
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "dbo.spAddCreditTransaction";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserName", userName));
            command.Parameters.Add(new SqlParameter("@SpentMoney", spentMoney));
            command.Parameters.Add(new SqlParameter("@FirstTimeDate", firstTimeDate));
            command.Parameters.Add(new SqlParameter("@TimeCount", timeCount));

            await _context.Database.OpenConnectionAsync();
            await command.ExecuteNonQueryAsync();
            await _context.Database.CloseConnectionAsync();
        }
    }
    catch (SqlException ex)
    {
        throw new Exception("An error occurred while adding the credit: " + ex.Message);
    }

    return Guid.NewGuid();  // Eğer kredi ID'sini kullanmak istiyorsanız, burayı ihtiyacınıza göre düzenleyin
}
        public async Task AddTaksitAsync(Guid krediID, DateTime taksitTarihi, decimal tutar, int taksitAyi,string aciklama)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "dbo.spEkleTaksit";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Mevcut bir krediID'yi kullanarak Taksit ekleme işlemi
                    command.Parameters.Add(new SqlParameter("@KrediID", krediID)); // KrediID'yi gönderiyoruz
                    command.Parameters.Add(new SqlParameter("@TaksitTarihi", taksitTarihi));
                    command.Parameters.Add(new SqlParameter("@Tutar", tutar));
                    command.Parameters.Add(new SqlParameter("@TaksitAyi", taksitAyi));
                    command.Parameters.Add(new SqlParameter("@Aciklama", aciklama));

                    await _context.Database.OpenConnectionAsync();
                    await command.ExecuteNonQueryAsync();
                    await _context.Database.CloseConnectionAsync();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Taksit eklenirken bir hata oluştu: " + ex.Message);
            }
        }





    }
}





    


