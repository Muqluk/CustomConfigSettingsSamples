using System;
using System.Configuration;

namespace ConfigSectionCode
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
        
        //public UrlsSection()
        //{
        //    Urls.Add(new UrlConfigElement());
        //}

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

        //public void Add(UrlConfigElement url)
        //{
        //    BaseAdd(url);
        //}

        //protected override void BaseAdd(ConfigurationElement element)
        //{
        //    BaseAdd(element, false);
        //}
    }
    
    public class UrlConfigElement : ConfigurationElement
    {
        //public UrlConfigElement(String name, String url, int port)
        //{
        //    this.Name = name;
        //    this.Url = url;
        //    this.Port = port;
        //}

        public UrlConfigElement() { }

        [ConfigurationProperty("name", DefaultValue = "Contoso", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            //set
            //{
            //    this["name"] = value;
            //}
        }

        [ConfigurationProperty("url", DefaultValue = "http://www.contoso.com", IsRequired = true)]
        [RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            //set
            //{
            //    this["url"] = value;
            //}
        }

        [ConfigurationProperty("port", DefaultValue = (int)4040, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 8080, ExcludeRange = false)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            //set
            //{
            //    this["port"] = value;
            //}
        }

    }

}
