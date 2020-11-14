using System;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Plugins;
using Impostor.Plugins.Insanity.Handlers;
using Microsoft.Extensions.Logging;

namespace Impostor.Plugins.Insanity
{
    [ImpostorPlugin(
        package: "me.shoraii.insanity",
        name: "Insanity",
        author: "shoraii",
        version: "0.1.0")]
    public class InsanityPlugin : PluginBase
    {
        private readonly ILogger<InsanityPlugin> _logger;
        private readonly IEventManager _eventManager;
        private IDisposable _unregister;
        public InsanityPlugin(ILogger<InsanityPlugin> logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;
        }
        public override ValueTask EnableAsync()
        {
            _logger.LogInformation("Insanity is being enabled.");
            _unregister = _eventManager.RegisterListener(new GameEventListener(_logger));
            return default;
        }
        public override ValueTask DisableAsync()
        {
            _logger.LogInformation("Insanity is being disabled.");
            _unregister.Dispose();
            return default;
        }
    }
}