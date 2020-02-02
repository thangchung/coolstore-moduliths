using Moduliths.Domain;
using System;
using System.Collections.Generic;

namespace CoolStore.Modules.Localization.Domain
{
    public class Culture : EntityBase<Guid, CultureId>, IAggregateRoot
    {
        public CultureId CultureId => (CultureId)Id;
        public string Name { get; private set; }
        public List<Resource> Resources { get; } = new List<Resource>();

        private Culture() { }

        public static Culture Of(CultureId cultureId, string name)
        {
            return new Culture
            {
                Id = cultureId,
                Name = name
            };
        }

        public Culture AddResource(string key, string value)
        {
            var resource = new Resource(key, value, this);
            Resources.Add(resource);
            return this;
        }
    }
}
