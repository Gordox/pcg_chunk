﻿/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here
 * 
 * Visit:
 * For more info or question
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PCG.Maps
{
    public class TileMap
    {
        public const int TILESIZE = 50;


        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Map { get; set; }

        public TileMap(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new Tile[Width, Height];
            InitMap();
        }

        private void InitMap()
        {
            for (int y = 0; y < Map.GetLength(1); y++)
            {
                for (int x = 0; x < Map.GetLength(0); x++)
                {
                    Map[x, y] = new Tile(new Vector2(x * TILESIZE, y * TILESIZE));
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
