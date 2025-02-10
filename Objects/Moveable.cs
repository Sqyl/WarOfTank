using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfTank.Objects
{

    enum Direction { Left, Right, Up, Down };
    internal class Moveable : GameObject
    {

        private Object _lock = new object();
        protected Bitmap UpBitmap {  get; set; }
        protected Bitmap DownBitmap { get; set; }
        protected Bitmap LeftBitmap { get; set; }
        protected Bitmap RightBitmap { get; set; }
        protected int Speed { get; set; }
        protected virtual void MoveCheck() {
            
        }
        protected virtual void Move() {
            
        }

        private Direction dir;
        protected Direction Dir { get {return dir; }
            set {
                dir = value;
                Bitmap bmp = null;
                switch (dir)
                {
                    case Direction.Up: bmp = UpBitmap; break;
                    case Direction.Down: bmp = DownBitmap; break;
                    case Direction.Left: bmp = LeftBitmap; break;
                    case Direction.Right: bmp = RightBitmap; break;
                    default: break;
                }
                lock (_lock)
                {
                    this.Width = bmp.Width;
                    this.Height = bmp.Height;
                }
            }
        }

        protected override Image GetImage()
        {
            Bitmap map = null;
            switch(Dir)
            {
                case Direction.Left:
                    map = LeftBitmap;break;
                case Direction.Right: 
                    map = RightBitmap;break;
                case Direction.Up: 
                    map = UpBitmap;break;
                case Direction.Down: 
                    map = DownBitmap;break;
            }
            map.MakeTransparent(Color.Black);
            return map;
        }

        public override void DrawSelf()
        {
            lock(_lock)
            {
                base.DrawSelf();
            }
        }
    }
}
