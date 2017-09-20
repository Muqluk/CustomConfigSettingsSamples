using System.Collections.Generic;

namespace CustomConfigSectionSample.Model
{
    public class FeatureTeam
    {
        public string FeatureTeamName { get; set; }

        public string SqlDatabaseSuffix { get; set; }

        public string WebAppSuffix { get; set; }

        public List<Environment> DeployedEnvironments { get; set;}
    }
}
