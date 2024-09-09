using KrediWebApplication1.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
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






    }
}

