using Harmony;
using System;
using DiscordHeck;
using System.Reflection;
using DiscordHeck.Gamemodes;

namespace DiscordRichPresence
{
    internal static class Hooks
    {
        public static void ApplyHooks(HarmonyInstance instance)
        {
            instance.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyLib.HarmonyPatch(typeof(LobbyController), "GetSpawnPoints", new Type[0])]
        private static class ChangeDetail
        {
            private static void Postfix(GameController __instance)
            {
                GetGameMode g = new GetGameMode();
                DiscordHeckMain.Presence.state = g.curMode.ToString();
                DiscordHeckMain.Presence.largeImageKey = "defaultlogo";
                DiscordHeckMain.Presence.largeImageText = "SpiderHeck";
                DiscordHeckMain.Presence.smallImageKey = "defaultlogo";
                DiscordHeckMain.Presence.startTimestamp = default(long);
                DiscordRpc.UpdatePresence(DiscordHeckMain.Presence);

                if (g.curMode == GetGameMode.Modes.WASPS)
                {
                    DiscordHeckMain.Presence.details = g.curScene.name + " " + g.curLives + " " + g.curWaves;
                    DiscordHeckMain.Presence.smallImageText = g.curMode.ToString();
                    DiscordRpc.UpdatePresence(DiscordHeckMain.Presence);
                }
                else if (g.curMode == GetGameMode.Modes.VERSUS)
                {
                    DiscordHeckMain.Presence.details = g.curScene.name + " " + g.curScore + " " + g.curLeader;
                    DiscordHeckMain.Presence.smallImageText = g.curMode.ToString();
                    DiscordRpc.UpdatePresence(DiscordHeckMain.Presence);
                }
                else if (g.curMode == GetGameMode.Modes.LOBBY)
                {
                    DiscordHeckMain.Presence.details = "PLAYING SpiderHeck";
                    DiscordHeckMain.Presence.smallImageText = g.curMode.ToString();
                    DiscordHeckMain.Presence.state = "IDLE";
                    DiscordRpc.UpdatePresence(DiscordHeckMain.Presence);
                }
                else if (g.curMode == GetGameMode.Modes.PODIUM)
                {
                    DiscordHeckMain.Presence.details = g.curScene.name + " " + g.curScore + " ";
                    DiscordHeckMain.Presence.smallImageText = g.curMode.ToString();
                    DiscordHeckMain.Presence.state = g.curLeader + "(" + g.leadScore + ")" + " " + "Won";
                    DiscordRpc.UpdatePresence(DiscordHeckMain.Presence);
                }
                else
                {
                    DiscordHeckMain.Presence.details = "PLAYING SpiderHeck";
                    DiscordHeckMain.Presence.smallImageText = g.curMode.ToString();
                    DiscordHeckMain.Presence.state = "IDLE";
                    DiscordRpc.UpdatePresence(DiscordHeckMain.Presence);
                }
            }
        }

    }
}
