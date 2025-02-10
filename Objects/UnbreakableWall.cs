using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarOfTank.Properties;

namespace WarOfTank.Objects
{
    internal class UnbreakableWall : Wall
    {
        public UnbreakableWall(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Img = Resources.steel;
        }
    }
}
