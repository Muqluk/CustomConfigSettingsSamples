using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

using ConfigurationSectionSamples;

namespace CustomConfigSectionSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //GetDevEnvironmentsMessageBox();
        }

        public void GetDevEnvironmentsMessageBox()
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
                        msg += $"   Deployed To: \r\n";
                        (from DeployedEnvironment deployedEnv
                         in x.DeployedEnvironments
                         select deployedEnv).ToList().ForEach(y =>
                         {
                             msg += $"     ---{ y.Name }\r\n";
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
    }
}
