using System.Globalization;
using System.Windows.Input;
using Rodriguez.Models;
using Rodriguez.Services;

namespace Rodriguez.ViewModels;

public class VistaInicioViewModel
{
    private readonly CalculadoraAporteIess _calculadora;
    private readonly IAlertaService _alertas;

    public VistaInicioViewModel(CalculadoraAporteIess calculadora, IAlertaService alertas)
    {
        _calculadora = calculadora;
        _alertas = alertas;
        CalcularCommand = new Command(async () => await CalcularAsync());
    }

    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Edad { get; set; } = string.Empty;
    public string Salario { get; set; } = string.Empty;

    public ICommand CalcularCommand { get; }

    private async Task CalcularAsync()
    {
        // 1. Todos los campos completos
        if (string.IsNullOrWhiteSpace(Nombre) ||
            string.IsNullOrWhiteSpace(Apellido) ||
            string.IsNullOrWhiteSpace(Edad) ||
            string.IsNullOrWhiteSpace(Salario))
        {
            await _alertas.MostrarAsync("Datos incompletos", "Todos los campos son obligatorios.");
            return;
        }

        // 2. Edad: solo números enteros
        if (!int.TryParse(Edad, out int edad) || edad <= 0 || edad > 120)
        {
            await _alertas.MostrarAsync("Edad inválida", "La edad debe ser un número entero entre 1 y 120.");
            return;
        }

        // 3. Salario: solo números (acepta coma o punto decimal)
        string salarioTexto = Salario.Trim().Replace(',', '.');

        if (!decimal.TryParse(salarioTexto, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal salario) || salario <= 0)
        {
            await _alertas.MostrarAsync("Salario inválido", "El salario debe ser un número mayor a 0, por ejemplo 470.50");
            return;
        }

        // 4. Datos revisados: se arma la orden de trabajo
        var afiliado = new Afiliado
        {
            Nombre = Nombre.Trim(),
            Apellido = Apellido.Trim(),
            Edad = edad,
            Salario = salario
        };

        // 5. Cálculo y resultado
        decimal aporte = _calculadora.CalcularAporte(afiliado.Salario);

        string mensaje = $"Bienvenido {afiliado.NombreCompleto}\nTienes {afiliado.Edad} años\nTu aporte mensual es ${aporte:F2}";

        await _alertas.MostrarAsync("Aporte al IESS", mensaje);
    }
}