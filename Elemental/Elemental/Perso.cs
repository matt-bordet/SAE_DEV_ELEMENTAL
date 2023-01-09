using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    internal class Perso
    {
        private const float MOMENTUM_MAX = 255;
        private const float MOMENTUM_STEP = 2;
        private const float MOMENTUM_JUMP = 200;
        private const float MOMENTUM_GRAVITY = 16;

        private float tractionX = 0;
        private float tractionY = 0;
        private TiledMap _tiledMap;
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        public void Initialize()
        {
            _positionPerso = new Vector2(50, 50);
        }
        public void LoadContent(Game1 game)
        {
            _tiledMap = game.Content.Load<TiledMap>("room2");
            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("Player_IJ_Animations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
        }
        public void Update(GameTime gameTime)
        {
            bool not_on_ground = true;
            _perso.Play("Player_IJ_Idle");

            KeyboardState keyboardState = Keyboard.GetState();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if ((Keyboard.GetState().IsKeyDown(Keys.D)))
            {
                _perso.Play("Player_IJ_Walk_Right");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")))
                {
                    while (tractionX <= MOMENTUM_MAX)
                        tractionX = tractionX + 1 * MOMENTUM_STEP;
                    _positionPerso.X += tractionX *deltaSeconds;
                }
            }
            else if (tractionX > 0 && (Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                _perso.Play("Player_IJ_Walk_Left");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")))
                {
                    while (tractionX >= 0)
                        tractionX = tractionX + -2 * MOMENTUM_STEP;
                    _positionPerso.X += tractionX * deltaSeconds;
                }
            }
            else if (tractionX > 0 && (!Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                _perso.Play("Player_IJ_Walk_Left");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")))
                {
                    while (tractionX >= 0)
                        tractionX = tractionX + -1 * MOMENTUM_STEP;
                    _positionPerso.X += tractionX * deltaSeconds;
                }
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                _perso.Play("Player_IJ_Walk_Left");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")))
                {
                    while (tractionX <= -MOMENTUM_MAX)
                        tractionX = tractionX + -1 * MOMENTUM_STEP;
                    _positionPerso.X += tractionX * deltaSeconds;
                }
            }
            else if (tractionX < 0 && (Keyboard.GetState().IsKeyDown(Keys.D)))
            {
                _perso.Play("Player_IJ_Walk_Left");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")))
                {
                    while (tractionX >= 0)
                        tractionX = tractionX + 2 * MOMENTUM_STEP;
                    _positionPerso.X += tractionX * deltaSeconds;
                }
            }
            else if (tractionX < 0 && (!Keyboard.GetState().IsKeyDown(Keys.D)))
            {
                _perso.Play("Player_IJ_Walk_Left");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")))
                {
                    while (tractionX >= 0)
                        tractionX = tractionX + 1 * MOMENTUM_STEP;
                    _positionPerso.X += tractionX * deltaSeconds;
                }
            }
            while (not_on_ground == false)
            {
                if ((Keyboard.GetState().IsKeyDown(Keys.Space)))
                {
                    _perso.Play("Player_IJ_Jump");
                    _perso.Update(deltaSeconds);
                    ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                    while (tractionY <= MOMENTUM_MAX && tractionY < MOMENTUM_GRAVITY )
                        tractionY = tractionY + MOMENTUM_JUMP;
                    not_on_ground = true;
                    if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")) && 0 < tractionY && tractionY < 34)
                    {
                        _perso.Play("Player_IJ_Fall_Transition");
                        tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                        ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                        _positionPerso.Y = _positionPerso.Y - MOMENTUM_GRAVITY;
                    }
                    else if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles")) && tractionY < 0)
                    {
                        _perso.Play("Player_IJ_Fall_Constant");
                        _positionPerso.Y = _positionPerso.Y - tractionY;
                    }
                    if ((IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>("obstacles"))) && tractionY > 80)
                    {
                        tractionY = 0;
                        _positionPerso.Y -= tractionY * deltaSeconds;

                    }
                }
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();

        }
        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer mapLayer)
        {
            //Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);

            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;

            if (!tile.Value.IsBlank)
            {
                if (mapLayer.GetTile(x, y).GlobalIdentifier == 357)
                    Console.WriteLine("PORTE");
                return true;
            }

            return false;

        }
        public GameTime gameTime 
        {
            get
            {
                return this.gameTime;
            }

            set
            {
                this.gameTime = value;
            }
        }
    }
}

