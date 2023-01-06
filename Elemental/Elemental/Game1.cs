using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Serialization;
using System;

namespace Elemental
{
    public class Game1 : Game
    {       
        private readonly ScreenManager _screenManager;
        public const int TAILLE_TUILE = 16;
        public const int LARGEUR_FENETRE = 38 * 16;
        public const int HAUTEUR_FENETRE = 21 * 16;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
       
        
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            LoadScreen1();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
           
            base.Initialize();
        }
            
        

        protected override void LoadContent()
        {          
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
           
            ushort x=0;
            ushort y=0;
            
            if (keyboardState.IsKeyDown(Keys.E) && mapLayer.GetTile(x,y).GlobalIdentifier==357)
            {
                LoadScreen2();
                
            }
             if (keyboardState.IsKeyDown(Keys.F3))
            {
                LoadScreen3();
            }
             if (keyboardState.IsKeyDown(Keys.F4))
            {
                LoadScreen4();
            }
             if (keyboardState.IsKeyDown(Keys.F5))
            {
                LoadScreen5();
            }
             if (keyboardState.IsKeyDown(Keys.F6))
            {
                LoadScreen6();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);          
           
            base.Draw(gameTime);
        }
        
        private void LoadScreen1()
        {
            _screenManager.LoadScreen(new screen1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen2()
        {
            _screenManager.LoadScreen(new screen2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen3()
        {
            _screenManager.LoadScreen(new screen3(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen4()
        {
            _screenManager.LoadScreen(new screen4(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen5()
        {
            _screenManager.LoadScreen(new screen5(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen6()
        {
            _screenManager.LoadScreen(new screen6(this), new FadeTransition(GraphicsDevice, Color.Black));
        }         
    }
}