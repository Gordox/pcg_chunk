using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Actors
{
    public class ActorBase
    {
        public Vector2 Position { get { return position; } }
        protected Vector2 position;

        public Vector2 Direction { get; set; }

        public ActorBase(Vector2 position)
        {
            this.position = position;
        }
    }
}
