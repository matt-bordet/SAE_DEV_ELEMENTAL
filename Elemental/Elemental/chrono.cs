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
    public static class chrono
    {
        public static SpriteFont font;
        private static float _chrono = 0;
        public static Vector2 _positionChrono;

        public static void Initialize()
        {
            _positionChrono = new Vector2(16, 16);
        }
        public static void LoadContent(Game1 game)
        {
        }
        public static void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _chrono += deltaSeconds;
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, Math.Round(_chrono,1).ToString(), _positionChrono, Color.White) ;
            _spriteBatch.End();
        }
    }
}
