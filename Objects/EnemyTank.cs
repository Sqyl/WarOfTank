using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    internal class EnemyTank : Tank
    {
        public int attackSpeed { get; set; }
        private int attackCnt = 0;

        Random r = new Random(); // 种子
        int turnCnt = 0; // 直线移动中的转向计时
        public EnemyTank(int x, int y, int speed,
            Bitmap bmpUp, Bitmap bmpDown, Bitmap bmpLeft, Bitmap bmpRight)
        {
            this.X = x; this.Y = y;
            this.UpBitmap = bmpUp;
            this.DownBitmap = bmpDown;
            this.LeftBitmap = bmpLeft;
            this.RightBitmap = bmpRight;
            this.Dir = Direction.Down;
            this.Speed = speed;
            this.attackSpeed = 30;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            ShootCheck();
            base.Update();
        }

        // 个人方法：改变方向
        protected void ChangeDir()
        {
            int tmp = r.Next(0, 4);
            if (tmp + Direction.Up == this.Dir)
                tmp += r.Next(1, 4);
            tmp %= 4;
            // 使用this.Dir = tmp + Direction.Up时，Dir值传递会失败导致程序崩溃，
            // 使用强制转换就不会有这个问题；
            // 推测原因是tmp原空间是int类型，在进行加法时将枚举变量Up识别成了int类型
            // ，即为0，于是向Dir传递了一个int，而不是Direction，于是导致set时无法
            // 绑定bmp
            this.Dir = (Direction) tmp;
            turnCnt = 0;
            MoveCheck();
        }

        protected override void MoveCheck()
        {
            // 检查有没有超出窗体边界
            if (this.Dir == Direction.Up)
            {
                if (this.Y - this.Speed < 0) { ChangeDir(); return; }
            }
            else if (this.Dir == Direction.Down)
            {
                if (this.Y + this.Speed + this.Height > 360) { ChangeDir(); return; }
            }
            if (this.Dir == Direction.Left)
            {
                if (this.X - this.Speed < 0) { ChangeDir(); return; }
            }
            else if (this.Dir == Direction.Right)
            {
                if (this.X + this.Speed + this.Width > 330) { ChangeDir(); return; }
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
                ChangeDir();
                return;
            }
        }

        protected override void Move()
        {
            // 使敌方坦克能够在直线移动时，每经过一个固定时间就进行一次转向。
            // turnCnt值越大，坦克越不趋向于在直线运动中改变方向，探索率就越低；
            // 反之则趋向于改变方向，进而增加探索率。
            if (turnCnt == 45)
                ChangeDir();
            switch (Dir)
            {
                case Direction.Up: Y -= Speed; break;
                case Direction.Down: Y += Speed; break;
                case Direction.Left: X -= Speed; break;
                case Direction.Right: X += Speed; break;
            }
            ++turnCnt;
        }

        private void ShootCheck()
        {
            ++attackCnt;
            if (attackCnt % attackSpeed == 0)
                Shoot();
            return;
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
                x, y, this.Dir, Tags.EnemyTank);
        }
    }
}
