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
    class Autorius
    {
       private Texture2D BackPhoto;
       private SpriteFont font;
       private Vector2 BackPos;
       private SoundEffectInstance Sound;
       private float scale, scaleText;
       public bool isOn;
        public bool isLoaded=false;
       private string text;

//------------------------------------------------------------------------------------------------------------------------
        public Autorius(Game1 game)
        {
            BackPhoto = game.Content.Load<Texture2D>("Background//LEU");
            font = game.Content.Load<SpriteFont>("font//font");

            BackPos = new Vector2(0, 0);
            Skait sk = new Skait();
            scale = sk.loadC();
            scaleText = sk.loadC() * 0.25f;

            Sound = game.Content.Load<SoundEffect>("Sound//Autorius").CreateInstance();
            isOn = false;
            text = "Autorius: \n Donatas Putna \n  Informatikos pedagogika \n       IV kursas \n        VILNIUS";
            isLoaded=true;

        }
        //---------------------------------------------------------------------
        public void Update(GameTime gametime)
        {
            if (isOn == true)
            {
                if (Sound.State==SoundState.Stopped) 
                {
                    
                    Sound.Play();  
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (Sound.State != SoundState.Stopped) 
                { 
                    Sound.Stop(); 
                }
                isOn = false;
                On(isOn);
            }

        }
//-----------------------------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isOn == true)
            {
                spriteBatch.Draw(BackPhoto, BackPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(font, text, new Vector2(TouchPanel.DisplayWidth / 2, TouchPanel.DisplayHeight / 15), Color.Black, 0.0f, Vector2.Zero, scaleText, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(font, "Lietuvos edukologijos universitetas, 2015", new Vector2(TouchPanel.DisplayWidth / 10, 18 * TouchPanel.DisplayHeight / 20), Color.White, 0.0f, Vector2.Zero, scaleText*1.2f, SpriteEffects.None, 0.0f);
            }
           
        }
//--------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------
        public void On(bool log) 
        { 
            isOn = log; 
   
        }
//-----------------------------------------------------
        public void Destroy()
        {
            isOn = false;
            BackPhoto =null;
            font = null;
            isLoaded=false;
        }
//--------------------------------------------------------------------------------------------------
       
    }
}

