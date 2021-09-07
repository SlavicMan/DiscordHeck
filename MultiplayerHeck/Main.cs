using MelonLoader;
using DiscordRichPresence;
using DiscordHeck.Gamemodes;
using System;
namespace DiscordHeck
{
    public class DiscordHeckMain : MelonMod
    {
        //private readonly float OnUpdateRoutine = 0f;
        private const string DiscordAppID = "809745341159178250";
        public static readonly DiscordRpc.RichPresence Presence = new DiscordRpc.RichPresence();

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void OnApplicationStart()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            var tMode = new GetGameMode();
            var i = new Harmony.HarmonyInstance("multiheck-rpc");
            //("multiheck-rpc");
            Hooks.ApplyHooks(i);
            MelonLogger.Msg(ConsoleColor.Magenta, "Starting Mod");
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(DiscordAppID, ref handlers, false, string.Empty);
            Presence.state = string.Empty;
            int p;
            var v = tMode.curMode;
            if (tMode.curMode == GetGameMode.Modes.VERSUS)
            {
                p = LobbyController.instance.GetPlayerCount();
                Presence.details = "In " + GetGameMode.Modes.VERSUS.ToString() + " " + p;
            }
            else
            {
                p = LobbyController.instance.GetPlayerCount();
                Presence.details = "In " + v.ToString() + " " + p;
            }

            Presence.startTimestamp = default(long);
            Presence.largeImageKey = "defaultlogo";
            Presence.largeImageText = "SPIDERHECK";
            Presence.smallImageKey = "defaultlogo";
            Presence.smallImageText = tMode.curScene.name;
            DiscordRpc.UpdatePresence(Presence);
        }
        public override void OnUpdate()
        {
            DiscordRpc.RunCallbacks();
        }

        public override void OnApplicationQuit()
        {
            DiscordRpc.Shutdown();
        }
    }
}
