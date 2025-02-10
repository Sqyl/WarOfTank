using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfTank.Objects
{
    abstract internal class Tank : Moveable
    {
        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();
        }
        protected abstract void Shoot();
    }
}
