using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace ZaidimasDP_1
{
    class PauseGame
    {
        private Texture2D blank;
        private SpriteFont font;
        private string Text;
        public int curentState;
        private Vector2 Pos;
        public bool isOn, gameIsOn;
        float scale;
        //-----------------------------------------------------------------------------------------
        public PauseGame(Game1 game)
        {
            blank = game.Content.Load<Texture2D>("AboutMenu//BackgroundA");
            font = game.Content.Load<SpriteFont>("font//font");

            Skait sk = new Skait();
            scale = sk.loadC() * 0.5f;

            Text = "Ar tęsite žaidimą?";
            curentState = 0;

            isOn = false;
            gameIsOn = true;

            Pos = new Vector2(0, 0);
        }
        //------------------------------------------------------------------------------------------
        public void Update(GameTime gameTime)
        {
            if (isOn == true)
            {
                if (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    if (gs.Position.X < TouchPanel.DisplayWidth / 2 && gs.Position.Y > TouchPanel.DisplayHeight / 3)
                    {

                        curentState = 1;
                        isOn = false;
                        gameIsOn = true;
                    }
                    else if ((gs.Position.X > TouchPanel.DisplayWidth / 2 && gs.Position.Y > TouchPanel.DisplayHeight / 3 ))
                    {
                        curentState = 2;
                        isOn = false;
                        gameIsOn = false;
                    }
                }
            }

        }
        //------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isOn == true)
            {
                spriteBatch.Draw(blank, Pos,null, Color.White, 0.0f, Vector2.Zero, scale*3.0f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(font, Text, new Vector2(TouchPanel.DisplayWidth / 6, TouchPanel.DisplayHeight / 3.8f), Color.White, 0.0f, Vector2.Zero, scale * 0.7f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(font, "V", new Vector2(TouchPanel.DisplayWidth / 3.5f, TouchPanel.DisplayHeight / 3.4f), Color.LawnGreen, 0.0f, Vector2.Zero, scale * 2.2f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(font, "X", new Vector2(TouchPanel.DisplayWidth / 1.5f, TouchPanel.DisplayHeight / 3.4f), Color.Red, 0.0f, Vector2.Zero, scale * 2.2f, SpriteEffects.None, 0.0f);
            }
        }
        //------------------------------------------------------------------------------------------
        public void On(bool log)
        {
            isOn = log;
            curentState = 0;
            if (isOn == true)
            {
                gameIsOn = false;
            }
            else if (isOn == false)
            {
                gameIsOn = true;
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}
