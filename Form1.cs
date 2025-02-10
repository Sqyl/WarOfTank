using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarOfTank.Objects;

namespace WarOfTank
{
    public partial class Form1 : Form
    {
        private Thread GameMainT;
        private static Graphics windowG;
        private static Bitmap tmpBmp;
        public Form1()
        {

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            windowG = this.CreateGraphics();

            tmpBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(tmpBmp);
            GameFramework.graph = bmpG;

            GameMainT = new Thread(new ThreadStart(GameMainThread));
            GameMainT.Start();
        }

        private static void GameMainThread()
        {
            //GameFramework
            GameFramework.Start();
            int sleepTime = 1000 / 60;
            while(true)
            {
                GameFramework.graph.Clear(Color.Black);
                GameFramework.Update(); // 60帧试试
                windowG.DrawImage(tmpBmp, 0, 0);
                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameMainT.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);  

        }



        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }

    }
}
