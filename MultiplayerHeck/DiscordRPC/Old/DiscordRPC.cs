using DiscordRPC;
using System;

namespace MultiplayerHeck.DiscordRPC
{
    class DiscordRPC
    {
        private DiscordRpcClient client;
        private Timestamps Time;
        private GetGameMode tGetModes;
        public void InitializeRPC()
        {
            //Client ID For Images Via Names, Client ID Name (Name Of Tool/Program At Top Of Rich Presence - Clickable
            client = new DiscordRpcClient("809745341159178250");

            //Connect To The Discord App's Rich Presence API
            client.Initialize();
        }
        public void UpdatePresence()
        {
            //Last Two Version Numbers, For Example My One As Of Writing This Is 0.0.2.5
            //string CurrentVersionNumber = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision;
            if(tGetModes == null)
            {
                tGetModes = new GetGameMode();
            }
            if (!tGetModes.CheckIfInVersus() || !tGetModes.CheckIfInWasps())
            {
                if (Time == null)
                {
                    //Only Define Timestamp Once - Fix For Time Resetting To 00:00 Each Update - Only Done Once Project Is Opened And File Is Selected
                    Time = Timestamps.Now;
                }

                if(tGetModes.CheckIfInVersus())
                {
                    MelonLoader.MelonLogger.Msg(ConsoleColor.Magenta, "Player in versus mode");
                    client.SetPresence(new RichPresence()
                    {
                        Details = "Playing " + "Versus",
                        State = "In " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                        Timestamps = Time,
                        Assets = new Assets()
                        {
                            LargeImageKey = "defaultlogo",
                            LargeImageText = "SpiderHeck",
                            SmallImageKey = "defaultlogo",
                            SmallImageText = "SpiderHeck",
                        }
                    });
                } else if (tGetModes.CheckIfInWasps())
                {
                    MelonLoader.MelonLogger.Msg(ConsoleColor.Magenta, "Player in wasps mode");
                    client.SetPresence(new RichPresence()
                    {
                        Details = "Playing " + "Wasps",
                        State = "In " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
                        Timestamps = Time,
                        Assets = new Assets()
                        {
                            LargeImageKey = "defaultlogo",
                            LargeImageText = "SpiderHeck",
                            SmallImageKey = "defaultlogo",
                            SmallImageText = "SpiderHeck",
                        }
                    });
                }
                //Define New RichPresence And Set/Update It
            }
            else
            {
                MelonLoader.MelonLogger.Msg(ConsoleColor.Magenta, "Player at menu or podium");
                //Define New RichPresence And Set/Update It
                client.SetPresence(new RichPresence()
                {
                    Details = "Idle",
                    Assets = new Assets()
                    {
                        LargeImageKey = "defaultlogo",
                        LargeImageText = "SpiderHeck",
                    }
                });
            }
        }
    }
}
