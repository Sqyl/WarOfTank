using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    internal class Explosion : GameObject
    {
        public bool IsDestroy { get; set; }

        private int displaySpeed = 2;
        private int displayCnt = -1;
        private int index = -1;
        private Bitmap[] explosionBitmap = new Bitmap[] {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };
        public Explosion(int x, int y) {
            foreach(Bitmap bmp in this.explosionBitmap)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - this.explosionBitmap[0].Width / 2;
            this.Y = y - this.explosionBitmap[0].Height / 2;
            this.IsDestroy = false;
        }
        protected override Image GetImage()
        {
            return explosionBitmap[index];
        }

        public override void Update()
        {
            ++displayCnt;
            index = displayCnt / displaySpeed;
            if (index > 3) IsDestroy = true;
            base.Update();
        }
    }
}
