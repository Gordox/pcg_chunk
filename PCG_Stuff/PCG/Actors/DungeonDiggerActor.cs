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
        private const int ROOMCHANCEINCREACE = 3;
        private const int DIRCHANCEINCREACE = 5;

        private const int PC_START_VALUE = 5;
        private const int PR_START_VALUE = 5;

        private const int HOWLARGEDUNGEON = 20; //in %
        private int totalRooms;


        //Pc changing dir chance
        //Pr Adding room chance
        private ActorSates.DiggerType diggerType;
        private int Pc, Pr;
        //Nc: random chnase for dir
        private Random Nc, Nr, sizeW, sizeH;

        private float timer;
        private const float INTERVAL = .0005f;

        private Texture2D texture;
        private TileMap map;

        public bool StopDigDungeon { get; set; }
        
        public DungeonDiggerActor(Texture2D texture, Vector2 startPos, TileMap map, ActorSates.DiggerType diggerType) :
            base(startPos)
        {
            this.texture = texture;
            this.map = map;
            this.diggerType = diggerType;
            StopDigDungeon = false;
            Init();
        }

        private void Init()
        {
            DiggCorridor();
            Direction = GetRandomDirection;
            this.Pc = PC_START_VALUE;
            this.Pr = PR_START_VALUE;
            this.Nc = new Random();
            this.Nr = new Random();
            this.sizeW = new Random(50);
            this.sizeH = new Random(1258);
        }


        public void Update(float gameTime)
        {
            timer += gameTime;

            switch (diggerType)
            {
                case ActorSates.DiggerType.CrazyDigger:
                    if (INTERVAL <= timer && StopDigDungeon == false)
                    {
                        Movement();
                        RandomDirChange();
                        RandomRoomCreation();
                        timer = 0;
                    }

                    StopDigDungeon = EnoughBigDungeon();
                    break;
                case ActorSates.DiggerType.SmartDigger:
                    break;
                default:
                    break;
            }
        }


        public void Draw(SpriteBatch SB)
        {
            SB.Draw(texture, Position, Color.White);
        }


        //Methods
        private void Movement()
        {
            if ((position / 50 + Direction).X > map.Width - 2 || (position / 50 + Direction).X < 1)
                Direction = GetRandomDirection;
            else if ((position / 50 + Direction).Y > map.Height - 2 || (position / 50 + Direction).Y < 1)
                Direction = GetRandomDirection;
            else
                position += Direction * 50;

            DiggCorridor();
        }
        private void DiggCorridor()
        {
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            map.Map[posX, posY].TileType = TileTypes.Room;
            totalRooms++;
        }
        private void DiggRoom(int width, int height)
        {
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            int sX, sY;

            sY = (posY - height / 2 < 1 ? 1 : posY - height / 2);
            sX = (posX - width / 2 < 1 ? 1 : posX - width / 2);

            for (int y = sY; y < (sY + height); y++)
            {
                for (int x = sX; x < (sX + width); x++)
                {
                    if (y < map.Height - 1 && x < map.Width - 1)
                    {
                        map.Map[x, y].TileType = TileTypes.Room;
                        totalRooms++;
                    }
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
                Pc += DIRCHANCEINCREACE;
        }
        private void RandomRoomCreation()
        {
            int nr = Nr.Next(0, 100);
            if (nr < Pr)
            {
                DiggRoom(sizeW.Next(3, 7), sizeH.Next(3, 7));
                Pr = 0;
            }
            else
                Pr += ROOMCHANCEINCREACE;
        }
        private Vector2 GetRandomDirection
        {
            get
            {
                Vector2 tempDir = Vector2.Zero;
                Random rnd = new Random();
                int randomDirection = rnd.Next(0,100);

                if (randomDirection <= 25)
                    tempDir = new Vector2(-1, 0); // Left

                else if (randomDirection >= 26 && randomDirection <= 50)
                    tempDir = new Vector2(0, -1); // up

                if (randomDirection >= 51 && randomDirection <= 75)
                    tempDir = new Vector2(0, 1); // Down

                else if (randomDirection >= 76 && randomDirection <= 100)
                    tempDir = new Vector2(1, 0); // Right
/*
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
 * */
                return tempDir;
            }
        }
        private bool EnoughBigDungeon()
        {
            int totalSqrWalls = map.Width * map.Height;

            float procent = ((float)totalRooms / (float)totalSqrWalls) * 100;

            if (procent>= HOWLARGEDUNGEON)
                return true;
            else
                return false;
        }
    }
}
