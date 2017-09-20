using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSectionCode
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
                return (string)base["SqlDatabaseSufix"];
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

        [ConfigurationProperty("Environments")]
        [ConfigurationCollection(typeof(EnvironmentsCollection), AddItemName = "Environment")]
        public EnvironmentsCollection Environments
        {
            get
            {
                return (EnvironmentsCollection)base["Environments"];
            }
        }
    }

    public class EnvironmentsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            throw new NotImplementedException();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            throw new NotImplementedException();
        }
    }

}