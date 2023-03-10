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
using Microsoft.Xna.Framework.Media;

namespace Elemental
{
    internal class perso
    {
        private int _vitessePerso;
        private float gravite =-1;
        
        private TiledMap _tiledMap;
        public Vector2 _positionPerso;
        
        private AnimatedSprite _perso;
        private Vector2 _saut;
       
        public void Initialize()
        {
            _positionPerso = new Vector2(50, 50);
          
            _saut = new Vector2(0, 20);
            _vitessePerso = 142;
        }
        public  void LoadContent(Game1 game)
        {
            SpriteSheet spriteSheet = game.Content.Load<SpriteSheet>("Player_IJ_Animations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
           
        }
        public  void Update(GameTime gameTime, string obstacleLayerName, TiledMap _tiledMap, Game1 game)
        {
            
            KeyboardState keyboardState = Keyboard.GetState();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            

            if((Keyboard.GetState().IsKeyDown(Keys.D)))
            {
                _perso.Play("Player_IJ_Walk_Right");
                _perso.Update(deltaSeconds);
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>(obstacleLayerName), game))
                    _positionPerso.X += _vitessePerso * deltaSeconds;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                _perso.Play("Player_IJ_Walk_Left");
                _perso.Update(deltaSeconds);

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>(obstacleLayerName), game))
                    _positionPerso.X -= _vitessePerso * deltaSeconds;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Space)))
            {
                bool est_entrain_de_sauter = true;
                _perso.Play("Player_IJ_Jump");
                _perso.Update(deltaSeconds);

                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 0.5);

                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>(obstacleLayerName), game) && est_entrain_de_sauter == true)
                {
                    _perso.Play("Player_IJ_Fall_Constant");
                    tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                    _positionPerso.Y = _positionPerso.Y - gravite;
                    if (IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>(obstacleLayerName), game))
                    {
                        _perso.Play("Player_IJ_Jump");
                        _positionPerso.Y -= _vitessePerso * deltaSeconds;
                        _positionPerso = _positionPerso - _saut;
                        est_entrain_de_sauter = false;
                    }
                }
            }
            else
            {
                _perso.Play("Player_IJ_Idle");
                
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                if (!IsCollision(tx, ty, _tiledMap.GetLayer<TiledMapTileLayer>(obstacleLayerName), game))
                {
                    _positionPerso.Y = _positionPerso.Y - gravite;
                    _perso.Play("Player_IJ_Fall_Constant");
                }
            }
           


        }
        public  void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            
            _spriteBatch.End();
        }
        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer mapLayer, Game1 game)
        {
            Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);
           
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;

            if (!tile.Value.IsBlank)
            {  
                if (mapLayer.GetTile(x, y).GlobalIdentifier == 217)
                {
                    Console.WriteLine("PORTE 1");
                    game.LoadScreen2();
                }

                if (mapLayer.GetTile(x, y).GlobalIdentifier == 357)
                {
                    Console.WriteLine("PORTE 2");
                    game.LoadScreen3();
                }

                if (mapLayer.GetTile(x, y).GlobalIdentifier == 358)
                {
                    Console.WriteLine("PORTE 3");
                    game.LoadScreen5();
                }
                if (mapLayer.GetTile(x, y).GlobalIdentifier==100)
                {
                    Console.WriteLine("fin du jeu");
                }
                //if (mapLayer.GetTile(x, y).GlobalIdentifier == 421|| mapLayer.GetTile(x, y).GlobalIdentifier == 422|| mapLayer.GetTile(x, y).GlobalIdentifier == 423 || mapLayer.GetTile(x, y).GlobalIdentifier == 424)
                //{
                //    game.LoadScreen1();
                //}
                return true;
            }
            return false;
        }
    }
}
