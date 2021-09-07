using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace DiscordHeck.Gamemodes
{
    class GetGameMode
    {
        private Scene _curScene;
        public Scene curScene
        {
            get
            {
                return SceneManager.GetActiveScene();
            }
            set
            {
                _curScene = value;
            }
        }
        private Modes _curMode;
        public Modes curMode
        {
            get
            {
                if (WaveMode.instance.GameModeActive())
                    return Modes.WASPS;
                else if (VersusMatchController.matchInProgress)
                    return Modes.VERSUS;
                else if (SceneManager.GetActiveScene().name == "Menu")
                    return Modes.MAINMENU;
                else if (SceneManager.GetActiveScene().name == "Lobby")
                    return Modes.LOBBY;
                return Modes.PODIUM;
            }
            set { _curMode = value; }
        }
        private int _curLives;
        public int curLives
        {
            get
            {
                return WaveMode.instance.lives;
            }
            set { _curLives = value; }
        }
        private int _curWaves;
        public String curWaves
        {
            get
            {
                var t = UnityEngine.GameObject.FindObjectOfType<WaveModeHud>();
                return t.waveCounter.text.ToString();
            }
            set { _curWaves = int.Parse(value); }
        }
        private int _curScore;
        public int curScore
        {
            get
            {
                var tPC = UnityEngine.GameObject.FindObjectOfType<PlayerController>();
                return ScoreKeeper.GetScore(tPC.playerName);
            }
            set { _curScore = value; }
        }
        private PlayerScore _curLeader;
        public PlayerScore curLeader
        {
            get
            {
                return ScoreKeeper.GetLeader();
            }
            set { _curLeader = value; }
        }
        private int _leadScore;
        public int leadScore
        {
            get
            {
                return ScoreKeeper.GetScore(ScoreKeeper.GetLeader().ToString());
            }
            set { _leadScore = value; }
        }
           
        public enum Modes
        {
            MAINMENU,
            LOBBY,
            WASPS,
            VERSUS,
            PODIUM
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
        
        public void CheckMode()
        {

        }
    }
}
