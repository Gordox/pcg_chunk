/* Copyright (C) 2016 Anton Svensson (Gordox) - All Rights Reserved
 * You may use, distribute and modify this code. As long this is here
 * 
 * Visit:
 * For more info or question
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PCG.Manager;

namespace PCG
{

    public class PCGGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameScene scene;

        public PCGGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

     
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            base.Initialize();
        }

    
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            scene = new GameScene(GraphicsDevice);

        }


        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyMouseReader.Update();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            scene.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            scene.Draw(spriteBatch);


            base.Draw(gameTime);
        }
    }
}
