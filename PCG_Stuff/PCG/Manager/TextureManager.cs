using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
