using System.Configuration;

namespace ConfigurationSectionSamples
{
    public class TeamEnvironmentConfigurationsSection : ConfigurationSection
    {
        [ConfigurationProperty("FeatureTeams", IsRequired = true)]
        [ConfigurationCollection(typeof(TeamsEnvironmentCollection), AddItemName = "FeatureTeam")]
        public TeamsEnvironmentCollection TeamEnvironments
        {
            get
            {
                return (TeamsEnvironmentCollection)base["FeatureTeams"];
            }
        }
    }

    public class TeamsEnvironmentCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TeamEnvironmentConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TeamEnvironmentConfig)element).Team;
        }

        public TeamEnvironmentConfig this[int index]
        {
            get
            {
                return (TeamEnvironmentConfig)BaseGet(index);
            }
        }

    }

    public class TeamEnvironmentConfig : ConfigurationElement
    {
        [ConfigurationProperty("Team", IsKey = true, IsRequired = true)]
        public string Team
        {
            get
            {
                return (string)base["Team"];
            }
        }

        [ConfigurationProperty("SqlDatabaseSuffix", DefaultValue = "")]
        public string SqlDatabaseSuffix
        {
            get
            {
                return (string)base["SqlDatabaseSuffix"];
            }
        }

        [ConfigurationProperty("WebAppSuffix", DefaultValue = "")]
        public string WebAppSuffix
        {
            get
            {
                return (string)base["WebAppSuffix"];
            }
        }

        [ConfigurationProperty("DeployedEnvironments")]
        [ConfigurationCollection(typeof(DeployedEnvironmentsCollection), AddItemName = "Environment")]
        public DeployedEnvironmentsCollection DeployedEnvironments
        {
            get
            {
                return (DeployedEnvironmentsCollection)base["DeployedEnvironments"];
            }
        }
    }

    public class DeployedEnvironmentsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DeployedEnvironment();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DeployedEnvironment)element).Name;
        }

        public DeployedEnvironment this[int index]
        {
            get
            {
                return (DeployedEnvironment)BaseGet(index);
            }
        }

    }

    public class DeployedEnvironment : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["Name"];
            }
        }
    }
}
