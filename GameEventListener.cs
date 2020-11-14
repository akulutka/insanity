using Impostor.Api.Events;
using Impostor.Api.Events.Player;
using Microsoft.Extensions.Logging;
using System;

namespace Impostor.Plugins.Insanity.Handlers
{
    public class GameEventListener : IEventListener
    {
        private readonly ILogger<InsanityPlugin> _logger;

        public GameEventListener(ILogger<InsanityPlugin> logger)
        {
            _logger = logger;
        }

        [EventListener]
        public void OnGameStarted(IGameStartedEvent e)
        {
            _logger.LogInformation($"Game is starting.");

            Array colorValues = Enum.GetValues(typeof(Api.Innersloth.Customization.ColorType));
            Random random = new Random();

            foreach (var player in e.Game.Players)
            {
                player.Character.SetNameAsync("");
                player.Character.SetPetAsync(Api.Innersloth.Customization.PetType.Crewmate);
                player.Character.SetHatAsync(Api.Innersloth.Customization.HatType.NoHat);
                player.Character.SetSkinAsync(Api.Innersloth.Customization.SkinType.None);
                int colIndex = random.Next(colorValues.Length);
                player.Character.SetColorAsync((Api.Innersloth.Customization.ColorType)colorValues.GetValue(colIndex));
            }
        }

        [EventListener]
        public void OnPlayerMurdered(IPlayerMurderEvent e)
        {
            Array colorValues = Enum.GetValues(typeof(Api.Innersloth.Customization.ColorType));
            Random random = new Random();
            foreach (var player in e.Game.Players)
            {
                int colIndex = random.Next(colorValues.Length);
                player.Character.SetColorAsync((Api.Innersloth.Customization.ColorType)colorValues.GetValue(colIndex));
            }
        }

        [EventListener]
        public void OnGameEnded(IGameEndedEvent e)
        {
            _logger.LogInformation($"Game has ended.");
        }

        [EventListener]
        public void OnPlayerChat(IPlayerChatEvent e)
        {
            _logger.LogInformation($"{e.PlayerControl.PlayerInfo.PlayerName} said {e.Message}");
        }
    }
}