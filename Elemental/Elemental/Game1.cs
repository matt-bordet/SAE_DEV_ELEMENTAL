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
        private int _vitessePerso;
        private float gravite=-2;
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
           
            _positionPerso = new Vector2(134, 130);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 38 * TAILLE_TUILE;
            _graphics.PreferredBackBufferHeight = 21 * TAILLE_TUILE;
            _graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _vitessePerso = 142;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("salle2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("Player_IJ_Walk.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
           
            if ((Keyboard.GetState().IsKeyDown(Keys.D)))
            {
                _perso.Play("pLayer_U_Walk");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth+0.5);
                ushort ty = (ushort)(_positionPerso.Y /_tiledMap.TileHeight);
                if (!IsCollision(tx, ty))
                    _positionPerso.X += _vitessePerso * deltaSeconds;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                _perso.Play("pLayer_U_Walk");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth-0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty))
                    _positionPerso.X -= _vitessePerso * deltaSeconds;
            }
           
            if ((Keyboard.GetState().IsKeyDown(Keys.Space)))
            {
                _perso.Play("pLayer_U_Walk");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty))
                {
                    tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                    if (IsCollision(tx, ty))
                        _positionPerso.Y -= _vitessePerso * deltaSeconds + 40;
                }
                    
            }
            else
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                if (!IsCollision(tx, ty))
                    _positionPerso.Y = _positionPerso.Y - gravite;
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