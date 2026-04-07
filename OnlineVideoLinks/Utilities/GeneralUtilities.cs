using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace OnlineVideoLinks.Utilities
{
    public static class GeneralUtilities
    {
        public readonly static string PluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
