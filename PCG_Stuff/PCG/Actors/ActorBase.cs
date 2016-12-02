/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here
 * 
 * Visit:
 * For more info or question
 */
using Microsoft.Xna.Framework;
using System;

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
