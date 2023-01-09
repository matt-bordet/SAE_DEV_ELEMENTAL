using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System.Threading.Tasks;

namespace Elemental
{
        public class ScreenMenu : GameScreen
        {
            // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
            // défini dans Game1
            private Game1 _myGame;

            // texture du menu avec 3 boutons
            private Texture2D _tabMenu;

            // contient les rectangles : position et taille des 3 boutons présents dans la texture 
            private Rectangle[] tabMenu;

            public ScreenMenu(Game1 game) : base(game)
            {
                _myGame = game;
                tabMenu = new Rectangle[3];
                tabMenu[0] = new Rectangle(351, 297, 108, 44);
                tabMenu[1] = new Rectangle(294, 400, 225, 50);
                tabMenu[2] = new Rectangle(340, 503, 132, 51);

            }
            public override void LoadContent()
            {
                _tabMenu = Content.Load<Texture2D>("Title_Elemental");
                base.LoadContent();
            }
            public override void Update(GameTime gameTime)
            {

                MouseState _mouseState = Mouse.GetState();
                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < tabMenu.Length; i++)
                    {
                        // si le clic correspond à un des 3 boutons
                        if (tabMenu[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                        {
                            // on change l'état défini dans Game1 en fonction du bouton cliqué
                            if (i == 0)
                                _myGame.Etat = Game1.Etats.Play;
                            else if (i == 1)
                                _myGame.Etat = Game1.Etats.Controls;
                            else
                                _myGame.Etat = Game1.Etats.Quit;
                            break;
                        }

                    }
                }

            }
            public override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.Black);
                _myGame.SpriteBatch.Begin();
                _myGame.SpriteBatch.Draw(_tabMenu, new Vector2(0, 0), Color.White);
                _myGame.SpriteBatch.End();

            }
        }
}
