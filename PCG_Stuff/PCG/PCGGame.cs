﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PCG
{

    public class PCGGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public PCGGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

     
        protected override void Initialize()
        {
            

            base.Initialize();
        }

    
        protected override void LoadContent()
        {
          
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }


        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

          

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

          

            base.Draw(gameTime);
        }
    }
}
