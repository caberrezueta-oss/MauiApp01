using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp2.views.v;

public partial class inicio1 : ContentPage
{
    private double total = 0;

    public inicio1()
    {
        InitializeComponent();
    }

    // Agregar elemento dinámicamente al panel derecho sin Popups
    private void AgregarElementoDirecto(string nombre, double precio)
    {
        // Ocultar mensaje de carrito vacío
        LblEmptyCart.IsVisible = false;

        // Actualizar total
        total += precio;
        LblTotal.Text = $"${total:F2}";

        // Crear fila dinámica en la lista visual del pedido
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        var lblNombre = new Label { Text = nombre, TextColor = Microsoft.Maui.Graphics.Colors.White, FontSize = 12 };
        var lblPrecio = new Label { Text = $"${precio:F2}", TextColor = Color.FromArgb("#D4AF37"), FontSize = 12, FontAttributes = FontAttributes.Bold };

        grid.Children.Add(lblNombre);
        Grid.SetColumn(lblNombre, 0);

        grid.Children.Add(lblPrecio);
        Grid.SetColumn(lblPrecio, 1);

        ContainerItems.Children.Add(grid);

        // Mostrar notificación flotante que se desvanece sola
        MostrarNotificacion($"Se agregó '{nombre}' a la orden.");
    }

    private void OnAgregarPlato1(object sender, EventArgs e) => AgregarElementoDirecto("Filet Mignon", 48.00);
    private void OnAgregarPlato2(object sender, EventArgs e) => AgregarElementoDirecto("Langosta Mantequilla", 62.00);
    private void OnAgregarPlato3(object sender, EventArgs e) => AgregarElementoDirecto("Vino Margaux", 120.00);
    private void OnAgregarPlato4(object sender, EventArgs e) => AgregarElementoDirecto("Souflé Chocolate", 22.00);

    private void OnLimpiarClicked(object sender, EventArgs e)
    {
        total = 0;
        LblTotal.Text = "$0.00";
        ContainerItems.Children.Clear();
        LblEmptyCart.IsVisible = true;
        MostrarNotificacion("Se ha limpiado la orden.");
    }

    private void OnEnviarCocinaClicked(object sender, EventArgs e)
    {
        if (total == 0)
        {
            MostrarNotificacion("Agregue al menos un platillo primero.");
            return;
        }

        MostrarNotificacion("¡Orden enviada con éxito a la cocina!");
        OnLimpiarClicked(sender, e);
    }

    private void OnConfirmarReservaDirecta(object sender, EventArgs e)
    {
        string personas = PckPersonas.SelectedItem?.ToString() ?? "2 Personas";
        string horario = PckHorario.SelectedItem?.ToString() ?? "20:00 PM";

        MostrarNotificacion($"Reserva Confirmada: {personas} para {horario}.");
    }

    private async void MostrarNotificacion(string mensaje)
    {
        LblNotificationText.Text = mensaje;
        NotificationBanner.IsVisible = true;

        // Espera 2.5 segundos y se oculta automáticamente sin interrumpir
        await Task.Delay(2500);
        NotificationBanner.IsVisible = false;
    }

    private void OnNavClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            BtnInicio.BackgroundColor = BtnMenu.BackgroundColor = BtnReservas.BackgroundColor = BtnCava.BackgroundColor = BtnContacto.BackgroundColor = Colors.Transparent;
            BtnInicio.TextColor = BtnMenu.TextColor = BtnReservas.TextColor = BtnCava.TextColor = BtnContacto.TextColor = Color.FromArgb("#CCCCCC");

            button.BackgroundColor = Color.FromArgb("#2A2415");
            button.TextColor = Color.FromArgb("#D4AF37");

            LblSeccionTitulo.Text = button.Text.Trim();
        }
    }
}