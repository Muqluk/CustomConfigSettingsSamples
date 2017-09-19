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
            GetSingleCollectionElements();
        }

        public void GetSingleCollectionElements()
        {
            Configuration envConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;

            SingleElementCollectionSection configSection = envConfig.GetSection("SingleElementCollectionSample") as SingleElementCollectionSection;

            if (configSection != null)
            {
                List<SingleCollectionSampleElement> elementsCollection;
                elementsCollection = (from SingleCollectionSampleElement element 
                                      in configSection.SingleCollectionElements
                                      select element).ToList();
                var firstElem = elementsCollection.Select(x => x).First();
                MessageBox.Show($"Name: { firstElem.Name }, FirstProp: { firstElem.SampleStringValue1 }, OptionalProp: { firstElem.SampleIntValue1 }");
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
