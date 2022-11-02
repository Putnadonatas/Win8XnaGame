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
    class WingBorder
    {
        private Texture2D[] frame = new Texture2D[3];
        private Texture2D[] cloud = new Texture2D[3];
        private Texture2D  background, pixel;
        private SpriteFont font;
        private SoundEffectInstance sound;
        private Random rnd = new Random();

        private Vector2 Pos1, Pos2, Pos3,Pos4;
        private short i, index;
        private int ranIndex, ranIndex2, frRate;
        
        private string text;
        public bool isOn;
        private float scale, textScale, cloudScale, scalePl;
        
        public WingBorder(Game1 game)
        {
            Skait sk = new Skait();
            scale = sk.loadC() * 1.2f;
            scalePl = sk.loadC() * 0.25f;
            textScale = sk.loadC() * 0.25f;
            cloudScale = sk.loadC() * 0.15f;
            frRate = 6;

            frame[0] = game.Content.Load<Texture2D>("WingBorder//mig-15WT1");
            frame[1] = game.Content.Load<Texture2D>("WingBorder//mig-15WT2");
            frame[2] = game.Content.Load<Texture2D>("WingBorder//mig-15WT3");

            cloud[0] = game.Content.Load<Texture2D>("Cloud//Cloud_1");
            cloud[1] = game.Content.Load<Texture2D>("Cloud//Cloud_2");
            cloud[2] = game.Content.Load<Texture2D>("Cloud//Cloud_3");

            pixel = game.Content.Load<Texture2D>("AboutMenu//clear");

            font = game.Content.Load<SpriteFont>("font//font");

            background = game.Content.Load<Texture2D>("WingBorder//water");

            sound = game.Content.Load<SoundEffect>("Sound//EngineMAX").CreateInstance();

            Pos1 = new Vector2(0, 0); //background
            Pos2 = new Vector2(0, TouchPanel.DisplayHeight / 8); //plane
            Pos3 = new Vector2(TouchPanel.DisplayWidth, TouchPanel.DisplayHeight+1);
            Pos4 = new Vector2(TouchPanel.DisplayWidth, -TouchPanel.DisplayHeight/2);

            text = "Lektuvo borteliai (geltoni)\n ant sparnų nukreipia oro\nsrautą lygiagrečiai lektuvo\nskriejimo krypčiai.\nAnimacijoje pavaizduota kaip\noro srauto linijos keičia\nkryptį pasiekusios sparno\nbortelius. Dalys oro\nnuslenka sparno kraštu ir\nnepatenka nei virš, nei po\nsparnu.";
            //max s30
            isOn = false;
            i = 0;
            index = 0;



        }
        public void Update(GameTime gametime) 
        {
            if (isOn == true)
            {

                if (Pos3.Y > TouchPanel.DisplayHeight)
                {
                    Pos3.X = XValue();
                    ranIndex = rnd.Next(0, 2);
                    Pos3.Y = -cloud[ranIndex].Height;
                }
                else 
                {
                    Pos3.Y +=TouchPanel.DisplayHeight / 20;
                }
//-------------------------------------------------------------
                if (Pos4.Y > TouchPanel.DisplayHeight)
                {
                    Pos4.X = XValue();
                    ranIndex2 = rnd.Next(0, 2);
                    Pos4.Y = -cloud[ranIndex].Height;
                }
                else
                {
                    Pos3.Y += TouchPanel.DisplayHeight / 20;
                }
//-------------------------------------------------------------------------
                if (i > frRate)
                {
                    i = 1;
                    if ((index + 1) > 2) { index = 0; }
                    else { index++; }
                }
                i++;
//---------------------------------------------------------------------------
                    if (SoundState.Stopped == sound.State)
                    {
                        sound.Play();
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
        public void Draw(SpriteBatch spriteBatch) 
        {
            if (isOn == true)
            {
                spriteBatch.Draw(background, Pos1, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(pixel, new Vector2(0.98f*TouchPanel.DisplayWidth/2,0), new Rectangle(0,0,TouchPanel.DisplayWidth/2,TouchPanel.DisplayHeight), Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(cloud[ranIndex], Pos3, null, Color.White, 0f, new Vector2(0, 0), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(cloud[ranIndex2], Pos4, null, Color.White, 0f, new Vector2(0, 0), cloudScale, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(frame[index], Pos2, null, Color.White, 0f, new Vector2(0, 0), scalePl, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(font, text, new Vector2(TouchPanel.DisplayWidth / 2, TouchPanel.DisplayHeight /35), Color.Black, 0.0f, Vector2.Zero, textScale, SpriteEffects.None, 0.0f);
            }
        }
        public void On(bool log)
        {
            isOn = log;
            
            Pos3 = new Vector2(TouchPanel.DisplayWidth, TouchPanel.DisplayHeight + 1);
            Pos4 = new Vector2(TouchPanel.DisplayWidth, -TouchPanel.DisplayHeight / 2);
            
            i = 0;
            index = 0;
        }

        private int XValue()
        {
            int x;
            x = rnd.Next(1, (int)TouchPanel.DisplayWidth / 25) * rnd.Next(1, 5);
            return x;
        }
    }
}
