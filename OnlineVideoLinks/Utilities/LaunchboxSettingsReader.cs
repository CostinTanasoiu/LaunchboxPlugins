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
                    _controllerSettings = new ControllerSettings();

                    var xml = GetSettingsDocument();
                    if (xml != null)
                    {
                        _controllerSettings.ControllerSelectButton =
                            GetNodeIntValue(xml, "LaunchBox/Settings/ControllerSelectButton");

                        _controllerSettings.ControllerBackButton =
                            GetNodeIntValue(xml, "LaunchBox/Settings/ControllerBackButton");
                    }
                }

                return _controllerSettings;
            }
        }

        private static string _steamApiKey;
        public static string SteamApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_steamApiKey))
                {
                    var xml = GetSettingsDocument();
                    if (xml != null)
                    {
                        var node = xml.SelectSingleNode("LaunchBox/Settings/ControllerSelectButton");
                        if (node != null)
                            _steamApiKey = node.Value;
                    }
                }
                return _steamApiKey;
            }
        }

        private static XmlDocument GetSettingsDocument()
        {
            var doc = new XmlDocument();

            var filename = PluginHelper.StateManager != null && PluginHelper.StateManager.IsBigBox
                ? "Data/BigBoxSettings.xml"
                : "Data/Settings.xml";
            
            if (File.Exists(filename))
            {
                doc.Load(filename);
                return doc;
            }
            return null;
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
