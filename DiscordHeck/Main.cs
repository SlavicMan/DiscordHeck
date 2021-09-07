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
        public override void OnApplicationStart()
        {
            var tmode = GetGameMode.instance;
            if (tmode == null)
                tmode = GetGameMode.instance;

#pragma warning disable CS0618 // Type or member is obsolete
            var i = new Harmony.HarmonyInstance("multiheck-rpc");
#pragma warning restore CS0618 // Type or member is obsolete
                              //("multiheck-rpc");
            Hooks.ApplyHooks(i);
            MelonLogger.Msg(ConsoleColor.Magenta, "Starting Mod");
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(DiscordAppID, ref handlers, false, string.Empty);
            Presence.state = string.Empty;
            if (tmode.curMode() == GetGameMode.Modes.VERSUS)
            {
                Presence.details = "In " 
                    + tmode.curMode().ToString() 
                    + " " 
                    + LobbyController.instance.GetPlayerCount(); ;
            }
            else
            {
                Presence.details = "In "
                    + tmode.curMode().ToString()
                    + " " 
                    + LobbyController.instance.GetPlayerCount(); ;
            }

            Presence.startTimestamp = default(long);
            Presence.largeImageKey = "defaultlogo";
            Presence.largeImageText = "SPIDERHECK";
            Presence.smallImageKey = "defaultlogo";
            Presence.smallImageText = tmode.curScene().name;
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
