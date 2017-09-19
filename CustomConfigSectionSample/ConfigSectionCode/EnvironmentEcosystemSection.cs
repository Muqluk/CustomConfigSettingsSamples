using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConfigSectionSample.ConfigSectionCode
{
    public class EnvironmentEcosystemSection : ConfigurationSection
    {
        [ConfigurationProperty("Environments")]
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

    public class Custom : ConfigurationSection
    {
        [ConfigurationProperty("Apps")]
        public AppElementCollection Apps
        {
            get { return this["Apps"] as AppElementCollection; }
        }
    }
    public class AppElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AppElement)element).LocalName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "App"; }
        }

        public AppElement this[int index]
        {
            get { return (AppElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public AppElement this[string employeeID]
        {
            get { return (AppElement)BaseGet(employeeID); }
        }

        public bool ContainsKey(string key)
        {
            bool result = false;
            object[] keys = BaseGetAllKeys();
            foreach (object obj in keys)
            {
                if ((string)obj == key)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
    public class AppElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string LocalName
        {
            get
            {
                return this["path"] as string;
            }
            set
            {
                this["path"] = value;
            }
        }

        [ConfigurationProperty("Methods")]
        public MethodElementCollection Methods
        {
            get
            {
                return this["Methods"] as MethodElementCollection;
            }
        }
    }
    public class MethodElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MethodElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MethodElement)element).LocalName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "Method"; }
        }

        public MethodElement this[int index]
        {
            get { return (MethodElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public MethodElement this[string employeeID]
        {
            get { return (MethodElement)BaseGet(employeeID); }
        }

        public bool ContainsKey(string key)
        {
            bool result = false;
            object[] keys = BaseGetAllKeys();
            foreach (object obj in keys)
            {
                if ((string)obj == key)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
    public class MethodElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string LocalName
        {
            get
            {
                return this["name"] as string;
            }
            set
            {
                this["name"] = value;
            }
        }
    }
}
