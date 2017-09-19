using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSectionCode
{
    /// <summary>
    /// 
    /// </summary>
    public class SingleElementCollectionSection : ConfigurationSection
    {
        [ConfigurationProperty("SingleCollection", IsRequired = true)]
        [ConfigurationCollection(typeof(SingleCollectionSampleCollection), AddItemName = "SingleElement")]
        public SingleCollectionSampleCollection SingleCollectionElements
        {
            get
            {
                return (SingleCollectionSampleCollection)base["SingleCollection"];
            }
        }
    }

    public class SingleCollectionSampleCollection : ConfigurationElementCollection
    {
        // required by abstract base class
        protected override ConfigurationElement CreateNewElement()
        {
            return new SingleCollectionSampleElement();
        }

        // required by abstract base class
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SingleCollectionSampleElement)element).Name;
        }

        public SingleCollectionSampleElement this[int index]
        {
            get
            {
                return (SingleCollectionSampleElement)BaseGet(index);
            }
        }
    }

    public class SingleCollectionSampleElement : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["Name"];
            }
        }

        [ConfigurationProperty("SampleStringValue1", IsRequired = true)]
        public string SampleStringValue1
        {
            get
            {
                return (string)this["SampleStringValue1"];
            }
        }

        /// <summary>
        /// Also includes a nice integer validator decorator sample.
        /// </summary>
        [ConfigurationProperty("SampleIntValue1", DefaultValue = "636", IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 56000, ExcludeRange = false)]
        public int SampleIntValue1
        {
            get
            {
                return (int)this["SampleIntValue1"];
            }
        }
    }
}
