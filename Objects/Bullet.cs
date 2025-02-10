using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    enum Tags {
        MyTank, EnemyTank
    }
    internal class Bullet : Moveable
    {
        public Tags Tag { get; set; }
        public bool IsDestroy { get; set; }

        public Bullet(int x, int y, Direction dir, Tags tag)
        {
            this.X = x; this.Y = y;
            this.UpBitmap = Resources.BulletUp;
            this.DownBitmap = Resources.BulletDown;
            this.LeftBitmap = Resources.BulletLeft;
            this.RightBitmap = Resources.BulletRight;
            this.Dir = dir;
            this.Tag = tag;
            this.Speed = 8;
            this.X -= 2;
            this.Y -= 2;

        }

        public override void Update()
        {
            this.MoveCheck();
            this.Move();
            base.Update();
        }
        
        public override void DrawSelf()
        {
            base.DrawSelf();
        }
        protected override void Move()
        {
            switch (Dir)
            {
                case Direction.Up: Y -= Speed; break;
                case Direction.Down: Y += Speed; break;
                case Direction.Left: X -= Speed; break;
                case Direction.Right: X += Speed; break;
            }
        }

        protected override void MoveCheck()
        {
            #region 检查有没有超出窗体边界
            if (this.Dir == Direction.Up)
            {
                if (this.Y < 0) { 
                    IsDestroy = true;
                    return;
                }
            }
            else if (this.Dir == Direction.Down)
            {
                if (this.Y > 360)
                {
                    IsDestroy = true;
                    return;
                }
            }
            if (this.Dir == Direction.Left)
            {
                if (this.X < 0) {
                    IsDestroy = true;
                    return;
                }
            }
            else if (this.Dir == Direction.Right)
            {
                if (this.X > 330) {
                    IsDestroy = true;
                    return;
                }
            }
            #endregion
            // 检查有没有和其他元素发生碰撞
            Rectangle rtNow = this.GetRectangle();
            int xExplosion = this.X + this.Width / 2;
            int yExplosion = this.Y + this.Height / 2;
            /*switch (this.Dir)
            {
                case Direction.Up: rtNow.Y -= Speed; break;
                case Direction.Down: rtNow.Y += Speed; break;
                case Direction.Left: rtNow.X -= Speed; break;
                case Direction.Right: rtNow.X += Speed; break;
            }*/
            Unmoveable wall = null;
            if ((wall = GameObjectManager.IsCollidedWall(rtNow)) != null)
            {
                SoundManager.GameBlast();
                IsDestroy = true;
                GameObjectManager.DestroyBreakableWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                return;
            }
            if ((wall = GameObjectManager.IsCollidedSteel(rtNow)) != null)
            {
                IsDestroy = true;
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                return;
            }

            if (GameObjectManager.IsCollidedBoss(rtNow))
            {
                SoundManager.GameBlast();
                GameFramework.ChangeToGameOver();
                return;
            }

            if (Tag == Tags.MyTank)
            {
                EnemyTank tank = null;
                if((tank = GameObjectManager.IsCollidedEnemyTank(rtNow)) != null)
                {
                    SoundManager.GameBlast();
                    IsDestroy = true;
                    GameObjectManager.DestroyEnemyTank(tank);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    return;
                }
            } else if (Tag == Tags.EnemyTank)
            {
                PlayerTank pTank = null;
                if((pTank = GameObjectManager.IsCollidedPlayerTank(rtNow)) != null)
                {
                    SoundManager.GameHit();
                    pTank.TakeDamage();
                    IsDestroy = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                }
            }
        }

    }
}
