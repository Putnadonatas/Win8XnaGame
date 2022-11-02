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
    public class engine
    {
        Texture2D[] eng = new Texture2D[2];
        Texture2D[] plane = new Texture2D[2];
        Texture2D Angled, background;
       
        SoundEffectInstance low, med, high;
        SpriteFont font;
        
       private float angle;

       private string[] engText = new string[6];
       private string[] planeText = new string[3];
       private int eIndex, pIndex, aIndex;
       private string[] angleText = new string[5];

       private Vector2 Pos, Pos2, origin, PosText;
       private int i = 1, j = 0, k = 0;
       private float scalePl, scaleEn, scaleText, scale, scaleB;
       public bool isOn, onAni, engOn;
       public bool isLoaded=false;

        

//-----------------------------------------------------------------------------------
        public engine(Game1 game)
        {
            eng[0] = game.Content.Load<Texture2D>("engine//MIG-15E_1");
            eng[1] = game.Content.Load<Texture2D>("engine//MIG-15E_2");

            plane[0] = game.Content.Load<Texture2D>("engine//15_1");
            plane[1] = game.Content.Load<Texture2D>("engine//15_2");

            Angled = game.Content.Load<Texture2D>("engine//MIGeng");
            background = game.Content.Load<Texture2D>("engine//engineW");

            font = game.Content.Load<SpriteFont>("font//font");

            engText[0] = "Turboreaktyvinis variklis yra vienas paprašiausių\nvariklių. Įsiurbiamas šaltas oras, jis sušildomas išplečia ir\nišeidamas išsuka turbiną.";
            engText[1] = "1 - variklio isiurbimo anga. Tai kanalas per kurį\npatenka oras į variklį.";
            engText[2] = "2 - kompresorius. Jis skirtas įtraukti ir suslėgtį\norą. Oras slegiamas siekiant jį lengvai padegt degimo\nkameroje.";
            engText[3] = "3 - degimo kamera. Šioje stadijoje padegamas oras.\nTemperatūra siekia 1600 laipsnių Celsijaus.";
            engText[4] = "4 - turbina. Turbina sukama išeinačio oro. Turbina\nsuką kompresorių, kuris įtraukia dar daugiau oro.";
            engText[5] = "5 - reaktyvinė tūta. Vamzdis išmetantis šiltą orą.\nJo dėka oras sutelkiams į vieną kryptį, taip\nsukuriama stumos jėga varanti lektuva priekin.";

            planeText[0] = "Paveiklėlyje matote Mig-15 išilginį pjūvį. Jame\npavaizduota kaip oro srautas patenka į variklį\nir jame cirkuliuoja. ";
            planeText[1] = "Melyna spalva pavaizduotas vėsiausias oras.\nGeltona spalva pažymėtas oras yra šiltesnis,\nnes kompresoriuje jis suslegiamas.  ";
            planeText[2] = "Raudona spalva pažymėtas oras yra pats\nkaršiausias nes padegtas (žibalas patekias į suslėgtą\norą užsidega).";

            angleText[0] = "1,2,3 - numeriais pažymeta oro įsiurbimo kanalas.\n1 - pradžia: dvi viendos angos lektuvo priekyje.\n2 ir 3 - kanalas vedantis iki varyklio.";
            angleText[1] = "4 - variklio oro įsiurbimo angos.";
            angleText[2] = "5 - turboreaktyvinis variklis VK-1 klimov.";
            

            low = game.Content.Load<SoundEffect>("Sound//EngineLOW").CreateInstance();
            med = game.Content.Load<SoundEffect>("Sound//EngineMED").CreateInstance();
            high = game.Content.Load<SoundEffect>("Sound//EngineMAX").CreateInstance();
            
            Pos = new Vector2(TouchPanel.DisplayWidth /25, 0);
            Pos2 = new Vector2(TouchPanel.DisplayWidth / 4, 0);
            PosText = new Vector2(TouchPanel.DisplayWidth / 25, TouchPanel.DisplayHeight / 2);

            Skait sk = new Skait();
            sk.loadC();
            scalePl = 0.75f * sk.loadC();
            scaleEn = 0.5f * sk.loadC();
            scale = 0.5f * sk.loadC();
            scaleB = sk.loadC()*0.75f;
            scaleText = sk.loadC() * 0.26f;

            origin = new Vector2(0,0);
            angle = 0.0f;
            isOn = false;
            onAni = false;
            i = 0;

            eIndex=0;
            pIndex=0;
            aIndex = 0;


            isLoaded = true;
        }
 //---------------------------------------------------------------------------------------------------------
        public void Update(GameTime gametime)
        {

              if (isOn == true)
              {

                   if (TouchPanel.IsGestureAvailable && isOn==true) 
                   {
                  GestureSample gs = TouchPanel.ReadGesture();

                  if (gs.Position.X > 0 && gs.Position.Y < TouchPanel.DisplayHeight / 2 && GestureType.Tap == gs.GestureType)
                  {
                      if (onAni == false)
                      {
                          onAni = true;
                      }
                      else if (onAni == true && engOn == false)
                      {
                          engOn = true;
                      }
                      else if (onAni == true && engOn == true)
                      {
                          engOn = false;
                          onAni = false;
                          i = 0;
                          stopPlaying();
                      }
                  }
                  else if (gs.Position.X > 0 && gs.Position.Y > TouchPanel.DisplayHeight / 2 && GestureType.DoubleTap == gs.GestureType)
                  {
                      if (onAni == false) 
                      {
                          if (aIndex + 1 > 2)
                          {
                              aIndex = 0;
                          }
                          else 
                          {
                              aIndex++; 
                          }
                          
                      }
                      else if (onAni == true && engOn==false)
                      {
                          if (pIndex + 1 > 2)
                          {
                              pIndex = 0;
                          }
                          else
                          {
                              pIndex++;
                          }

                      }
                      else if (onAni == true && engOn == true)
                      {
                          if (eIndex + 1 > 5)
                          {
                              eIndex = 0;
                          }
                          else
                          {
                              eIndex++;
                          }

                      }

                  }
           
                   }
                
                  if ((i+1) > 1 && onAni==true ) 
                  { 
                      i = 0; 
                  } 
                  else 
                  { 
                      i++; 
                  }


                  if (SoundState.Stopped == low.State && onAni==true && k<=2)
                  {
                      k++;
                      low.Play();
                  }
                  if (SoundState.Stopped == med.State && onAni == true && j <= 2 && k >= 2)
                  {
                      j++;
                      med.Play();
                  }
                  if (SoundState.Stopped == high.State && onAni == true && j >= 2)
                  {
                      high.Play();
                  }
                

                  if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) 
                  {
                      stopPlaying();
                      On(false);

                  }
              } 
        }
//------------------------------------------------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isOn == true)
            {
                spriteBatch.Draw(background, new Vector2(TouchPanel.DisplayWidth / 25, TouchPanel.DisplayHeight / 25), null, Color.LightSkyBlue, angle, origin, scaleB, SpriteEffects.None, 0.0f);

                if (onAni == true && engOn == true) 
                {
                    spriteBatch.Draw(eng[i], Pos2, null, Color.White, angle, origin, scaleEn, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, engText[eIndex], PosText, Color.Black, 0.0f, Vector2.Zero, scaleText, SpriteEffects.None, 0.0f);
                }
                else if (onAni == true)
                {
                    spriteBatch.Draw(plane[i], Pos, null, Color.White, angle, origin, scalePl, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, planeText[pIndex], PosText, Color.Black, 0.0f, Vector2.Zero, scaleText, SpriteEffects.None, 0.0f);
                }
                else
                {
                    spriteBatch.Draw(Angled, new Vector2(TouchPanel.DisplayHeight / 2 + TouchPanel.DisplayHeight / 10, -TouchPanel.DisplayHeight/25), null, Color.White, 0.50f, new Vector2(TouchPanel.DisplayWidth / 2, TouchPanel.DisplayHeight / 4), scale, SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, angleText[aIndex], PosText, Color.Black, 0.0f, Vector2.Zero, scaleText, SpriteEffects.None, 0.0f);
                }

            }
        }
        //-----------------------------------------------------------------------------------------------------------------------
        public void On(bool log) 
        { 
            isOn = log;
            engOn = false;
            onAni = false;
            i = 0;
            eIndex = 0;
            pIndex = 0;
            aIndex = 0;

        }
        //---------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------
        public void Destroy() // dar neisbandytas
        {
            isOn = false;
            
            low= null;
            med= null; 
            high= null;

            eng[0] = null;
            eng[1] = null;

            plane[0] = null;
            plane[1] = null;

            Angled = null;
            background = null;
            font = null;
           
            isLoaded = false;
 
        }
        public void stopPlaying() 
        {
            if (SoundState.Playing == low.State)
            {
                low.Stop();
            }
            if (SoundState.Playing == med.State)
            {
                med.Stop();
            }
            if (SoundState.Playing == high.State)
            {
                high.Stop();
            }

        }
//-------------------------------------------------------------------------------------------------------------------------
       
    }
}