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
    class airplane
    {
        private Texture2D plane; 
        private Texture2D[] thrust = new Texture2D[2];
        private Texture2D[] gaz = new Texture2D[2];
        private Texture2D[] brake = new Texture2D[2];

        private Vector2 Pos,PosBack;
        private SpriteFont font;
        public bool isOn;
        private SoundEffectInstance med, max;
        private float scale, k1,k2,angle;
        private int currenState, pedlG,pedlB;
        Double Nuotolis, aukstis;
        public bool  isControl;
        private int index, speed, stepSpeed, speed_index;
//----------------------------------------------------------------------------------------
        public airplane(Game1 game) 
        {
            Skait sk = new Skait();
            scale = sk.loadC()*0.25f;
            plane = game.Content.Load<Texture2D>("Game//mig15-3");

            thrust[0] = game.Content.Load<Texture2D>("Game//mig15-3thurst1");
            thrust[1] = game.Content.Load<Texture2D>("Game//mig15-3thurst2");

            gaz[0] = game.Content.Load<Texture2D>("Game//Gaz");
            gaz[1] = game.Content.Load<Texture2D>("Game//Gaz2");
           
            brake[0] = game.Content.Load<Texture2D>("Game//Brk");
            brake[1] = game.Content.Load<Texture2D>("Game//Brk2");

            med = game.Content.Load<SoundEffect>("Sound//EngineMED").CreateInstance();
            max = game.Content.Load<SoundEffect>("Sound//EngineMAX").CreateInstance();

            font = game.Content.Load<SpriteFont>("font//font");
           
            isOn = true;
           
            isControl = true;
            index = 0;
            currenState=0;
            Pos = new Vector2(TouchPanel.DisplayWidth / 6, TouchPanel.DisplayHeight /2.1f);
            PosBack = new Vector2(TouchPanel.DisplayWidth / 6, TouchPanel.DisplayHeight / 2.1f);
            speed = 200;
            aukstis =60;
            stepSpeed=1;
            speed_index = 0;
          
            k1 = 1.0f;
            k2 = 1.0f;
            pedlB = 0;
            pedlG = 0;
 
        }
        //--------------------------------------------------------------------------------------------
        public void Update(GameTime gametime, Vector3 duom)
        {
            if (isOn == true)
            {
                sound();
                //-----------------------------------------------------------------------
                Nuotolis += speed / 108;
               if(aukstis<=2000) {aukstis += (speed / 108) * Math.Sin(duom.Y); }
               else if (aukstis > 2000 && duom.Y < 0) { aukstis += (speed / 108) * Math.Sin(duom.Y); }
                //-------------------------------------------------------------------
                angle = -duom.Y * 0.5f;
                //-----------------kadravimas----------------
                if (index + 1 > 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                if (TouchPanel.DisplayHeight < Pos.Y || isControl ==false)
                {
                    speed = 0;
                    currenState = 0;
                    if (Pos.Y > TouchPanel.DisplayHeight) { isControl = false; }
                    else { Pos.Y = TouchPanel.DisplayHeight * 2; }
                    if (max.State == SoundState.Playing) { max.Stop(); }
                    if (med.State == SoundState.Playing) { med.Stop(); }
                }
                //---------------------
               
                if (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    if (gs.Position.X < TouchPanel.DisplayWidth / 4 && gs.Position.Y > 2*TouchPanel.DisplayHeight / 3 && gs.GestureType == GestureType.Tap && isControl ==true) 
                    {
                        if (currenState != 2)
                        {
                            currenState = 2;
                            pedlB = 1;
                            pedlG = 0;
                        }
                        else 
                        {
                            currenState = 0;
                            pedlB = 0;
                            pedlG = 0;
                        }
                        
                    }
                    else if (gs.Position.X > 3 * TouchPanel.DisplayWidth / 4 && gs.Position.Y > 2 * TouchPanel.DisplayHeight / 3 && gs.GestureType == GestureType.Tap && isControl == true)
                    {
                        if (currenState != 1)
                        {
                            currenState = 1;
                            pedlG = 1;
                            pedlB = 0;
                        }
                        else
                        {
                            currenState = 0;
                            pedlB = 0;
                            pedlG = 0;
                        }
                    }
                      
                    else
                    {
                        currenState = 0;
                        pedlB = 0;
                        pedlG = 0;

                    }
               }
                
//----------------------------------------------------------------------------------
               
             if (speed_index + 1 > 10)
                {
                    speed_index = 0;

                    if (currenState != 0)
                    {
                        k1 = (float)(((1040 * 2) / speed) * ((1040) / speed));
                        k2 = (float)1040 / k1 + 2;
                    }

                    switch (currenState)
                    {
                        case 0:
                            break;
                        case 1:
                            if ((speed + (int)(stepSpeed * k1)) <= 1045)
                            {
                                speed += (int)(stepSpeed * k1);
                            }
                            break;
                        case 2:
                            if ((speed - (int)(stepSpeed * k2)) >= 30)
                            {
                                speed -= (int)(stepSpeed * k2);
                            }
                            break;
                    }

                    if (duom.Y > 0 && speed <= 1045 && aukstis > 30 && isControl==true)
                    {
                        speed -= (int)Math.Round((Math.Sin(duom.Y) * 1f), 0);
                    }
                    else if(speed<=1045 && aukstis>30 && isControl==true)
                    { 
                        speed += Math.Abs((int)Math.Round((Math.Sin(duom.Y) * 1f), 0)); 
                    }

                    if (speed <= 100)
                    {
                        if (aukstis > 25) { aukstis -= 100 - speed; }
                        else
                        {
                            Pos.Y += 100 - speed;
                        }
                    }

                   
                   if (speed >= 115)
                    {
                        if (Pos.Y >= PosBack.Y)
                        {
                            Pos.Y -= speed - 110;
                        }
                        if (Pos.Y > PosBack.Y) { Pos.Y = PosBack.Y; }
                    }
  
                }
                else 
                {
                    speed_index++;
                }

//-----------------------------------------------------------------------            
            }

        }
//------------------------------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch) 
        {
            if (isOn == true)
            {
                spriteBatch.Draw(plane, Pos, null, Color.White, angle, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(thrust[index], Pos, null, Color.WhiteSmoke, angle, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(brake[pedlB], new Vector2(0,2*TouchPanel.DisplayHeight/3), null, Color.WhiteSmoke,0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(gaz[pedlG], new Vector2(5*TouchPanel.DisplayWidth/6, 2 * TouchPanel.DisplayHeight / 3), null, Color.WhiteSmoke, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);

                if (speed > 120) { spriteBatch.DrawString(font, "Km/h: " + speed.ToString(), new Vector2(0, 0), Color.Black, 0.0f, Vector2.Zero, scale * 1.0f, SpriteEffects.None, 0.0f); }
                else { spriteBatch.DrawString(font, "Km/h: " + speed.ToString(), new Vector2(0, 0), Color.Red, 0.0f, Vector2.Zero, scale * 1.0f, SpriteEffects.None, 0.0f); }

                spriteBatch.DrawString(font, "Nuotolis(m): "+Math.Round(Nuotolis,0).ToString() , new Vector2(0, TouchPanel.DisplayHeight/10), Color.Black, 0.0f, Vector2.Zero, scale * 1.0f, SpriteEffects.None, 0.0f);

                if (aukstis > 50) { spriteBatch.DrawString(font, "Aukštis(m): " + Math.Round(aukstis, 0).ToString(), new Vector2(TouchPanel.DisplayWidth / 2, 0), Color.Black, 0.0f, Vector2.Zero, scale * 1.5f, SpriteEffects.None, 0.0f); }
                else if (aukstis > 30 && 50 > aukstis) { spriteBatch.DrawString(font, "Aukštis(m): " + Math.Round(aukstis, 0).ToString(), new Vector2(TouchPanel.DisplayWidth / 2, 0), Color.Red, 0.0f, Vector2.Zero, scale * 1.5f, SpriteEffects.None, 0.0f); }
                else if(isControl==true) { spriteBatch.DrawString(font, "Leistis draudžiama!" , new Vector2(TouchPanel.DisplayWidth / 2, 0), Color.Red, 0.0f, Vector2.Zero, scale * 1.5f, SpriteEffects.None, 0.0f); }
                else if (isControl == true) { spriteBatch.DrawString(font, "Žaidimas baigtas!!!", new Vector2(TouchPanel.DisplayWidth / 2, 0), Color.Red, 0.0f, Vector2.Zero, scale * 1.5f, SpriteEffects.None, 0.0f); }
               
            }
        }
//-------------------------------------------------------------------------------------------
        public int speedValue() 
        {
            return (speed/10)*3;
        }
//-----------------------------------------------------------------------------------------
        public void OnResume(bool log) 
        {
            isOn = log;
            if (log == false)
            {
                    if (med.State == SoundState.Playing)
                    {
                        med.Pause();
                    }
                    if (max.State == SoundState.Playing)
                    {
                        max.Pause();
                    }
            }
            else 
            {
                if (med.State == SoundState.Paused)
                {
                    med.Resume();
                }
                if (max.State == SoundState.Paused)
                {
                    max.Resume();
                }

            }
        
        }
        //--------------------------------------------------------------------------------------
        public int aukstisValue()
        {
            return (int)aukstis;
        }
        //-----------------------------------------------------------------
        public int nuotolisValue()
        {
            return (int)Nuotolis;
        }
//--------------------------------------------------------------------------------------
        public void On(bool log)
        {
            isOn = log;
            isControl = true;
            index = 0;
            currenState = 0;
            Pos =  new Vector2(TouchPanel.DisplayWidth / 6, TouchPanel.DisplayHeight / 2.1f);
            PosBack = Pos;
            speed = 200;
            aukstis = 60;
            stepSpeed = 1;
            speed_index = 0;

            k1 = 1.0f;
            k2 = 1.0f;
            pedlB = 0;
            pedlG = 0;
 
        }
//-------------------------------------------------------------------------------------
        private void sound() 
        {
            if (currenState == 0 && isControl==true)
            {
                if (max.State == SoundState.Stopped)
                {
                    max.Play();
                }
            }
            else if (currenState == 2 && isControl == true)
            {
                if (med.State == SoundState.Stopped)
                {
                    med.Play();
                }

            }
            else if (currenState == 3 && isControl == true)
            {
                if (med.State == SoundState.Stopped)
                {
                    med.Play();
                }
                if (max.State == SoundState.Stopped)
                {
                    max.Play();
                }

            }
            else if (isControl == true) 
            {
                if (med.State == SoundState.Stopped)
                {
                    med.Play();
                }
                if (max.State == SoundState.Stopped)
                {
                    max.Play();
                }
            }

            
        }
 //-----------------------------------------------
        public void setPos(int difSpeed, int difAukstis) 
        {
            speed -= difSpeed;
            aukstis -= difAukstis;

        }
        public Rectangle getPos()
        {
            return new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)(plane.Width * scale), (int)(plane.Height * scale));
        }
//-------------------------------------------------------------------------------------
  
    }
}
