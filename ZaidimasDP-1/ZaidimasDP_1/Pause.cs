
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
using ZaidimasDP_1;

namespace zaidimasDP
{
    public class Pause
    {
     private   Texture2D option;
     private   Vector2 optionPos;
        bool isOn = false;
        float scale;

//----------------Konstruktorius-------------------------------------------------------------------------------------
        public Pause(Texture2D newOp, Vector2 Pos,float newScale)
        {
            option = newOp;
            optionPos = Pos;
            scale = newScale;
        }
//----------------Update metodas-------------------------------------------------------------------------------------
       public void update(GameTime gameTime)
        {
           if(TouchPanel.IsGestureAvailable) {
                GestureSample gs = TouchPanel.ReadGesture();
                switch (gs.GestureType)
                {
                    case GestureType.DoubleTap:
                        if (isOn == true)
                        {
                            optionPos = new Vector2(TouchPanel.DisplayWidth / 4, TouchPanel.DisplayHeight / 4);
                            isOn = false;
                        }
                        else { isOn = true; }
                        break;
                    case GestureType.Tap:
                        if ((gs.Position.X >= optionPos.X) && (gs.Position.X <= option.Width + optionPos.X) && (gs.Position.Y >= optionPos.Y) && (gs.Position.Y <= option.Height + optionPos.Y) && isOn == true) 
                        { 
                            isOn = false;
                           // tap();
                        } 
                        break;
                    case GestureType.Hold:
                        optionPos.X = 450;
                        break;
                }
            }

        }
//----------------Draw metodas-------------------------------------------------------------------------------------       
        public void Draw(SpriteBatch spriteBatch)
        {
           if (isOn==true) 
           { 
               spriteBatch.Draw(option, optionPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
           }    
        }
//----Reakcija i Tap--------------------------------------------------------------------------------------------------------------
      public void tap() 
        {
          

 
        } 
//----------------------------------------------------------------------------------------------------------------------------------
      public void destroy() // dar neisbandytas
      {
          isOn = false;
          option = null;
      }
    }
}
