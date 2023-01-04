using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using System.Threading.Tasks;

namespace Elemental
{
    internal class screen2 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public screen2(Game1 game) : base(game) { }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {

            Game.GraphicsDevice.Clear(new Color(27, 78, 204));
        }
    }
}
