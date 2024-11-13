using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("SamplePlugin", "Aspire Team", "1.0.0")]
    [Description("Sample Rust Plugin.")]
    class SamplePlugin : CovalencePlugin
    {
        private const string LOG_FLAG = "<------------{ SamplePlugin }------------>";

        private void Init()
        {
            Puts("Initializing SamplePlugin...");
            Puts("Initialized SamplePlugin...");
        }

        private void OnUserConnected(IPlayer player)
        {
            player.Reply("Welcome to the server, " + player.Name + "!");
        }
    }
}