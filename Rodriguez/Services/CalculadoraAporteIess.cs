namespace Rodriguez.Services;

public class CalculadoraAporteIess
{
    private const decimal PorcentajeAporte = 0.0945m;

    public decimal CalcularAporte(decimal salario)
    {
        return Math.Round(salario * PorcentajeAporte, 2, MidpointRounding.AwayFromZero);
    }
}