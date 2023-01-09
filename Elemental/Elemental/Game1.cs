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
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private TiledMapTileLayer mapLayer;
        private Etats etat;
        private ScreenMenu _screenMenu;
        private ScreenSetings _screenSetings;
        private GameScreen _screenPlay1, _screenPlay2, _screenPlay3, _screenPlay4, _screenPlay5, _screenPlay6;

        public enum Etats { Menu, Controls, Play, Quit };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            Etat = Etats.Menu;
            _screenMenu = new ScreenMenu(this);
            _screenPlay1 = new screen1(this);
            _screenPlay2 = new screen2(this);
            _screenPlay3 = new screen3(this);
            _screenPlay4 = new screen4(this);
            _screenPlay5 = new screen5(this);
            _screenPlay6 = new screen6(this);
        }
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }

        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }

        protected override void Initialize()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("salle2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
            LoadMenu();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState _mouseState = Mouse.GetState();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ushort x = 0;
            ushort y = 0;

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play)
                    LoadScreen2();
            }
            if (keyboardState.IsKeyDown(Keys.E) && mapLayer.GetTile(x, y).GlobalIdentifier == 357)
            {
                LoadScreen2();
            }
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Etat = 0;
                LoadMenu();
            }
            else if (keyboardState.IsKeyDown(Keys.F1))
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
            base.Update(gameTime);
            // TODO: Add your update logic here
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        private void LoadMenu()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new ScreenMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen1()
        {
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new screen1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen2()
        {
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new screen2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen3()
        {
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new screen3(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen4()
        {
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new screen4(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen5()
        {
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new screen5(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadScreen6()
        {
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            _screenManager.LoadScreen(new screen6(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private bool IsCollision(ushort x, ushort y)
        {
            Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;

            if (!tile.Value.IsBlank)
            {
                return true;
            }

            return false;

        }
    }
}