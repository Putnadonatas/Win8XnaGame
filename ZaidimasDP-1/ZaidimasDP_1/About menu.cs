using System;
using System.Collections.Generic;
using System.Linq;
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
    class AboutMenu
    {
        private Texture2D menuTex, Pixel, BackTex;
        private SpriteFont Font;
        private Vector2 Pos; 
        private bool isOn,on;
        public bool isLoaded = false;
        private float scale, scaleText, scaleBack;
        private short pasiriktis;
        Color spalva;
        private string Text;
        private short i;
        short p = 0;
        
//-------------------------------------------------------------------------------------------------------------------------------------
        public AboutMenu(Game1 game) 
        {
            menuTex = game.Content.Load<Texture2D>("AboutMenu//AboutMenu");
            BackTex = game.Content.Load<Texture2D>("AboutMenu//BackgroundA");
            Pixel = game.Content.Load<Texture2D>("AboutMenu//clear");
            Font = game.Content.Load<SpriteFont>("font//font");

            Skait sk = new Skait();
            scale = sk.loadC()*0.25f;
            scaleBack = sk.loadC()*1.2f;
            scaleText = sk.loadC() * 0.45f;
            Pos = new Vector2(TouchPanel.DisplayWidth / 4f, TouchPanel.DisplayHeight / 5f);
            isOn = false;
            on = false;
            Text = "";
            pasiriktis=0;
            i = 0;
            isLoaded = true;
            
        }
        //----------------------------------------------------------------------------------------------------------------------------
        
        public void Update(GameTime gametime)
        {
            
            if ((isOn == true) && (on == true)) 
            {
                if (i > 10)
                {
                    i = 0; 
                    pasiriktis = p;
                    on = false;
                    isOn = false;
                }
                else 
                {
                    i++;
                }
            }
            else{
                if (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    if (gs.Position.X <= TouchPanel.DisplayWidth / 5 && gs.Position.Y <= TouchPanel.DisplayHeight / 5)
                    {
                        p = 1;
                        Text = "Vairavimo elementai";
                        on = true;
                        spalva = Color.Green;
                    }
                    else if (gs.Position.X > TouchPanel.DisplayWidth / 5 && gs.Position.X <= TouchPanel.DisplayWidth * 2 / 5 && gs.Position.Y <= TouchPanel.DisplayHeight / 5)
                    {
                        p =2;
                        Text = "Variklis";
                        on = true;
                        spalva = Color.Blue;
                    }
                    else if (gs.Position.X > TouchPanel.DisplayWidth * 2 / 5 && gs.Position.X <= TouchPanel.DisplayWidth * 3 / 5 && gs.Position.Y <= TouchPanel.DisplayHeight / 5)
                    {
                        p = 3;
                        Text = "Sparnai";
                        on = true;
                        spalva = Color.Red;
                    }
                    else if (gs.Position.X > TouchPanel.DisplayWidth * 3 / 5 && gs.Position.X <= TouchPanel.DisplayWidth * 4 / 5 && gs.Position.Y <= TouchPanel.DisplayHeight / 5)
                    {
                        p = 4;
                        Text = "Sparnu borteliai";
                        on = true;
                        spalva = Color.Yellow;

                    }
                    else if (gs.Position.X > TouchPanel.DisplayWidth * 4 / 5 && gs.Position.Y <= TouchPanel.DisplayHeight / 5) 
                    {
                        p = 5;
                        Text = "Sparnų forma";
                        on = true;
                        spalva = Color.Orange;
                    }

                    
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    isOn = false;
                    On(isOn);
                }

            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spritebatch)
        {
            if (isOn == true)
            {
                spritebatch.Draw(BackTex, new Vector2(0,TouchPanel.DisplayHeight/6), null, Color.White, 0f, new Vector2(0, 0), scaleBack, SpriteEffects.None, 0.0f);
                spritebatch.Draw(menuTex,Pos, null, Color.WhiteSmoke, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spritebatch.Draw(Pixel, new Rectangle(0, 0, TouchPanel.DisplayWidth / 5, TouchPanel.DisplayHeight / 5), Color.Green);
                spritebatch.Draw(Pixel, new Rectangle(TouchPanel.DisplayWidth / 5, 0, 2 * TouchPanel.DisplayWidth / 5, TouchPanel.DisplayHeight / 5), Color.Blue);
                spritebatch.Draw(Pixel, new Rectangle(2 * TouchPanel.DisplayWidth / 5,0, 3*TouchPanel.DisplayWidth / 5, TouchPanel.DisplayHeight / 5), Color.Red);
                spritebatch.Draw(Pixel, new Rectangle(3 * TouchPanel.DisplayWidth / 5, 0, 4 * TouchPanel.DisplayWidth / 5, TouchPanel.DisplayHeight / 5), Color.Yellow);
                spritebatch.Draw(Pixel, new Rectangle(4 * TouchPanel.DisplayWidth / 5, 0, 5 * TouchPanel.DisplayWidth / 5, TouchPanel.DisplayHeight / 5), Color.Orange);

                if (on == true)
                {
                        spritebatch.Draw(Pixel, new Rectangle(0, 0, 5 * TouchPanel.DisplayWidth / 5, TouchPanel.DisplayHeight / 5), spalva);
                        spritebatch.DrawString(Font, Text, new Vector2(TouchPanel.DisplayWidth/3, 0), Color.Black, 0f, new Vector2(0, 0), scaleText, SpriteEffects.None, 0.0f);

                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------
        public void On(bool log) 
        { 
            isOn = log;
            on = false;
            Text = "";
            pasiriktis = 0;
            i = 0;
        }
        //---------------------------------------------------------------------------------------------------------------
        public short pasirinktis() { return pasiriktis; }
        //-------------------------------------------------------------
        public bool isON() { return isOn; }
        //----------------------------------------------------------------------------------------------------------------
        public void pasirinktis(short a) 
        { 
            pasiriktis = a; 
        }
        //----------------------------------------------------------------------------------------------------------------
        public void Destroy() // dar neisbandytas
        {
           
            isOn = false;
            on = false;
            menuTex = null;
            Pixel = null;
            pasiriktis = 0;
            Font = null;
            isLoaded = false;
        }
    }
//------------------------------------------------------------------------------------------------------------------------
}
