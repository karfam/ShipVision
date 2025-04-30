using System.Windows;
using Microsoft.Win32;

namespace ShipVision
{
    public partial class SettingsWindow : Window
    {
        private const string RegistryKeyPath = "Software\\ShipVision\\Settings";

        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                if (key != null)
                {
                    ProviderComboBox.Text = key.GetValue("AISProvider", "aisstream.io")?.ToString();
                    ApiKeyTextBox.Text = key.GetValue("APIKey", string.Empty)?.ToString();
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedProvider = ProviderComboBox.Text;
            string apiKey = ApiKeyTextBox.Text;

            using (var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
            {
                key?.SetValue("AISProvider", selectedProvider);
                key?.SetValue("APIKey", apiKey);
            }

            MessageBox.Show($"Provider: {selectedProvider}\nAPI Key: {apiKey}", "Settings Saved", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}