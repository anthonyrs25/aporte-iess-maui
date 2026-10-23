using Rodriguez.ViewModels;

namespace Rodriguez.Views;

public partial class VistaInicio : ContentPage
{
    public VistaInicio(VistaInicioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}