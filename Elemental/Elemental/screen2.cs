﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading.Tasks;

namespace Elemental
{
    internal class screen2 : GameScreen
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private Perso _perso = new Perso();
        private new Game1 Game => (Game1)base.Game;
        public screen2(Game1 game) : base(game) { }
        public override void Initialize()
        {
            _perso.Initialize();
            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("room2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
            _perso.LoadContent(Game);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);

            _perso.Update(gameTime);

        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
            _perso.Draw(Game._spriteBatch);
        }
    }
}
