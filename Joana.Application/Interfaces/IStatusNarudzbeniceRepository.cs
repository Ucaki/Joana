using Joana.Domain;

namespace Joana.Application.Interfaces;

public interface IStatusNarudzbeniceRepository
{
    Task<List<StatusNarudzbenice>> GetAsync();
    Task<StatusNarudzbenice?> GetByIdAsync(int statusNarudzbeniceId);
}