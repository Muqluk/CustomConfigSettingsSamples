using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

using ConfigSectionCode;

namespace CustomConfigSectionSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetEnvironments();
        }

        public void GetEnvironments()
        {
            string output;
            Configuration envConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;

            EnvironmentConfigSection environmentsSection = envConfig.GetSection("ConfiguredEnvironments") as EnvironmentConfigSection;

            if (environmentsSection != null)
            {
                List<EnvironmentElement> environments = (from EnvironmentElement env in environmentsSection.Environments select env).ToList();
                MessageBox.Show(environments.Select(x => x.Name).First());
            }
            else
            {
                throw new System.Exception("Issues with your config idiot.");
            }

        }

        private void GetConfig1()
        {
            string output;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;
            UrlsSection myUrlsSection = config.GetSection("MyUrls") as UrlsSection;

            if (myUrlsSection == null)
            {
                output = "Failed to load UrlsSection.";
            }
            else
            {
                output = "Collection elements contained in the custom section collection:\r\n";
                for (int i = 0; i < myUrlsSection.Urls.Count; i++)
                {
                    output += $"   Name={myUrlsSection.Urls[i].Name} URL={myUrlsSection.Urls[i].Url} Port={myUrlsSection.Urls[i].Port}\r\n";
                }
            }

        }
    }
}
