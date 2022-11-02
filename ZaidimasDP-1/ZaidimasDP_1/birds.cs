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
    class birds
    {
        private Texture2D[] hawk = new Texture2D[3];
        private Texture2D[] pigeon = new Texture2D[3];
        private Texture2D[] dove = new Texture2D[3];
        private Texture2D[] boom = new Texture2D[5];
        private Rectangle PosPlane;
        private Rectangle birdsR;
        SpriteFont font;
       
        private bool iscontrol;
   
        private Vector2[] BirdPos = new Vector2[9];
        public bool isOn;
        private float scale;
        int bI ,blowIndex;
        private int i,index;

        public birds(Game1 game) 
        {
            hawk[0] = game.Content.Load<Texture2D>("Game//P_21");
            hawk[1] = game.Content.Load<Texture2D>("Game//P_22");
            hawk[2] = game.Content.Load<Texture2D>("Game//P_23");

            pigeon[0] = game.Content.Load<Texture2D>("Game//P_11");
            pigeon[1] = game.Content.Load<Texture2D>("Game//P_12");
            pigeon[2] = game.Content.Load<Texture2D>("Game//P_13");

            dove[0] = game.Content.Load<Texture2D>("Game//P_31");
            dove[1] = game.Content.Load<Texture2D>("Game//P_32");
            dove[2] = game.Content.Load<Texture2D>("Game//P_33");

            boom[0] = game.Content.Load<Texture2D>("Game//1k");
            boom[1] = game.Content.Load<Texture2D>("Game//2k");
            boom[2] = game.Content.Load<Texture2D>("Game//3k");
            boom[3] = game.Content.Load<Texture2D>("Game//4k");
            boom[4] = game.Content.Load<Texture2D>("Game//5k");

            font = game.Content.Load<SpriteFont>("font//font");

            birdsR =new Rectangle(0, 0, hawk[1].Width/2, hawk[1].Height/2);

            BirdPos[0] = new Vector2(-1 * TouchPanel.DisplayWidth/2, 10);
            BirdPos[1] = new Vector2(-1 * TouchPanel.DisplayWidth/3,  TouchPanel.DisplayWidth / 3);
            BirdPos[2] = new Vector2(-1 * TouchPanel.DisplayWidth, 0);
            BirdPos[3] = new Vector2(-1 * TouchPanel.DisplayWidth, 0);
            BirdPos[4] = new Vector2(-1 * TouchPanel.DisplayWidth / 3, 0);
            BirdPos[5] = new Vector2(-1 * TouchPanel.DisplayWidth, 0);
            BirdPos[6] = new Vector2(-3 * TouchPanel.DisplayWidth, 0);
            BirdPos[7] = new Vector2(-2 * TouchPanel.DisplayWidth / 3, 0);
            BirdPos[8] = new Vector2(-2 * TouchPanel.DisplayWidth, 0);

            Skait sk = new Skait();
            scale = sk.loadC() * 1.0f;

            index = 0;
            i=0;
           
            iscontrol = true;

            isOn = true;

        }
        public void Update(GameTime gameTime, Vector3 duom, Rectangle planePos, int aukstis,int nuotolis,int speed) 
        {
            if (isOn == true)
            {
               
                if ((bI + 1) > 4 && (blowIndex+1)<=5 && iscontrol==false)
                {
                    bI = 0;
                    if (blowIndex + 1 > 4) { isOn = false; }
                    else blowIndex++;
                }
                else
                {
                     bI++;
                }
                if (blowIndex >= 6) { isOn = false; }
       //----------------------------------------------------------------------
                if ((i + 1) > 2)
                {
                    i = 0;    
                    if (index + 1 > 2) { index = 0; }  
                    else index++;   
                }
                else
                {
                    i++;
                }
//-------------------------------------------
                for (int j = 0; j<9; j++)
                {
                    if (BirdPos[j].X <  -TouchPanel.DisplayWidth || BirdPos[0].Y < -2.0f * TouchPanel.DisplayHeight || BirdPos[0].Y > 2.0f * TouchPanel.DisplayHeight && iscontrol==true)
                    {
                        if (aukstis > 1000) 
                        {
                            BirdPos[j].X = (int)(3 * TouchPanel.DisplayWidth);
                        }
                        else
                        {
                            BirdPos[j].X = (int)(2 * TouchPanel.DisplayWidth);
                        }
                        BirdPos[j].Y = yValue();
                    }
                    else 
                    {
                        BirdPos[j].X -= (int)(-8+speed/2 * Math.Cos(duom.Y * 0.9f));
                        BirdPos[j].Y += (int)(speed/2 * Math.Sin(duom.Y * 0.7f));
                        birdsR.X=(int)(BirdPos[j].X);
                        birdsR.Y=(int)(BirdPos[j].Y);
                        
                        if (birdsR.Intersects(planePos) == true) 
                        {
                            PosPlane = planePos;
                            iscontrol = false;
                            BirdPos[j].X = (int)(3 * TouchPanel.DisplayWidth);
                           

                        }
                    } 
                }
                
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isOn == true)
            {
                 spriteBatch.Draw(hawk[index], BirdPos[0], null, Color.White, 0f, new Vector2(hawk[index].Width / 2, hawk[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(pigeon[index], BirdPos[1], null, Color.White, 0f, new Vector2(pigeon[index].Width / 2, pigeon[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(dove[index], BirdPos[2], null, Color.White, 0f, new Vector2(dove[index].Width / 2, dove[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(hawk[index], BirdPos[3], null, Color.White, 0f, new Vector2(hawk[index].Width / 2, hawk[index].Height / 2), scale , SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(pigeon[index], BirdPos[4], null, Color.White, 0f, new Vector2(pigeon[index].Width / 2, pigeon[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(dove[index], BirdPos[5], null, Color.White, 0f, new Vector2(dove[index].Width / 2, dove[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(hawk[index], BirdPos[6], null, Color.White, 0f, new Vector2(hawk[index].Width / 2, hawk[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(pigeon[index], BirdPos[7], null, Color.White, 0f, new Vector2(pigeon[index].Width / 2, pigeon[index].Height / 2), scale, SpriteEffects.None, 0.0f);
                 spriteBatch.Draw(dove[index], BirdPos[8], null, Color.White, 0f, new Vector2(dove[index].Width / 2, dove[index].Height / 2), scale, SpriteEffects.None, 0.0f);
            }
        }
        
        private int yValue()
        {
            int Y;
            Random rnd = new Random();
            Y = rnd.Next((int)(-TouchPanel.DisplayHeight/2 ),(int)(2*TouchPanel.DisplayHeight/3));

            return Y;
        }
        public bool isControlValue()
        {
            return iscontrol;
        }
        public void onResume(bool log) 
        {
            isOn = log;
        }
        
    }
}
