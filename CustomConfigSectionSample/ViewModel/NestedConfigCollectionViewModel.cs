using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using GalaSoft;
using GalaSoft.MvvmLight;

using ConfigurationSectionSamples;
using CustomConfigSectionSample.Model;

namespace CustomConfigSectionSample.ViewModel
{
    public class NestedConfigCollectionViewModel : ViewModelBase
    {
        private List<FeatureTeam> _featureTeams;
        public List<FeatureTeam> FeatureTeams
        {
            get
            {
                return _featureTeams;
            }
            set
            {
                if(value != _featureTeams)
                {
                    _featureTeams = value;
                    RaisePropertyChanged("FeatureTeams");
                }
            }
        }

        private FeatureTeam _selectedTeam;
        public FeatureTeam SelectedTeam
        {
            get
            {
                return _selectedTeam;
            }
            set
            {
                if (value != _selectedTeam)
                {
                    _selectedTeam = value;
                    RaisePropertyChanged("SelectedTeam");
                }
            }
        }

        public NestedConfigCollectionViewModel()
        {
            GetFeatureTeamConfigurations();
        }

        protected void GetFeatureTeamConfigurations()
        {
            FeatureTeams = new List<FeatureTeam>();
            Configuration envConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;
            TeamEnvironmentConfigurationsSection teamEnvCfgs = envConfig.GetSection("FeatureTeamConfigurations") as TeamEnvironmentConfigurationsSection;

            if (teamEnvCfgs != null)
            {
                FeatureTeam team;
                Environment env;

                (from TeamEnvironmentConfig teamCfg in teamEnvCfgs.TeamEnvironments select teamCfg)
                    .ToList().ForEach(x =>
                    {
                        team = new FeatureTeam { FeatureTeamName = x.Team, SqlDatabaseSuffix = x.SqlDatabaseSuffix,
                                                    WebAppSuffix = x.WebAppSuffix, DeployedEnvironments = new List<Environment>() };

                        if (x.DeployedEnvironments.Count > 0)
                        {
                            (from DeployedEnvironment deployedEnv in x.DeployedEnvironments select deployedEnv)
                                .ToList().ForEach(y => { env = new Environment { EnvironmentName = y.Name }; });
                        }
                        FeatureTeams.Add(team);
                    });
            }
            else
            {
                throw new ConfigurationErrorsException("Failed to read FeatureTeamConfigurations");
            }
        }

    }
}
