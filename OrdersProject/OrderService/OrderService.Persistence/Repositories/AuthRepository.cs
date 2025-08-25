using Dapper;
using OrderService.Business.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Persistence.Contexts;


namespace OrderService.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;

        public AuthRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Login login)
        {
            var sql = @"
        INSERT INTO orders.""Logins""
        (""Username"", ""Password"", ""Salt"", ""Role"", ""IsActive"", ""IsDeleted"", ""CreatedBy"", ""CreatedDate"")
        VALUES
        (@Username, @Password, @Salt, @Role, @IsActive, @IsDeleted, @CreatedBy, @CreatedDate)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, login);
        }

        public async Task<Login?> GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or whitespace", nameof(username));
            }

            try
            {
                var sql = @"SELECT * FROM orders.""Logins"" 
                    WHERE LOWER(""Username"") = LOWER(@Username)
                    LIMIT 1";

                using var connection = _context.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<Login>(sql, new { Username = username.Trim() });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving user by username: {ex.Message}");
                throw; 
            }
        }


    }
}
