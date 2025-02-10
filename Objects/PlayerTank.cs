using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    internal class PlayerTank : Tank
    {
        private int focusCnt = 0;
        public bool isMoving { get; set; }
        private int HP { get; set; }
        private int life { get; set; }

        public PlayerTank(int x = 5 * 30, int y = 18 * 15) {
            isMoving = false;
            this.X = x; this.Y = y;
            this.UpBitmap = Resources.MyTankUp;
            this.DownBitmap = Resources.MyTankDown;
            this.LeftBitmap = Resources.MyTankLeft;
            this.RightBitmap = Resources.MyTankRight;
            this.Dir = Direction.Up;
            this.Speed = 1 * 5;
            this.HP = 4;
            this.life = 3;
            
        }

        public void KeyDown(KeyEventArgs e)
        {
            /* 个人优化：当方向没有变化时不做任何更改
             通过增加focus计数来解决切换方向时运动的卡顿问题
               原理：多键同时down时无论哪个键up都会停止运动，
               此时对有效down进行计数，当up时如没有其他down，
               则停止运动；否则沿最后一次down的方向运动
             多线程访问同一对象时会产生资源冲突
            */
            /*
               VITAL: 长按某一按键并松开后坦克将一直移动，
            此时按下其他按键仅有改变方向的作用
             */
            ++focusCnt;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    this.Shoot();break;

                case Keys.W:
                case Keys.Up:
                    isMoving = true;
                    if (this.Dir != Direction.Up)
                                  this.Dir = Direction.Up; break;
                case Keys.S:    
                case Keys.Down:
                    isMoving = true;
                    if (this.Dir != Direction.Down)
                                    this.Dir = Direction.Down; break;
                case Keys.A:
                case Keys.Left:
                    isMoving = true;
                    if (this.Dir != Direction.Left)
                                    Dir = Direction.Left; break;
                case Keys.D:
                case Keys.Right:
                    isMoving = true;
                    if (this.Dir != Direction.Right)
                                     this.Dir = Direction.Right; break;
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            focusCnt = focusCnt > 0 ? --focusCnt : 0;
            if(focusCnt == 0)
                isMoving = false;

        }

        protected override void MoveCheck()
        {
            // 检查有没有超出窗体边界
            if (this.Dir == Direction.Up)
            {
                if (this.Y - this.Speed < 0) { isMoving = false; return; }
            }
            else if (this.Dir == Direction.Down)
            {
                if (this.Y + this.Speed + this.Height > 360) { isMoving = false; return; }
            }
            if (this.Dir == Direction.Left)
            {
                if (this.X - this.Speed < 0) { isMoving = false; return; }
            }
            else if (this.Dir == Direction.Right)
            {
                if (this.X + this.Speed + this.Width > 330) { isMoving = false; return; }
            }
            // 检查有没有和其他元素发生碰撞
            Rectangle rtNow = this.GetRectangle();
            switch (this.Dir)
            {
                case Direction.Up: rtNow.Y -= Speed; break;
                case Direction.Down: rtNow.Y += Speed; break;
                case Direction.Left: rtNow.X -= Speed; break;
                case Direction.Right: rtNow.X += Speed; break;
            }

            if (GameObjectManager.IsCollidedWall(rtNow) != null ||
                GameObjectManager.IsCollidedSteel(rtNow) != null ||
                GameObjectManager.IsCollidedBoss(rtNow))
            {
                isMoving = false;
                return;
            }
        }

        protected override void Move()
        {
            if (!isMoving) return;
            switch (Dir)
            {
                case Direction.Up: Y -= Speed; break;
                case Direction.Down: Y += Speed; break;
                case Direction.Left: X -= Speed; break;
                case Direction.Right: X += Speed; break;
            }
        }

        protected override void Shoot()
        {
            int x = this.X, y = this.Y;
            switch (this.Dir)
            {
                case Direction.Up:
                    x = this.X + this.Width / 2;
                    y = this.Y; break;
                case Direction.Down:
                    x = this.X + this.Width / 2;
                    y = this.Y + this.Height; break;
                case Direction.Left:
                    x = this.X;
                    y = this.Y + this.Height / 2; break;
                case Direction.Right:
                    x = this.X + this.Width;
                    y = this.Y + this.Height / 2; break;
                    default: break;
            }
            GameObjectManager.CreateBullet(
                x, y, this.Dir, Tags.MyTank);
            SoundManager.GameFire();
        }

        public void TakeDamage()
        {
            --this.HP;
            if(this.HP < 1)
            {
                --this.life;
                if (this.life > 0) {
                    this.X = 5 * 30;
                    this.Y = 18 * 15;
                    this.HP = 4;
                } else
                {
                    GameFramework.ChangeToGameOver();
                }
            }
        }

    }
}
