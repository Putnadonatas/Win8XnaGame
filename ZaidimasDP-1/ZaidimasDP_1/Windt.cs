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
    public class WindTunnel
    {
        private Texture2D[] frameCr = new Texture2D[5];
        private Texture2D[] frame0 = new Texture2D[5];
        private Texture2D[] frameC = new Texture2D[5];

        private SpriteFont font;

        private Vector2 Pos1, Pos2, Pos3,Pos4, Pos5;
        public short FrameRate, i, index;
        private SoundEffectInstance sound;
        private string text,txt;
        public bool isOn,onV,isLoaded=false;
        public float scale, textScale;

        //----------------Konstruktorius-------------------------------------------------------------------------------------
        public WindTunnel(Game1 game)
        {
            Skait sk = new Skait();
            scale = sk.loadC() * 0.75f;
            textScale = sk.loadC() * 0.25f;

            frameCr[0] = game.Content.Load<Texture2D>("WindW//wC1");
            frameCr[1] = game.Content.Load<Texture2D>("WindW//wC2");
            frameCr[2] = game.Content.Load<Texture2D>("WindW//wC3");
            frameCr[3] = game.Content.Load<Texture2D>("WindW//wC4");
            frameCr[4] = game.Content.Load<Texture2D>("WindW//wC5");


            frame0[0] = game.Content.Load<Texture2D>("WindW//wing_A");
            frame0[1] = game.Content.Load<Texture2D>("WindW//wing_B");
            frame0[2] = game.Content.Load<Texture2D>("WindW//wing_C");
            frame0[3] = game.Content.Load<Texture2D>("WindW//wing_D");
            frame0[4] = game.Content.Load<Texture2D>("WindW//wing_E");

            frameC[0] = game.Content.Load<Texture2D>("WindW//WingnCr1");
            frameC[1] = game.Content.Load<Texture2D>("WindW//WingnCr2");
            frameC[2] = game.Content.Load<Texture2D>("WindW//WingnCr3");
            frameC[3] = game.Content.Load<Texture2D>("WindW//WingnCr4");
            frameC[4] = game.Content.Load<Texture2D>("WindW//WingnCr5");

           font = game.Content.Load<SpriteFont>("font//font");

            

            sound = game.Content.Load<SoundEffect>("Sound//EngineLOW").CreateInstance();
         
       
        
        
        Pos1 =new Vector2(0, 0);
        Pos2 =new Vector2(TouchPanel.DisplayWidth/3,0);
        Pos3 = new Vector2(2 * TouchPanel.DisplayWidth / 3, 0);
        Pos4 = new Vector2(TouchPanel.DisplayWidth / 35, TouchPanel.DisplayHeight /2);
         i = 0;
         index = 1;
         FrameRate = 1;
         isOn = false;
         onV = false;
         text = "Šios animacijos parodo: \n kaip oro srautą veikia sparnai. Kuo oro srauto linijos\n suspaustos labiau tuo stipriau jos stumą sparną nuo\n savęs. \n (Palietus animacijos paveiklą rasite komentarą.)";
         txt = text;
         isLoaded = true;
//55
        }
        //----------------Update metodas-------------------------------------------------------------------------------------
        public void update(GameTime gameTime)
        {
            if (isOn == true)
            {
                if (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                   
                    if ((gs.Position.X > Pos1.X) && (gs.Position.X < Pos2.X) && (gs.Position.Y<TouchPanel.DisplayHeight/2)) 
                    {
                        text = "Kampas tarp sparno ir oro srauto yra pakankamai\nmažas. Norint sukurti didesnę keliamąją jėgą reikia\npadidint kampą tarp sparno ir oro srauto arbą\nlabai stipriai padidinti skriejimo greitį.";
                        onV = true;
                        Pos5 = new Vector2(Pos1.X, Pos1.Y);
                    }
                    else if ((gs.Position.X > Pos2.X) && (gs.Position.X < Pos3.X) && (gs.Position.Y < TouchPanel.DisplayHeight / 2)) 
                    {
                        text = "Tai yra vienas optimaliausių sparno pakreipimo kampų,\nlektuvas yra pakankamai greitas ir manevringas.\n";
                        onV = true;
                        Pos5 = new Vector2(Pos2.X, Pos2.Y);
                    }
                    else if ((gs.Position.X > Pos3.X) && (gs.Position.Y < TouchPanel.DisplayHeight / 2))
                    {
                        text = "Šis kampas dar vadinamas kritiniu kampu, nes nesvarbu\nar didinsite ar mažinsite kampą, keliamoji galia tik mažės.\n";
                        onV = true;
                        Pos5 = new Vector2(Pos3.X, Pos3.Y);
                    }
                    else if (gs.Position.Y > TouchPanel.DisplayHeight / 2 && gs.GestureType == GestureType.DoubleTap) 
                    {
                        text = txt;
                        onV = false;
                    }
                }


                if (i > FrameRate)
                {
                    i = 1;
                    if ((index+1) > 4) { index = 0; }
                    else { index++; }
                }
                i++;

                if (SoundState.Stopped == sound.State) 
                {
                    sound.Play();
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) 
                {
                    sound.Stop();
                    isOn = false;
                    On(isOn);
                }
            }
        }


        //----------------Draw metodas-------------------------------------------------------------------------------------       
        public void Draw(SpriteBatch spriteBatch)
        {

            if (isOn == true)
            {

                        spriteBatch.Draw(frame0[index], Pos1, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                        spriteBatch.Draw(frameC[index], Pos2, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                        spriteBatch.Draw(frameCr[index], Pos3, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                        spriteBatch.DrawString(font, text, Pos4, Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
                        if (onV == true) { spriteBatch.DrawString(font, "V", Pos5, Color.Red, 0.0f, Vector2.Zero, 1.0f*scale, SpriteEffects.None, 0.0f); }
            }
               
        }
        //------------------------------------------
        public void On(bool log) 
        {
            isOn = log;
            Pos1 = new Vector2(0, 0);
            Pos2 = new Vector2(TouchPanel.DisplayWidth / 3, 0);
            Pos3 = new Vector2(2 * TouchPanel.DisplayWidth / 3, 0);
            Pos4 = new Vector2(TouchPanel.DisplayWidth / 35, TouchPanel.DisplayHeight / 2);
            i = 0;
            index = 0;
            FrameRate = 1;
            onV = false;
            text = "Šios animacijos parodo: \n kaip oro srautą veikia sparnai. Kuo oro srauto linijos\n suspaustos labiau tuo stipriau jos stumą sparną nuo\n savęs. \n (Palietus animacijos paveiklą rasite komentarą.)";
            txt = text;
        }
        //-----------------------------------------
         public void Destroy()
        {
         isOn = false;
         
         frameCr[0] = null;
         frameCr[1] = null;
         frameCr[2] = null;
         frameCr[3] = null;
         frameCr[4] = null;


         frame0[0] = null;
         frame0[1] = null;
         frame0[2] = null;
         frame0[3] = null;
         frame0[4] = null;

         frameC[0] = null;
         frameC[1] = null;
         frameC[2] = null;
         frameC[3] = null;
         frameC[4] = null;

         font = null;

         sound = null;
         isLoaded = false;
          
        }
     
        //-----------------------------------------------------------------------------------------------------------------------
    }
}
