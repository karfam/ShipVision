using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ShipVision;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetBrowserFeatureControl();
    }

    private void SetBrowserFeatureControl()
    {
        string appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "");
        using (var key = Registry.CurrentUser.CreateSubKey($"Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION"))
        {
            if (key != null && !string.IsNullOrEmpty(appName))
            {
                key.SetValue(appName, 11001, RegistryValueKind.DWord); // Use Edge mode
            }
        }
    }

    private void MapView_Loaded(object sender, RoutedEventArgs e)
    {
        // Load a satellite map using Bing Maps or Google Maps
        string mapUrl = "https://www.google.com/maps/@?api=1&amp;map_action=map&amp;basemap=satellite";
        MapView.Source = new Uri(mapUrl);
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsWindow settingsWindow = new SettingsWindow();
        settingsWindow.ShowDialog();
    }
}