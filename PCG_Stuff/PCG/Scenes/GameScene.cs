/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here
 * 
 * Visit:
 * For more info or question
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PCG.Actors;
using PCG.Manager;
using PCG.Maps;

namespace PCG
{
    class GameScene
    {
        TileMap tileMap;
        DungeonDiggerActor digger;
        private Camera2D camera;

        private const int WIDTH = 20;
        private const int HEIGHT = 20;


        public GameScene(GraphicsDevice GD)
        {
            Init();
            camera = new Camera2D(GD.Viewport);
        }

        private void Init()
        {
            tileMap = new TileMap(WIDTH, HEIGHT);
            digger = new DungeonDiggerActor(TextureManager.Digger, new Vector2(WIDTH / 2 * 50, HEIGHT / 2 * 50), tileMap, ActorSates.DiggerType.SmartDigger);
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
