namespace Joana.Application.DTOs.AdministratorDTOS;

public class AdministratorDto
{
    public int Id { get; set; }
    public required string Ime { get; set; }
    public required string Prezime { get; set; }
    public required string Email { get; set; }
}