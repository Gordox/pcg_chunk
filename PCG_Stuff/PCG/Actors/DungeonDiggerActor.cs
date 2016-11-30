using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PCG.Maps;
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
        private ActorSates.DiggerType diggerType;
        private int Pc, Pr;
        //Nc: random chnase for dir
        private Random Nc, Nr, sizeW, sizeH;

        private float timer;
        private const float INTERVAL = 300;

        private Texture2D texture;
        private TileMap map;
        
        public DungeonDiggerActor(Texture2D texture, Vector2 startPos, TileMap map, ActorSates.DiggerType diggerType) :
            base(startPos)
        {
            this.texture = texture;
            this.map = map;
            this.diggerType = diggerType;
            Init();
        }

        private void Init()
        {
            DiggCorridor();
            Direction = GetRandomDirection;
            this.Pc = 5;
            this.Pr = 5;
            this.Nc = new Random();
            this.Nr = new Random();
            this.sizeW = new Random();
            this.sizeH = new Random();
        }


        public void Update(float gameTime)
        {
            timer += gameTime;

            switch (diggerType)
            {
                case ActorSates.DiggerType.CrazyDigger:
                    if (INTERVAL >= timer)
                    {
                        Movement();
                        RandomDirChange();
                        RandomRoomCreation();
                        timer = 0;
                    }
                    break;
                case ActorSates.DiggerType.SmartDigger:
                    break;
                default:
                    break;
            }
        }


        public void Draw(SpriteBatch SB)
        {

        }


        //Methods
        private void Movement()
        {
            if ((position + Direction).X > map.Width - 1 || (position + Direction).X < 0)
                return;
            else if ((position + Direction).Y > map.Height - 1 || (position + Direction).Y < 0)
                return;
            else
                position += Direction;

            DiggCorridor();
        }
        private void DiggCorridor()
        {
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            map.Map[posX, posY].TileType = TileTypes.Room;
        }
        private void DiggRoom(int width, int height)
        {
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            int sX, sY;

            sY = (posY - height < 0 ? 0 : posY - height);
            sX = (posX - width < 0 ? 0 : posX - width);

            for (int y = sY; y < (sY + height - 1); y++)
            {
                for (int x = sX; x < (sX + width -1); x++)
                {
                    if(y < map.Width || x < map.Height)
                        map.Map[x, y].TileType = TileTypes.Room;
                }
            }
        }
        private void RandomDirChange()
        {
            if (Nc.Next(0, 100) < Pc)
            {
                Direction = GetRandomDirection;
                Pc = 0;
            }
            else
                Pc += 5;
        }
        private void RandomRoomCreation()
        {
            if (Nr.Next(0, 100) < Pr)
            {
                DiggRoom(sizeW.Next(3, 7), sizeH.Next(3, 7));
                Pr = 0;
            }
            else
                Pr += 5;
        }
        private Vector2 GetRandomDirection
        {
            get
            {
                Vector2 tempDir = Vector2.Zero;
                Random rnd = new Random();
                int randomDirection = rnd.Next(4);

                switch (randomDirection)
                {
                    case 1:
                        // Left
                        tempDir = new Vector2(-1, 0);
                        break;
                    case 2:
                        // Right
                        tempDir = new Vector2(1, 0);
                        break;
                    case 3:
                        // Up
                        tempDir = new Vector2(0, -1);
                        break;
                    case 4:
                        // Down
                        tempDir = new Vector2(0, 1);
                        break;
                    default:
                        tempDir = Vector2.Zero;
                        break;
                }
                return tempDir;
            }
        }
    }
}
