using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IAdministratorRepository
{
    Task<Administrator?> GetByEmailAsync(string  email);
    Task<Administrator?> GetByIdAsync(int administratorId);
}