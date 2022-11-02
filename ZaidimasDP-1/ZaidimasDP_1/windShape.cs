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
    public class WindShape
    {
        private Texture2D[] frameI = new Texture2D[6];
        private Texture2D[] frameW = new Texture2D[7];

        private SpriteFont font;

        private Vector2 Pos1, Pos2, Pos3, Pos4;
        private short i,j, indexI, indexW;
        private int frRateI, frRateW;

        private SoundEffectInstance sound;
        private string text, txt, aniText;
        public bool isOn, onV, on, onPl;
        public float scale, textScale;

        //----------------Konstruktorius-------------------------------------------------------------------------------------
        public WindShape(Game1 game)
        {
            Skait sk = new Skait();
            scale = sk.loadC() * 0.75f;
            textScale = sk.loadC() * 0.25f;

            frameI[0] = game.Content.Load<Texture2D>("WindShape//I1");
            frameI[1] = game.Content.Load<Texture2D>("WindShape//I2");
            frameI[2] = game.Content.Load<Texture2D>("WindShape//I3");
            frameI[3] = game.Content.Load<Texture2D>("WindShape//I4");
            frameI[4] = game.Content.Load<Texture2D>("WindShape//I5");
            
            frameI[5] = game.Content.Load<Texture2D>("WindShape//spitfireKamu");

            frameW[0] = game.Content.Load<Texture2D>("WindShape//v1");
            frameW[1] = game.Content.Load<Texture2D>("WindShape//v2");
            frameW[2] = game.Content.Load<Texture2D>("WindShape//v3");
            frameW[3] = game.Content.Load<Texture2D>("WindShape//v4");
            frameW[4] = game.Content.Load<Texture2D>("WindShape//v5");
            frameW[5] = game.Content.Load<Texture2D>("WindShape//v6");
            
            frameW[6] = game.Content.Load<Texture2D>("WindShape//Mig15Kamu");

            font = game.Content.Load<SpriteFont>("font//font");

            sound = game.Content.Load<SoundEffect>("Sound//EngineLOW").CreateInstance();

            Pos1 = new Vector2(0, 0);
            Pos2 = new Vector2(TouchPanel.DisplayWidth / 1.5f, 0);
            Pos3 = new Vector2(TouchPanel.DisplayWidth / 35, TouchPanel.DisplayHeight / 1.9f);
            Pos4 = new Vector2(1.1f * TouchPanel.DisplayWidth / 3, 0);
            
            i = 0;
            indexI = 1;
            frRateI = 1;
            isOn = false;
            onV = false;
            on = false;
            onPl = true;
            frRateI = 3;
            frRateW =4;
            text = "Oro srautas veikia skirtingos\nformos sparnus Sparnai yra palenkti kritiniu kampu\nsu oro srautu. Sparnai dar nepaveikti jokios išorinės\njėgos. Todėl prie didelio greičio išlieka stabilus.";
            aniText = "Abu sparnų tipus veikia vienodo\nstiprumo oro srautas. Tiesus sparnas sukasi daug\ngreičiau. Sukimasi sukelia oro gūsis arba kitoks\nlaikinas nevienodumas po skirtingų pusių sparnais.\n Pastaba: sukimasis tai pasekmė."; 
            txt = text;
            
           
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

                    if (((gs.Position.X > Pos4.X) && (gs.Position.X < Pos2.X) && (gs.Position.Y < TouchPanel.DisplayHeight / 3) && onPl == false))
                    {
                        if (on == false) { on = true; }
                        else { on = false; }
                    }
                    else if (on == false && (gs.Position.Y < TouchPanel.DisplayWidth / 3))
                    {
                        if (onPl == false)
                        {
                            onPl = true;
                        }
                        else { onPl = false; }

                    }

                }

                if (on == true)
                {

                    /* Fram'ai Iwing */
                    if (i > frRateI)
                    {
                        i = 1;
                        if ((indexI + 1) > 4) { indexI = 0; }
                        else { indexI++; }
                    }
                    i++;
                    /* Fram'ai Vwing */
                    if (j > frRateW)
                    {
                        j = 1;
                        if ((indexW + 1) > 5) { indexW = 0; }
                        else { indexW++; }
                    }
                    j++;
                }
                if (onPl == false) 
                {
                    if (SoundState.Stopped == sound.State)
                    {
                        sound.Play();
                    }
                }
                else
                {
                    if (sound.State == SoundState.Playing)
                    {
                        sound.Stop();
                    }
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    if (sound.State == SoundState.Playing)
                    {
                        sound.Stop();
                    }
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
                if (on == true && onPl==false)
                {
                    spriteBatch.Draw(frameI[indexI], Pos1, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(frameW[indexW], Pos2, null, Color.Yellow, 0f, new Vector2(0, 0), scale * 1.5f, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, "Gražin į\npirminę padėtį.", Pos4, Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, aniText, Pos3, Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
                }
                else if (onPl == true && on == false) 
                {
                    spriteBatch.Draw(frameI[5], Pos1, null, Color.White, 0f, new Vector2(0, 0), scale * 0.6f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(frameW[6], new Vector2(1.8f*TouchPanel.DisplayWidth/3,0), null, Color.White, 0f, new Vector2(0, 0), scale * 0.6f, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, "     Spitfire Mk VB                       Mig -15\nTai yra realus pavyzdžiai, Tiesių ir palenktų sparnų.\nNevienodi sparnai turi ne vienodas sąvybes.", new Vector2(TouchPanel.DisplayWidth / 35, 1.9f * TouchPanel.DisplayHeight / 3), Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
                
                }
                else 
                {
                    spriteBatch.Draw(frameI[0], Pos1, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(frameW[0], Pos2, null, Color.Yellow, 0f, new Vector2(0, 0), scale * 1.5f, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, text, Pos3, Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, "Paveikt sparnus,\nne vienoda jėga.\n\nP.s. jėga neveikia\nobjekto nuolatos!", Pos4, Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
                }
            }

        }
        //------------------------------------------
        public void On(bool log)
        {
            isOn = log;
            i = 0;
            indexI = 0;
            indexW = 0;
            onV = false;
            on = false;
            onPl = true;   
        }
        //-----------------------------------------
        public void Destroy()
        {
            isOn = false;

            frameI[0] = null;
            frameI[1] = null;
            frameI[2] = null;
            frameI[3] = null;
            frameI[4] = null;

            frameW[0] = null;
            frameW[1] = null;
            frameW[2] = null;
            frameW[3] = null;
            frameW[4] = null;
            frameW[5] = null;
            font = null;
            sound = null; 
        }
        //---------------------------------------------------------------------------------------
        

        //-----------------------------------------------------------------------------------------------------------------------
    }
}
