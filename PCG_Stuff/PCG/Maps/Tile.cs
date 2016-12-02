/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here
 * 
 * Visit:
 * For more info or question
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PCG.Manager;

namespace PCG.Maps
{
    public enum TileTypes
    {
        Wall,
        Room
    }
    public class Tile
    {
        public Vector2 Position { get; private set; }
        public TileTypes TileType { get; set; }

        public Tile(Vector2 position, TileTypes type = TileTypes.Wall)
        {
            Position = position;
            TileType = type;
        }

        public void Draw(SpriteBatch SB)
        {
            if(TileType == TileTypes.Room)
                SB.Draw(TextureManager.Floor, Position, Color.White);
            else
                SB.Draw(TextureManager.Wall, Position, Color.White);
        }
    }
}
