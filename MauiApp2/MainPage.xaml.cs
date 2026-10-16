namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCalcularClicked(object sender, EventArgs e)
        {
            // 1. Obtener valores sin espacios adicionales
            string nombre = txtNombre.Text?.Trim() ?? "";
            string apellido = txtApellido.Text?.Trim() ?? "";
            string edadText = txtEdad.Text?.Trim() ?? "";
            string salarioText = txtSalario.Text?.Trim() ?? "";

            // 2. Validar que todos los campos estén completos
            if (string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(apellido) ||
                string.IsNullOrEmpty(edadText) ||
                string.IsNullOrEmpty(salarioText))
            {
                await DisplayAlert("Campos Incompletos", "Por favor, complete todos los campos antes de continuar.", "OK");
                return;
            }

            // 3. Validar que la edad sea un número entero válido y positivo
            if (!int.TryParse(edadText, out int edad) || edad <= 0)
            {
                await DisplayAlert("Dato Inválido", "Por favor, ingrese una edad válida.", "OK");
                return;
            }

            // 4. Validar que el salario sea un número decimal válido y mayor a cero
            if (!decimal.TryParse(salarioText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal salario) || salario <= 0)
            {
                await DisplayAlert("Dato Inválido", "Por favor, ingrese un salario válido.", "OK");
                return;
            }

            // 5. Cálculo del aporte al IESS (9.45%)
            decimal aporteIESS = salario * 0.0945m;

            // 6. Alerta con salto de línea (\n) según el requerimiento
            string mensaje = $"Bienvenido {nombre} {apellido}\n" +
                             $"Tienes {edad} años\n" +
                             $"Tu aporte mensual es ${aporteIESS:F2}";

            await DisplayAlert("Resultado del Cálculo", mensaje, "Aceptar");
        }
    }
}