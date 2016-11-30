using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Maps
{
    public class TileMap
    {
        public const int TILESIZE = 50;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Map { get; private set; }

        public TileMap(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new Tile[Width, Height];
            InitMap();
        }

        public void InitMap()
        {
            for (int y = 0; y < Map.GetLength(1); y++)
            {
                for (int x = 0; x < Map.GetLength(0); x++)
                {
                    //Map[x, y] = new Tile(, new Vector2(x * TILESIZE, y * TILESIZE));
                }
            }
        }


        public void Draw(SpriteBatch SB)
        {
            foreach (Tile t in Map)
            {
                if (t != null)
                    t.Draw(SB);
            }
        }
    }
}
