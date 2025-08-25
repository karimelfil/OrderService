using OrderService.Domain.Entities;


namespace OrderService.Business.Interfaces
{
    public interface IAuthRepository
    {
        Task<Login?> GetByUsername(string username);
        Task CreateAsync(Login login);


    }
}
