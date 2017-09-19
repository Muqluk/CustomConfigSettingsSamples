using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSectionCode
{
    public class NestedElementCollectionSection : ConfigurationSection
    {
        [ConfigurationProperty("ParentCollection", IsRequired = true)]
        [ConfigurationCollection(typeof(ParentElementCollection), AddItemName = "ChildCollection")]
        public ParentElementCollection ParentCollection
        {
            get
            {
                return (ParentElementCollection)base["ParentCollection"];
            }
        }

    }
    
    #region Element Collections

    public class ParentElementCollection : ConfigurationElementCollection
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

    public class ChildElementCollectionOne : ConfigurationElementCollection
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

    public class ChildElementCollectionTwo : ConfigurationElementCollection
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

    #endregion

    #region Elements of Child Collections

    public class ChildOneElementOne : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["Name"];
            }
        }

        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)base["Url"];
            }
        }

        [ConfigurationProperty("Port", IsRequired = true)]
        [IntegerValidator(MinValue = 1000, MaxValue = 50000)]
        public int Port
        {
            get
            {
                return (int)base["Port"];
            }
        }

    }

    public class ChildTwoElementOne : ConfigurationElement
    {

    }

    public class ChildTwoElementTwo : ConfigurationElement
    {

    }
#endregion
}
