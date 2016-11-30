
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG.Maps
{
    public enum TileTypes
    {
        Wall,
        Room
    }
    public class Tile
    {
        Texture2D texture;
        public Vector2 Position { get; private set; }
        public TileTypes TileType { get; set; }

        public Tile(Texture2D texture, Vector2 position, TileTypes type = TileTypes.Wall)
        {
            this.texture = texture;
            Position = position;
            TileType = type;
        }

        public void Draw(SpriteBatch SB)
        {
            if(TileType == TileTypes.Room)
                SB.Draw(texture, Position, Color.White);
        }


    }
}
