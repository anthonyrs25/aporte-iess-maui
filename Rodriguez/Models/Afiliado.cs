namespace Rodriguez.Models;

public class Afiliado
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int Edad { get; set; }
    public decimal Salario { get; set; }

    public string NombreCompleto => $"{Nombre} {Apellido}";
}