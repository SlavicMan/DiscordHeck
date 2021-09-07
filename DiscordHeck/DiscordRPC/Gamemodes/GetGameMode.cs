using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using MelonLoader;
using UnityEngine;

namespace DiscordHeck.Gamemodes
{
    class GetGameMode : MelonMod
    {
        public static GetGameMode instance;
        public override void OnApplicationStart()
        {
            GetGameMode.instance = this;
        }
        private Scene _curScene;
        public Scene curScene()
        {
            this._curScene = SceneManager.GetActiveScene();
            return this._curScene;
        }
        private Modes _curMode;
        public Modes curMode()
        {

            if (WaveMode.instance.GameModeActive())
            {
                this._curMode = Modes.WASPS;
                return this._curMode;
            }

            else if (VersusMatchController.matchInProgress)
            {
                this._curMode = Modes.VERSUS;
                return this._curMode;
            }
            else if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                this._curMode = Modes.MAINMENU;
                return this._curMode;
            }
            else if (SceneManager.GetActiveScene().name == "Lobby")
            {
                this._curMode = Modes.LOBBY;
                return this._curMode;
            } else
            {
                this._curMode = Modes.PODIUM;
                return Modes.PODIUM;
            }
        }
        private int _curLives;
        public int curLives()
        {
            this._curLives = WaveMode.instance.lives;
            return this._curLives;
        }
        private string _curWaves;
        public string curWaves()
        {
            this._curWaves = UnityEngine.GameObject.FindObjectOfType<WaveModeHud>().waveCounter.text;
            return this._curWaves;
        }
        private int _curScore;
        public int curScore()
        {
            var tPC = UnityEngine.GameObject.FindObjectOfType<PlayerController>();
            this._curScore = ScoreKeeper.GetScore(tPC.playerName);
            return this._curScore;
            
        }
        private PlayerScore _curLeader;
        public PlayerScore curLeader()
        {
            this._curLeader = ScoreKeeper.GetLeader();
            return this._curLeader;
        }
        private int _leadScore;
        public int leadScore()
        {
            this._leadScore = ScoreKeeper.GetScore(ScoreKeeper.GetLeader().ToString());
            return this._leadScore;
        }
        public bool CheckIfInWasps()
        {
            if (WaveMode.instance.GameModeActive())
                return true;
            else
                return false;
        }
        public bool CheckIfInVersus()
        {
            if (VersusMatchController.matchInProgress)
                return true;
            else
                return false;
        }
           
        public enum Modes
        {
            MAINMENU,
            LOBBY,
            WASPS,
            VERSUS,
            PODIUM
        }
        
        public void CheckMode()
        {

        }
    }
}
