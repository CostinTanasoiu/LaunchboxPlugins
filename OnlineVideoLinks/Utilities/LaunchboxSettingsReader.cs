using OnlineVideoLinks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Unbroken.LaunchBox.Plugins;

namespace OnlineVideoLinks.Utilities
{
    public class LaunchboxSettingsReader
    {
        private static ControllerSettings _controllerSettings;

        public static ControllerSettings ControllerSettings
        {
            get
            {
                if(_controllerSettings == null)
                {
                    var xml = GetSettingsDocument();
                    _controllerSettings = new ControllerSettings();

                    _controllerSettings.ControllerSelectButton =
                        GetNodeIntValue(xml, "LaunchBox/Settings/ControllerSelectButton");

                    _controllerSettings.ControllerBackButton =
                        GetNodeIntValue(xml, "LaunchBox/Settings/ControllerBackButton");
                }

                return _controllerSettings;
            }
        }

        private static XmlDocument GetSettingsDocument()
        {
            var doc = new XmlDocument();

            var filename = PluginHelper.StateManager.IsBigBox
                ? "Data/BigBoxSettings.xml"
                : "Data/Settings.xml";
            doc.Load(filename);
            return doc;
        }

        private static int GetNodeIntValue(XmlDocument xml, string xpath)
        {
            var node = xml.SelectSingleNode("LaunchBox/Settings/ControllerSelectButton");
            int result;
            if (node != null && int.TryParse(node.Value, out result))
                return result;
            return -1;
        }
    }
}
