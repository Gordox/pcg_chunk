/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here
 * 
 * Visit:
 * For more info or question
 */
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PCG.Manager
{
    static class TextureManager
    {
         public static Texture2D Wall { get; private set; }
         public static Texture2D Floor { get; private set; }
         public static Texture2D Digger { get; private set; }



         public static void LoadContent(ContentManager Content)
         {
             Wall = Content.Load<Texture2D>("Wall");
             Floor = Content.Load<Texture2D>("Floor");
             Digger = Content.Load<Texture2D>("Digger");
         }
    }
}
