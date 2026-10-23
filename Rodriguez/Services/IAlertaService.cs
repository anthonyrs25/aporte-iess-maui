namespace Rodriguez.Services;

public interface IAlertaService
{
    Task MostrarAsync(string titulo, string mensaje);
}