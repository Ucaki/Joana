using Joana.Application.DTOs.AdministratorDTOS;

namespace Joana.Application.Interfaces;

public interface IAdministratorService
{
    Task<AdministratorDto?> GetByEmailAsync(string email);
}