using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Localization.Domain
{
    public class Resource : EntityBase<Guid, ResourceId>
    {
        public ResourceId ResourceId => (ResourceId)Id;
        public string Key { get; private set; }
        public string Value { get; private set; }
        public Culture Culture { get; }

        private Resource() { }

        public Resource(string key, string value, Culture culture)
        {
            Key = key;
            Value = value;
            Culture = culture;
        }
    }
}
