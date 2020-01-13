using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Localization.Domain
{
    public class CultureId : IdentityBase<Guid>
    {
        private CultureId(Guid id) : base(id) { }
        public static explicit operator CultureId(Guid id) => id == Guid.Empty ? null : new CultureId(id);
    }
}
