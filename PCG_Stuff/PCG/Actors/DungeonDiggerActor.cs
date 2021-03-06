﻿/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here.
 * 
 * Visit:
 * For more info or question
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PCG.Maps;
using System;
using System.Collections.Generic;

namespace PCG.Actors
{
    class DungeonDiggerActor : ActorBase
    {
        private const int ROOMCHANCEINCREACE = 3;
        private const int DIRCHANCEINCREACE = 5;

        private const int PC_START_VALUE = 5;
        private const int PR_START_VALUE = 5;

        private const int HOWLARGEDUNGEON = 80; //in %
        private int totalRooms;


        //Pc changing dir chance
        //Pr Adding room chance
        private ActorSates.DiggerType diggerType;
        private int Pc, Pr, Fr, Fc;
        //Nc: random chnase for dir
        private Random Nc, Nr, sizeW, sizeH;

        private float timer;
        private const float INTERVAL = .9f;

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
            switch (diggerType)
            {
                case ActorSates.DiggerType.CrazyDigger:
                    Direction = GetRandomDirection;
                    this.Pc = PC_START_VALUE;
                    this.Pr = PR_START_VALUE;
                    this.Nc = new Random();
                    this.Nr = new Random();
                    this.sizeW = new Random(50);
                    this.sizeH = new Random(1258);
                    break;
                case ActorSates.DiggerType.SmartDigger:
                    this.Fc = 0;
                    this.Fr = 0;
                    break;
                default:
                    break;
            }
        }


        public void Update(float gameTime)
        {
            timer += gameTime;

            switch (diggerType)
            {
                case ActorSates.DiggerType.CrazyDigger:
                    UpdateCrazyDigger();
                    StopDigDungeon = EnoughBigDungeon();
                    break;
                case ActorSates.DiggerType.SmartDigger:
                    UpdateSmartDigger();                  
                    break;
                default:
                    break;
            }
            
        }

        private void UpdateSmartDigger()
        {
            if (INTERVAL <= timer && StopDigDungeon == false)
            {
                Fc = Fr = 0;
                CanPlaceRoom();
                CanPlaceCorridor();
                timer = 0;
                StopStuckSmartDigger();
            }  
        }
        private void UpdateCrazyDigger()
        {
            if (INTERVAL <= timer && StopDigDungeon == false)
            {
                CrazyDiggerMovement();
                RandomDirChange();
                RandomRoomCreation();
                timer = 0;
            }
        }


        public void Draw(SpriteBatch SB)
        {
            SB.Draw(texture, Position, Color.White);
        }


        //Methods
        //Crazy digger
        private void CrazyDiggerMovement()
        {
            if ((position / 50 + Direction).X > map.Width - 2 || (position / 50 + Direction).X < 1)
                Direction = GetRandomDirection;
            else if ((position / 50 + Direction).Y > map.Height - 2 || (position / 50 + Direction).Y < 1)
                Direction = GetRandomDirection;
            else
                position += Direction * 50;

            DiggCorridor();
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
                int randomDirection = rnd.Next(0, 100);

                if (randomDirection <= 25)
                    tempDir = new Vector2(-1, 0); // Left

                else if (randomDirection >= 26 && randomDirection <= 50)
                    tempDir = new Vector2(0, -1); // up

                if (randomDirection >= 51 && randomDirection <= 75)
                    tempDir = new Vector2(0, 1); // Down

                else if (randomDirection >= 76 && randomDirection <= 100)
                    tempDir = new Vector2(1, 0); // Right

                return tempDir;
            }
        }
        private bool EnoughBigDungeon()
        {
            int totalSqrWalls = map.Width * map.Height;

            float procent = ((float)totalRooms / (float)totalSqrWalls) * 100;

            if (procent >= HOWLARGEDUNGEON)
                return true;
            else
                return false;
        }

        //Smart digger
        private void SmartDiggerMovement()
        {
            if ((position / 50 + Direction).X > map.Width - 2 || (position / 50 + Direction).X < 1)
                CanPlaceCorridor();
            else if ((position / 50 + Direction).Y > map.Height - 2 || (position / 50 + Direction).Y < 1)
                CanPlaceCorridor();
            else
                position += Direction * 50;

            DiggCorridor();
        }
        private void CanPlaceRoom()
        {
            List<Vector2> roomHolder = new List<Vector2>();
            //check if a room form size 3-7 can be placed
            for (int ySize = 3; ySize < 8; ySize++)
            {
                for (int xSize = 3; xSize < 8; xSize++)
                {
                    if (RoomWontIntersectOtherRooms(xSize+1, ySize+1) == true)
                    {
                        roomHolder.Add(new Vector2(xSize, ySize));
                    }
                }
            }

            if (roomHolder.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, roomHolder.Count - 1);

                DiggRoom((int)roomHolder[index].X, (int)roomHolder[index].Y);
                Fr = 1;
            }
        }
        private bool RoomWontIntersectOtherRooms(int width, int height)
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
                        if (map.Map[x, y].TileType == TileTypes.Room)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private void CanPlaceCorridor()
        {
            List<CombinationHolder> combination = new List<CombinationHolder>();
            Vector2 tempDir = Vector2.Zero;
            for (int dir = 0; dir < 4; dir++)
            {
                for (int size = 3; size < 8; size++)
                {
                    switch (dir)
                    {
                        case 0: // Up    
                            tempDir = new Vector2(0, -1);
                            break;
                        case 1:// Down
                            tempDir = new Vector2(0, 1);
                            break;
                        case 2: // Left
                            tempDir = new Vector2(-1, 0);
                            break;
                        case 3: // Right
                            tempDir = new Vector2(1, 0);
                            break;
                        default:
                            break;
                    }

                    if (CorridorWontIntersect(tempDir, size) == true)
                    {
                        CombinationHolder holder = new CombinationHolder();
                        holder.SetHolder(tempDir, size);
                        combination.Add(holder);
                    }
                }
            }

            if (combination.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, combination.Count - 1);
                DiggCorridor(combination[index].Direction, combination[index].Lenght);
                Fc = 1;
            }



        }
        private bool CorridorWontIntersect(Vector2 dir, int length)
        {
            int roomCount = 0;
            
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            Vector2 temp = new Vector2(posX, posY);
            for (int i = 0; i < length; i++)
            {
                temp += dir;

                if ((int)temp.X < 1 || (int)temp.X > map.Width - 2)
                    return false;
                if ((int)temp.Y < 1 || (int)temp.Y > map.Width - 2)
                    return false;

                if (map.Map[(int)temp.X, (int)temp.Y].TileType == TileTypes.Room)
                    roomCount++;

                if (i == length - 1 && map.Map[(int)temp.X, (int)temp.Y].TileType == TileTypes.Room && roomCount > 2)
                    return false;

                if (map.Map[(int)temp.X, (int)temp.Y].TileType == TileTypes.Corridor)
                    return false;
                else
                    continue;



            }


            if (roomCount == length)
                return false;
            else
                return true;
        }
        private void StopStuckSmartDigger()
        {
            if (Fc == 0 && Fr == 0)
                StopDigDungeon = true;
        }


        //General
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
                        if (map.Map[x, y].TileType == TileTypes.Wall)
                            totalRooms++;

                        map.Map[x, y].TileType = TileTypes.Room;
                       
                    }
                }
            }
        }
        private void DiggCorridor()
        {
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            if (map.Map[posX, posY].TileType == TileTypes.Wall)
            {
                map.Map[posX, posY].TileType = TileTypes.Corridor;
                totalRooms++;
            }
        }
        private void DiggCorridor(Vector2 dir, int length)
        {
            int posX = (int)Position.X / 50;
            int posY = (int)Position.Y / 50;
            Vector2 temp = new Vector2(posX, posY);


            for (int i = 0; i < length; i++)
            {
                temp += dir;

                if ((int)temp.Y < map.Height - 1 && (int)temp.X < map.Width - 1 && (int)temp.X >= 1 && (int)temp.Y >= 1)
                {
                    if (map.Map[(int)temp.X, (int)temp.Y].TileType == TileTypes.Wall)
                    {
                        position = (temp * 50);
                        map.Map[(int)temp.X, (int)temp.Y].TileType = TileTypes.Corridor;
                        totalRooms++;
                    }
                    else
                    {
                        position = (temp * 50);
                    }
                }
                else
                    continue;
            }

        }


        //Other
        
    }
    struct CombinationHolder
    {
        public Vector2 Direction { get; set; }
        public int Lenght { get; set; }

        public void SetHolder(Vector2 dir, int lenght)
        {
            Direction = dir;
            Lenght = lenght;
        }
        public Tuple<Vector2, int> GetCombination()
        {
            return Tuple.Create(Direction, Lenght);
        }
    }
}
