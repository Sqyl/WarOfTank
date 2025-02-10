using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    class Target : Unmoveable
    {
        public Target(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Img = Resources.GEMSTAR;
        }
    }
}
