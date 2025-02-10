using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfTank.Objects
{
    abstract internal class GameObject
    {
        protected int X { get; set; }
        protected int Y { get; set; }

        protected int Width { get; set; }
        protected int Height { get; set; }

        abstract protected Image GetImage();
        public virtual void DrawSelf()
        {
            Graphics g = GameFramework.graph;
            g.DrawImage(this.GetImage(), X, Y);
        }

        public virtual void Update() {
            DrawSelf();
        }

        public Rectangle GetRectangle()
        {
            Rectangle r = new Rectangle(this.X, this.Y, this.Width, this.Height);
            return r;
        }
    }
}
