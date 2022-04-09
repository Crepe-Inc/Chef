using System.Windows;
using System.Runtime.InteropServices;
using System;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Controls;

namespace Chef
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public class Profile
        {
            public string name { get; set; }
            public string iconPath { get; set; }
            public string b64hosts { get; set; }
        }

        public class Profiles
        {
            public Profile[] profiles { get; set; }
        }

        public string toJSON(Profiles obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        public Profiles? fromJSON(string json)
        {
            Profiles? obj = JsonSerializer.Deserialize<Profiles>(json);
            return obj;
        }
        private void addProfile(Button btn)
        {
            profileList.Items.Add(btn);
        }
        public MainWindow()
        {
            InitializeComponent();
            Profiles? profiles = fromJSON(getProfileConfig());
            if (profiles != null)
            {
                foreach (Profile profile in profiles.profiles)
                {
                    Button button = new Button();
                    button.Content = profile.name;
                    button.Name = $"profileBtn_{profile.name}";
                    addProfile(button);
                }
            }
        }
        private string getProfileConfig()
        {
            var pathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Chef/");
            var fileName = Path.Combine(pathName, "config.json");
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            } else
            {
                if (!Directory.Exists(pathName))
                {
                    Directory.CreateDirectory(pathName);
                }
                string defaultJSON = @"{""profiles"": [{""name"": ""Default"",""iconPath"": """",""b64hosts"": """"}]}";
                File.WriteAllText(fileName, defaultJSON);
                return defaultJSON;
            }
        }
        private void setProfileConfig(string content)
        {
            var pathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Chef/");
            var fileName = Path.Combine(pathName, "config.json");
            File.WriteAllText(fileName, content);
        }
        private void toTaskbar(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void fromTaskbar(object sender, RoutedEventArgs e)
        {
            this.Show();
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void openProfileConfig(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            var pathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Chef/");
            var fileName = Path.Combine(pathName, "config.json");
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/K start " + fileName;
            process.Start();
        }
    }
}
