using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Actors
{
    class DungeonDiggerActor : ActorBase
    {
        //Pc changing dir chance
        //Pr Adding room chance
        int Pc, Pr;
        Random Nc, Nr, sizeW, sizeH;
        public DungeonDiggerActor(Vector2 position) : 
            base(position)
        {
            this.Pc = 5;
            this.Pr = 5;
            this.Nc = new Random();
            this.Nr = new Random();
            this.sizeW = new Random();
            this.sizeH = new Random();
        }


        public void Update(float gameTime)
        {

        }
    }
}
