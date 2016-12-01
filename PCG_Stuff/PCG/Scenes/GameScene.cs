using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PCG.Actors;
using PCG.Manager;
using PCG.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG
{
    class GameScene
    {
        TileMap tileMap;
        DungeonDiggerActor digger;
        private Camera2D camera;

        private const int WIDTH = 100;
        private const int HEIGHT = 100;


        public GameScene(GraphicsDevice GD)
        {
            Init();
            camera = new Camera2D(GD.Viewport);
        }

        private void Init()
        {
            tileMap = new TileMap(WIDTH, HEIGHT);
            digger = new DungeonDiggerActor(TextureManager.Digger, new Vector2(WIDTH / 2 * 50, HEIGHT / 2 * 50), tileMap, ActorSates.DiggerType.CrazyDigger);
        }

        public void Update(float gameTime)
        {
            camera.Update();

            digger.Update(gameTime);

            if (KeyMouseReader.KeyPressed(Keys.R))
                Init();
        }

        public void Draw(SpriteBatch SB)
        {
            SB.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            tileMap.Draw(SB);
            digger.Draw(SB);

            SB.End();
        }
    }
}
