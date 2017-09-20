using System;
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
                if (value != _featureTeams)
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
                    DeployedEnvironmentsListHeight = (_selectedTeam.DeployedEnvironments != null
                                                        ? _selectedTeam.DeployedEnvironments.Count * 25
                                                        : 25);
                }
            }
        }

        public NestedConfigCollectionViewModel()
        {
            GetFeatureTeamConfigurations();
        }

        private int _deployedEnvironmentsListHeight;
        public int DeployedEnvironmentsListHeight
        {
            get
            {
                return _deployedEnvironmentsListHeight;
            }
            set
            {
                if (value != _deployedEnvironmentsListHeight)
                {
                    _deployedEnvironmentsListHeight = value;
                    RaisePropertyChanged("DeployedEnvironmentsListHeight");
                }
            }
        }

        protected void GetFeatureTeamConfigurations()
        {
            FeatureTeams = new List<FeatureTeam>();
            Configuration envConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;
            TeamEnvironmentConfigurationsSection teamEnvCfgs = envConfig.GetSection("FeatureTeamConfigurations") as TeamEnvironmentConfigurationsSection;

            if (teamEnvCfgs != null)
            {
                FeatureTeam team;
                Model.Environment env;

                (from TeamEnvironmentConfig teamCfg in teamEnvCfgs.TeamEnvironments select teamCfg)
                    .ToList().ForEach(x =>
                    {

                        List<Model.Environment> environments = null;
                        if (x.DeployedEnvironments.Count != 0)
                        {
                            environments = new List<Model.Environment>();
                            (from DeployedEnvironment deployedEnv in x.DeployedEnvironments select deployedEnv)
                                .ToList().ForEach(y =>
                                {
                                    env = new Model.Environment { EnvironmentName = y.Name };
                                    environments.Add(env);
                                });
                        }

                        team = new FeatureTeam
                        {
                            FeatureTeamName = x.Team,
                            SqlDatabaseSuffix = String.IsNullOrEmpty(x.SqlDatabaseSuffix) ? null : x.SqlDatabaseSuffix,
                            WebAppSuffix = String.IsNullOrEmpty(x.WebAppSuffix) ? null : x.WebAppSuffix,
                            DeployedEnvironments = environments == null ? null : environments
                        };
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
