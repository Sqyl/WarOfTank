using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    class GameObjectManager
    {
        public enum tankType { Gray, Green, Quick, Slow};
        private static List<Unmoveable> wallList = new List<Unmoveable>();
        private static List<Unmoveable> steelList = new List<Unmoveable>();
        private static PlayerTank pTank;
        private static List<EnemyTank> enemyTanks = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static List<Explosion> explosionList = new List<Explosion>();
        private static Boss boss;

        private static int enemyBornSpeed = 58;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];
        public static void Start()
        {
            // 生成敌人
            points[0].X = 0 * 30;
            points[0].Y = 0 * 30;
            points[1].X = 5 * 30;
            points[1].Y = 0 * 30;
            points[2].X = 10 * 30;
            points[2].Y = 0 * 30;
        }

        public static void Update()
        {
            for(int i = 0; i < wallList.Count; ++i)
                wallList[i].Update();
            foreach (Unmoveable um in steelList)
                um.Update();
            foreach (EnemyTank tank in enemyTanks)
                tank.Update();
            CheckAndDestroyBullet();
            CheckAndDestroyExplosion();
            for(int i = 0; i < bulletList.Count; ++i)
                bulletList[i].Update();
            foreach (Explosion explosion in explosionList)
                explosion.Update();
            
            pTank.Update();
            boss.Update();

            EnemyBorn();

        }

        // 生成敌人
        private static void EnemyBorn()
        {
            ++enemyBornCount;
            if (enemyBornCount < enemyBornSpeed) return;
            SoundManager.GameAdd();
            // 随机生成0-2代表随机选中一个位置
            Random rd = new Random();
            // 随机位置在3个位置中选一个
            int index = rd.Next(0, 3);
            Point pos = points[index];
            // 随机类型在4个类型中选一个
            int enemyType = rd.Next(0, 4);
            CreateEnemyTank(pos, tankType.Gray + enemyType);

            enemyBornCount = 0;
        }

        // 创建敌人坦克
        public static void CreateEnemyTank(Point pos, tankType tank_type)
        {
            EnemyTank tank;
            switch (tank_type) {
                case tankType.Gray:
                    tank = new EnemyTank(pos.X, pos.Y, 5,
                        Resources.GrayUp, Resources.GrayDown,
                        Resources.GrayLeft, Resources.GrayRight);
                    enemyTanks.Add(tank); break;
                case tankType.Green:
                    tank = new EnemyTank(pos.X, pos.Y, 5,
                        Resources.GreenUp, Resources.GreenDown,
                        Resources.GreenLeft, Resources.GreenRight);
                    enemyTanks.Add(tank); break;
                case tankType.Quick:
                    tank = new EnemyTank(pos.X, pos.Y, 8,
                        Resources.QuickUp, Resources.QuickDown,
                        Resources.QuickLeft, Resources.QuickRight);
                    enemyTanks.Add(tank); break;
                case tankType.Slow:
                    tank = new EnemyTank(pos.X, pos.Y, 2,
                        Resources.SlowUp, Resources.SlowDown,
                        Resources.SlowLeft, Resources.SlowRight);
                    enemyTanks.Add(tank); break;
                default: break;
            }
        }

        /*public static void DrawGame()
        {
            foreach(Unmoveable um in wallList) 
                um.DrawSelf();
            *//*foreach(Moveable um in moveList)
                um.DrawSelf();*//*
            pTank.DrawSelf();

        }*/

        // 绘制坦克
        public static void CreatePlayerTank()
        {
            PlayerTank playerTank = new PlayerTank();
            pTank = playerTank;
        }
        // 绘制地图
        public static void CreateMap()
        {
            CreateUnbreakableWall(0, 13, 1, wallList); 
            CreateUnbreakableWall(5, 6, 2, wallList);
            CreateUnbreakableWall(10, 12, 2, wallList);
            CreateUnbreakableWall(3, 22, 2, wallList);
            CreateUnbreakableWall(7, 22, 2, wallList);
            CreateBreakableWall(1, 2, 4, wallList);
            CreateBreakableWall(1, 8, 2, wallList);
            CreateBreakableWall(1, 16, 6, wallList);
            CreateBreakableWall(0, 22, 2, wallList);
            CreateBreakableWall(2, 12, 2, wallList);
            CreateBreakableWall(3, 2, 8, wallList);
            CreateBreakableWall(3, 12, 2, wallList);
            CreateBreakableWall(5, 2, 4, wallList);
            CreateBreakableWall(5, 8, 2, wallList);
            CreateBreakableWall(7, 2, 8, wallList);
            CreateBreakableWall(9, 2, 4, wallList);
            CreateBreakableWall(9, 8, 2, wallList);
            //CreateBreakableWall(10, 6, 2, wallList);
            CreateBreakableWall(3, 16, 4, wallList);
            CreateBreakableWall(4, 20, 4, wallList);
            CreateBreakableWall(5, 20, 2, wallList);
            CreateBreakableWall(5, 14, 4, wallList);
            CreateBreakableWall(6, 20, 4, wallList);
            CreateBreakableWall(7, 16, 4, wallList);
            CreateBreakableWall(7, 12, 2, wallList);
            CreateBreakableWall(8, 12, 2, wallList);
            CreateBreakableWall(9, 16, 6, wallList);
            CreateBreakableWall(10, 22, 2, wallList);
            CreateBoss(5, 22, wallList);

        }
        // 通过坐标绘制各种物体
        private static void CreateBreakableWall(int x, int y, int len, List<Unmoveable> wallList)
        {
            for(int i = y * 15; i < (y+len) * 15; i += 15) 
            {
                // 两个位置 i xPos i xPos+15
                Unmoveable wall_lft = new BreakableWall(x * 30, i);
                Unmoveable wall_rht = new BreakableWall(x * 30 + 15, i);
                wallList.Add(wall_lft);
                wallList.Add(wall_rht);
            }
        }

        public static void DestroyBreakableWall(Unmoveable wall)
        {
            wallList.Remove(wall);
        }
        public static void DestroyEnemyTank(EnemyTank tank)
        {
            enemyTanks.Remove(tank);
        }
        private static void CreateUnbreakableWall(int x, int y, int len, List<Unmoveable> wallList)
        {
            for(int i = y * 15; i < (y + len) * 15; i += 15)
            {
                Unmoveable wall_lft = new UnbreakableWall(x * 30, i);
                Unmoveable wall_rht = new UnbreakableWall(x * 30 + 15, i);
                steelList.Add(wall_lft);
                steelList.Add(wall_rht);
            }
        }
        private static void CreateTarget(int x, int y, List<Unmoveable> wallList)
        {
            Unmoveable target = new Target(x * 30, y * 15);
            wallList.Add(target);
        }

        private static void CreateBoss(int x, int y, List<Unmoveable> wallList)
        {
            Boss bs = new Boss(x * 30, y * 15);
            boss = bs;
        }

        public static void CreateBullet(int X, int Y, Direction dir, Tags tag)
        {
            Bullet bullet = new Bullet(X, Y, dir, tag);
            bulletList.Add(bullet);
        }

        public static void CreateExplosion(int X, int Y)
        {
            Explosion explosion = new Explosion(X, Y);
            explosionList.Add(explosion);

        }
        private static void CheckAndDestroyBullet()
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach (Bullet b in bulletList)
            {
                if (b.IsDestroy)
                {
                    needToDestroy.Add(b);
                }
            }
            foreach (Bullet b in needToDestroy)
            {
                bulletList.Remove(b);
            }
        }

        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion b in explosionList)
            {
                if (b.IsDestroy)
                {
                    needToDestroy.Add(b);
                }
            }
            foreach (Explosion b in needToDestroy)
            {
                explosionList.Remove(b);
            }
        }

        // 输入缓冲
        public static void KeyDown(KeyEventArgs e)
        {
            pTank.KeyDown(e);
        }

        public static void KeyUp(KeyEventArgs e)
        {
            pTank.KeyUp(e);
        }

        // 碰撞检测
        public static Unmoveable IsCollidedWall(Rectangle x)
        {
            foreach (Unmoveable wall in wallList)
            {
                if(x.IntersectsWith(wall.GetRectangle())) return wall;
            }
            return null;
        }

        public static Unmoveable IsCollidedSteel(Rectangle x)
        {
            foreach (Unmoveable steel in steelList)
            {
                if (x.IntersectsWith(steel.GetRectangle())) return steel;
            }
            return null;
        }

        public static bool IsCollidedBoss(Rectangle x)
        {
            if (boss.GetRectangle().IntersectsWith(x)) return true;
            else return false;
        }
        public static PlayerTank IsCollidedPlayerTank(Rectangle x)
        {
            if(pTank.GetRectangle().IntersectsWith(x))
                return pTank;
            else return null;
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle x)
        {
            for(int i = 0; i < enemyTanks.Count; ++i)
            {
                if(enemyTanks[i].GetRectangle().IntersectsWith(x))
                {
                    return enemyTanks[i];
                }
            }
            return null;
        }

        
    }   
}
