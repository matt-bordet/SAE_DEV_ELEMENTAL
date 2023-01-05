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

namespace Elemental
{
    public class Game1 : Game
    {
        
        private readonly ScreenManager _screenManager;
       

        public const int TAILLE_TUILE = 16;
        public const int LARGEUR_FENETRE = 48 * 16;
        public const int HAUTEUR_FENETRE = 21 * 16;
        private int _vitessePerso;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
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
           
            _positionPerso = new Vector2(20, 340);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _vitessePerso = 50;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("Player_IJ_Walk.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("salle2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if ((Keyboard.GetState().IsKeyDown(Keys.D)) && (_positionPerso.X < LARGEUR_FENETRE - 26))
            {
               // _perso.Play("Player_U_Walk");
               // _perso.Update(deltaSeconds);
            }
                if (keyboardState.IsKeyDown(Keys.F1))
            {
                LoadScreen1();
            }
            else if (keyboardState.IsKeyDown(Keys.F2))
            {
                LoadScreen2();
            }
            else if (keyboardState.IsKeyDown(Keys.F3))
            {
                LoadScreen3();
            }
            else if (keyboardState.IsKeyDown(Keys.F4))
            {
                LoadScreen4();
            }
            else if (keyboardState.IsKeyDown(Keys.F5))
            {
                LoadScreen5();
            }
            else if (keyboardState.IsKeyDown(Keys.F6))
            {
                LoadScreen6();
            }
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
            // TODO: Add your update logic here
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();
            // TODO: Add your drawing code here

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