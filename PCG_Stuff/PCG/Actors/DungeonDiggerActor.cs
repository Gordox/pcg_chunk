using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        ActorSates.DiggerType diggerType;
        int Pc, Pr;
        //Nc: random chnase for dir
        Random Nc, Nr, sizeW, sizeH;

        float timer;

        const float X = 300;
        
        public DungeonDiggerActor(Vector2 startPos, ActorSates.DiggerType diggerType) :
            base(startPos)
        {
            this.Pc = 5;
            this.Pr = 5;
            this.Nc = new Random();
            this.Nr = new Random();
            this.sizeW = new Random();
            this.sizeH = new Random();
            this.diggerType = diggerType;

            Init();
        }

        private void Init()
        {
            Direction = GetRandomDirection;
        }


        public void Update(float gameTime)
        {
            timer += gameTime;

            switch (diggerType)
            {
                case ActorSates.DiggerType.CrazyDigger:
                    //Dig here

                    //
                    if (X >= timer)
                    {
                        if (Nc.Next(0, 100) < Pc)
                        {
                            Direction = GetRandomDirection;
                            Pc = 0;
                        }
                        else
                            Pc += 5;



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
