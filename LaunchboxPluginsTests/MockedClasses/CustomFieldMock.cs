using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchboxPluginsTests.MockedClasses
{
    public class CustomFieldMock : ICustomField
    {
        public string GameId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
