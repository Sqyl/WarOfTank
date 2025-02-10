using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfTank.Objects
{
    // 不可移动
    internal abstract class Unmoveable : GameObject
    {
        private Image img;
        protected Image Img
        {
            get { return img; }
            set {
                img = value; 
                this.Height = img.Height;
                this.Width = img.Width;
            } 
        }

        protected override Image GetImage()
        {
            return Img;
        }

    }
}
