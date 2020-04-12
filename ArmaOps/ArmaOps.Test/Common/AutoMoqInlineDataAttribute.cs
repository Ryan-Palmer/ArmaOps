using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playwaze.Tests.Helpers
{
    public class AutoMoqInlineDataAttribute : InlineAutoDataAttribute
    {
        public AutoMoqInlineDataAttribute(params object[] arguments)
            : base(() => new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true }), arguments)
        {
        }
    }
}
