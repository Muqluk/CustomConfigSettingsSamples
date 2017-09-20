using System.Configuration;

/*
----------------------------------------------------------------------------------------------------------------------------------------

The following will need to be added to the web.config or app.config file to consume the code below.
Remember, the <startup> node can only exist once, it's just here to help identify correct locations
for the <configSections> and <SingleElementCollectionSample> nodes.

----------------------------------------------------------------------------------------------------------------------------------------

<configSections>
    <section name = "SingleElementCollectionSample" type="ConfigSectionCode.SingleElementCollectionSection, CustomConfigSectionSample"/>
</configSections>
<startup>
    <supportedRuntime version = "v4.0" sku=".NETFramework,Version=v4.5.2" />
</startup>
<SingleElementCollectionSample>
    <SingleCollection>
        <SingleElement Name = "First Element Name" SampleStringValue1="First String Value"/>
        <SingleElement Name = "Second Element Name" SampleStringValue1="Second String Value" SampleIntValue1="1"/>
        <SingleElement Name = "Third Element Name" SampleStringValue1="Third String Value"/>
    </SingleCollection>
</SingleElementCollectionSample>

----------------------------------------------------------------------------------------------------------------------------------------

Here is an overly simplified example of how to access the configuration settings in the uncommented code below it.

----------------------------------------------------------------------------------------------------------------------------------------        


    public void GetSingleCollectionElements()
    {
        Configuration envConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;

        SingleElementCollectionSection configSection = envConfig.GetSection("SingleElementCollectionSample") as SingleElementCollectionSection;

        if (configSection != null)
        {
            List<SingleCollectionSampleElement> elementsCollection;
            elementsCollection = (from SingleCollectionSampleElement element
                                  in configSection.SingleCollectionElements
                                  select element).ToList();
            var firstElem = elementsCollection.Select(x => x).First();
            MessageBox.Show($"Name: { firstElem.Name }, FirstProp: { firstElem.SampleStringValue1 }, OptionalProp: { firstElem.SampleIntValue1 }");
        }
        else
        {
            throw new System.Exception("Issues with your config idiot.");
        }

    }     

*/
namespace ConfigurationSectionSamples
{
    
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
