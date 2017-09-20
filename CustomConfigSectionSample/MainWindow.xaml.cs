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
            GetFeatureTeams();
            //GetSingleCollectionElements();
        }

        public void GetFeatureTeams()
        {

            Configuration envConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;
            TeamEnvironmentConfigurationsSection teamEnvCfgs = envConfig.GetSection("FeatureTeamConfigurations") as TeamEnvironmentConfigurationsSection;

            if (teamEnvCfgs != null)
            {
                List<TeamEnvironmentConfig> teamEnvs;
                teamEnvs = (from TeamEnvironmentConfig teamCfg
                            in teamEnvCfgs.TeamEnvironments
                            select teamCfg).ToList();
                var msg = "I see Teams!\r\n";
                msg += "-------------------------\r\n";

                teamEnvs.ForEach(x =>
                {
                    msg += $"Found Team: { x.Team }\r\n";
                    if (x.DeployedEnvironments.Count > 0)
                    {
                        msg += $"\tDeployed To: \r\n";
                        (from DeployedEnvironment deployedEnv
                         in x.DeployedEnvironments
                         select deployedEnv).ToList().ForEach(y =>
                         {
                             msg += $"\t---{ y.Name }\r\n";
                         });
                    } else
                    {
                        msg += "\t(No known deployments)\r\n";
                    }

                });

                MessageBox.Show(msg);
            }
            else
            {
                throw new ConfigurationErrorsException("Failed to read FeatureTeamConfigurations");
            }
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
