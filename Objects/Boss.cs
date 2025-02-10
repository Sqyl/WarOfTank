using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    internal class Boss : Unmoveable
    {
        public Boss(int x, int y) {
            this.X = x;
            this.Y = y;
            this.Img = Resources.Boss;
        }
    }
}
