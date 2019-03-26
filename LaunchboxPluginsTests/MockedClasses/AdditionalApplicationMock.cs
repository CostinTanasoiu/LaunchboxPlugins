using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchboxPluginsTests.MockedClasses
{
    public class AdditionalApplicationMock : IAdditionalApplication
    {
        public string Id { get; set; }

        public string GameId { get; set; }

        public int PlayCount { get; set; }
        public string ApplicationPath { get; set; }
        public bool AutoRunAfter { get; set; }
        public bool AutoRunBefore { get; set; }
        public string CommandLine { get; set; }
        public string Name { get; set; }
        public bool UseDosBox { get; set; }
        public bool UseEmulator { get; set; }
        public bool WaitForExit { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Region { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int? Disc { get; set; }
        public string EmulatorId { get; set; }
        public bool SideA { get; set; }
        public bool SideB { get; set; }
        public int Priority { get; set; }

        public System.Drawing.Image GetIconImage(IGame game)
        {
            throw new NotImplementedException();
        }

        public string Launch(IGame game)
        {
            throw new NotImplementedException();
        }
    }
}
