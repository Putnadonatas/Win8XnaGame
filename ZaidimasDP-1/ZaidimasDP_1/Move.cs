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
    public class Move
    {
       private Texture2D[] ObPhoto= new Texture2D[6];
       private  Vector2[] ObPos = new Vector2[6];
       
       private Texture2D background;
       private float scale, angle, cloudScale;
       private int[] index = new int[6];

       private bool isOn;

        public Move(Game1 game)
        {
            ObPhoto[0]  = game.Content.Load<Texture2D>("Cloud//Cloud_1");
            ObPhoto[1] = game.Content.Load<Texture2D>("Cloud//Cloud_2");
            ObPhoto[2] = game.Content.Load<Texture2D>("Cloud//Cloud_3");
            ObPhoto[3] = game.Content.Load<Texture2D>("Cloud//Cloud_4");
            ObPhoto[4] = game.Content.Load<Texture2D>("Cloud//Cloud_5");
            ObPhoto[5] = game.Content.Load<Texture2D>("Cloud//Cloud_6");

            background = game.Content.Load<Texture2D>("Background//Background_2");
            
            ObPos[0] = new Vector2(1.5f*TouchPanel.DisplayWidth,0);
            ObPos[1] = new Vector2(2*TouchPanel.DisplayWidth/3 , 0);
            ObPos[2] = new Vector2(1*TouchPanel.DisplayWidth, 0);
            ObPos[3] = new Vector2(-3*TouchPanel.DisplayWidth, 0);
            ObPos[4] = new Vector2(-2 * TouchPanel.DisplayWidth / 3, 0);
            ObPos[5] = new Vector2(-2 * TouchPanel.DisplayWidth, 0);

            index[0] = 0;
            index[1] = 0;
            index[2] = 0;
            index[3] = 0;
            index[4] = 0;
            index[5] = 0;

            Skait sk = new Skait();
            scale = sk.loadC() * 1.0f;
            
            cloudScale = sk.loadC() * 0.2f;

            isOn = true;

        }
//--------------------------------------------------------------------------------------------------
        public void Update(GameTime gametime, Vector3 acc,int speed)
        {
            if (isOn == true)
            {
                angle = acc.Y; 
                for (int i = 0; i < 6; i++)
                {

                    if (ObPos[i].X < -1.5f * TouchPanel.DisplayWidth || ObPos[i].Y < -1.5f * TouchPanel.DisplayHeight || ObPos[i].Y > 1.5f * TouchPanel.DisplayHeight)
                    {
                        if (acc.Y > 0.06f) { ObPos[i].X =XValue(); }
                        else if (acc.Y < -0.06f) { ObPos[i].X = XValue(); }
                        else { ObPos[i].X = 2 * TouchPanel.DisplayWidth; }
                        ObPos[i].Y = YValue()*4;
                        index[i] = indexValue();   
                    }
                    else 
                    {
                        ObPos[i].X -= (int)(1+speed * Math.Cos(angle*0.9f));
                        ObPos[i].Y += (int)(speed * Math.Sin(angle *0.7f));
                       
                    }
                }
             
            }

        }
//--------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isOn == true)
            {
                spriteBatch.Draw(background, new Vector2(TouchPanel.DisplayWidth / 2, TouchPanel.DisplayHeight / 2), null, Color.White, angle, new Vector2(background.Width / 2, background.Height / 2), scale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(ObPhoto[index[0]], ObPos[0], null, Color.White, 0f, new Vector2(ObPhoto[index[0]].Width / 2, ObPhoto[index[0]].Height / 2), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(ObPhoto[index[1]], ObPos[1], null, Color.White, 0f, new Vector2(ObPhoto[index[1]].Width / 2, ObPhoto[index[1]].Height / 2), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(ObPhoto[index[2]], ObPos[2], null, Color.White, 0f, new Vector2(ObPhoto[index[2]].Width / 2, ObPhoto[index[2]].Height / 2), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(ObPhoto[index[3]], ObPos[3], null, Color.White, 0f, new Vector2(ObPhoto[index[3]].Width / 2, ObPhoto[index[3]].Height / 2), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(ObPhoto[index[4]], ObPos[4], null, Color.White, 0f, new Vector2(ObPhoto[index[4]].Width / 2, ObPhoto[index[4]].Height / 2), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(ObPhoto[index[5]], ObPos[5], null, Color.White, 0f, new Vector2(ObPhoto[index[5]].Width / 2, ObPhoto[index[5]].Height / 2), cloudScale, SpriteEffects.None, 0.0f);
            }
        }
//--------------------------------------------------------------------------------------------------
     public void OnOff(bool log) 
     { 
         isOn = log;
         
         ObPos[0] = new Vector2(TouchPanel.DisplayWidth, 0);
         ObPos[1] = new Vector2(2 * TouchPanel.DisplayWidth / 3, 0);
         ObPos[2] = new Vector2(2 * TouchPanel.DisplayWidth, 0);
         ObPos[3] = new Vector2(TouchPanel.DisplayWidth, 0);
         ObPos[4] = new Vector2(2 * TouchPanel.DisplayWidth / 3, 0);
         ObPos[5] = new Vector2(2 * TouchPanel.DisplayWidth, 0);

         index[0] = 0;
         index[1] = 0;
         index[2] = 0;
         index[3] = 0;
         index[4] = 0;
         index[5] = 0;
     }
     public void OnResume(bool log)
     {
         isOn = log;

     }
        private int YValue()
        {
            int y;
            int k;
            Random rnd = new Random();

           do{
            k=rnd.Next(-2, 6);
           } while (k==0);

            y = rnd.Next(1, (int)TouchPanel.DisplayHeight / 7) * k; 
            return y;
        }
        private int XValue()
        {
          Random rnd = new Random();
            int X;
            X = rnd.Next((int)TouchPanel.DisplayWidth / 2,TouchPanel.DisplayWidth);
            return X;
        }
         private int indexValue()
         {
             Random rnd = new Random();
             int x;
             x = rnd.Next(6);
             return x;
         }
//--------------------------------------------------------------------------------------------------
     public void Destroy()
     {
         isOn = false;
         ObPhoto = null;
         scale = 0f;
     } 
    }
}

