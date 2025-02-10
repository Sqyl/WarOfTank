using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarOfTank.Objects;
using WarOfTank.Properties;

namespace WarOfTank
{

    enum GameState { 
        Running,
        GameOver
    }
    internal class GameFramework
    {
        private static GameState gameState = GameState.Running;
        public static Graphics graph;
        public static void Start()
        {
            SoundManager.InitSound();
            GameObjectManager.CreateMap();
            GameObjectManager.CreatePlayerTank();
            GameObjectManager.Start();
            SoundManager.GameStart();
        }

        public static void Update()
        {//调用次数就是FPS
            if(gameState == GameState.Running)
            {
                GameObjectManager.Update();
            } else if (gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }
        private static void GameOverUpdate()
        {
            graph.DrawImage(Resources.GameOver, -90, -50);
        }

        public static void ChangeToGameOver() {
            gameState = GameState.GameOver;
        }
        
        

    }
}
