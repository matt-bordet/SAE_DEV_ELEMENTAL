using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Serialization;

namespace Elemental
{
    internal class screen4 : GameScreen
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
       
        public Vector2 _positionPerso;
        private perso _perso = new perso();
        private new Game1 Game => (Game1)base.Game;
        
        public screen4(Game1 game) : base(game) { }
        public override void Initialize()
        {
            
            _perso.Initialize();
            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("salle4");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obs4");
            _perso.LoadContent(Game);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            _perso.Update(gameTime, "obs4", _tiledMap, Game);
            chrono.Update(gameTime);
            
        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
             chrono.Draw(Game._spriteBatch);
            _perso.Draw(Game._spriteBatch);
        }
    }
}
