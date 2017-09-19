using System;
using System.Configuration;

namespace ConfigSectionCode
{
    public class EnvironmentConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Environments", IsRequired = true)]
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
            return new EnvironmentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentElement)element).Name;
        }

        public EnvironmentElement this[int index]
        {
            get
            {
                return (EnvironmentElement)BaseGet(index);
            }
        }
    }

    public class EnvironmentElement : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["Name"];
            }
        }

        [ConfigurationProperty("LDSServer", IsRequired = true)]
        public string LDSServer
        {
            get
            {
                return (string)this["LDSServer"];
            }
        }

        [ConfigurationProperty("Port", DefaultValue = "636", IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 56000, ExcludeRange = false)]
        public int Port
        {
            get
            {
                return (int)this["Port"];
            }
        }
    }


}
