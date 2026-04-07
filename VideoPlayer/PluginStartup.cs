using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins;
using VideoPlayer.Utilities;

namespace VideoPlayer
{
    public class PluginStartup : ISystemEventsPlugin
    {
        ILog _log;

        public static bool StartupFailed { get; set; }

        public PluginStartup()
        {
            var log4NetConfigPath = Path.Combine(OnlineVideoLinks.Utilities.GeneralUtilities.PluginDirectory, "Log4Net.config");
            var file = new FileInfo(log4NetConfigPath);

            log4net.GlobalContext.Properties["PluginDirectory"] = OnlineVideoLinks.Utilities.GeneralUtilities.PluginDirectory;
            log4net.Config.XmlConfigurator.Configure(file);

            _log = LogManager.GetLogger(nameof(PluginStartup));

            if (!OnlineVideoLinks.Utilities.VlcUtilities.IsVlcInstalled())
            {
                _log.Error("VLC was not found. Turning off plugin.");
                StartupFailed = true;
            }
        }

        public void OnEventRaised(string eventType)
        {
            if (StartupFailed)
                return;

            // On startup, we want to check if Launchbox's VLC distribution has the latest YouTube addon.
            if (eventType == SystemEventTypes.LaunchBoxStartupCompleted
                || eventType == SystemEventTypes.BigBoxStartupCompleted)
            {
                _log.Info("Event raised: " + eventType.ToString());

                try
                {
                    OnlineVideoLinks.Utilities.VlcUtilities.VerifyYoutubeAddon();
                    _log.Info("Verified the Youtube addon for VLC.");

                    var gameVideoUtility = new GameVideoUtility();
                    gameVideoUtility.ValidateVideosForAllGames();
                }
                catch (Exception ex)
                {
                    _log.Error("Startup failed. Turning off plugin.", ex);
                    StartupFailed = true;
                }
            }
        }
    }
}
