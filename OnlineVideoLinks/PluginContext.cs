using OnlineVideoLinks.Database;
using OnlineVideoLinks.Forms;
using OnlineVideoLinks.Gamepad;
using OnlineVideoLinks.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVideoLinks
{
    /// <summary>
    /// This is a Singleton that contains instances of the widgets and tools that the plugin will reuse.
    /// </summary>
    public class PluginContext
    {
        private static PluginContext? _instance;
        private static readonly object _lock = new();

        public static PluginContext Instance => _instance ?? throw new InvalidOperationException("PluginContext has not been initialized. Call Initialize() first.");

        public GameVideoUtility VideoUtility { get; }
        public VideoPlayerForm VideoPlayer { get; }
        public GamepadXinputProvider GamepadInput { get; }

        private PluginContext()
        {
            VideoUtility = new GameVideoUtility();
            GamepadInput = new GamepadXinputProvider();
            VideoPlayer = new VideoPlayerForm();
        }

        public static void Initialize()
        {
            if (_instance is not null)
                return;

            lock (_lock)
            {
                _instance ??= new PluginContext();
            }
        }

        public static void Destroy()
        {
            lock (_lock)
            {
                _instance = null;
            }
        }
    }
}
