using System;
using System.Configuration;

/*
    The sample below was taken almost in its entirety directly from the MSDN sample for the ConfigurationElementCollection Class.
    https://msdn.microsoft.com/en-us/library/system.configuration.configurationelementcollection(v=vs.110).aspx
    Below is the config section app.config or web.config entry.
    ====================================================================================

	<configSections>
		<section name = "MyUrls" type="ConfigSectionCode.UrlsSection, CustomConfigSectionSample"/>
	</configSections>
	<startup>
		<supportedRuntime version = "v4.0" sku=".NETFramework,Version=v4.5.2" />
	</startup>
	<MyUrls>
		<urls>
			<add name = "first" url="http://yeahright.com" port="4040"/>
		</urls>
	</MyUrls>

    And this is the sample for how to consume the configsection code below.
    ====================================================================================
     private void GetConfig()
        {
            string output;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) as Configuration;
            UrlsSection myUrlsSection = config.GetSection("MyUrls") as UrlsSection;

            if (myUrlsSection == null)
            {
                output = "Failed to load UrlsSection.";
            }
            else
            {
                output = "Collection elements contained in the custom section collection:\r\n";
                for (int i = 0; i < myUrlsSection.Urls.Count; i++)
                {
                    output += $"   Name={myUrlsSection.Urls[i].Name} URL={myUrlsSection.Urls[i].Url} Port={myUrlsSection.Urls[i].Port}\r\n";
                }
            }

        }
*/

namespace ConfigurationSectionSamples
{
    public class UrlsSection : ConfigurationSection
    {
        [ConfigurationProperty("urls", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(UrlsCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public UrlsCollection Urls
        {
            get
            {
                return (UrlsCollection)base["urls"];
            }

        }

        public UrlsSection()
        {
            Urls.Add(new UrlConfigElement());
        }

    }

    public class UrlsCollection : ConfigurationElementCollection
    {
        public UrlsCollection() { }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new UrlConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((UrlConfigElement)element).Name;
        }

        public UrlConfigElement this[int index]
        {
            get
            {
                return (UrlConfigElement)BaseGet(index);
            }
        }

        new public UrlConfigElement this[string Name]
        {
            get
            {
                return (UrlConfigElement)BaseGet(Name);
            }
        }


        public int IndexOf(UrlConfigElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(UrlConfigElement url)
        {
            BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }
    }

    public class UrlConfigElement : ConfigurationElement
    {
        public UrlConfigElement(String name, String url, int port)
        {
            this.Name = name;
            this.Url = url;
            this.Port = port;
        }

        public UrlConfigElement() { }

        [ConfigurationProperty("name", DefaultValue = "Contoso", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("url", DefaultValue = "http://www.contoso.com", IsRequired = true)]
        [RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("port", DefaultValue = (int)4040, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 8080, ExcludeRange = false)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }

    }
}
