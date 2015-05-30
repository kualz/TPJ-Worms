using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_Square.WeaponsAndProjectiles
{
    class Mag
    {
        private int mag;

        public Mag(int mag){
            this.mag = mag;
        }

        public int getMag(){
            return this.mag;
        }

        public void setMag(int number){
            this.mag = number;
        }

        public void decAmmo(){
            this.mag--;
            if (mag <= 0)
                mag = 0;
        }
    }
}
