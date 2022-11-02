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
    public class MainMenu
    {
       private Texture2D Background, NewGame, Authors, Exit,About;     
       private Vector2 BGPos,NGpos,ABPos,EPos,ATPos;
       public int pasirinktis,i;
       private bool isOn, on;
       public float scale,scaleText;
        

        //----------------Konstruktorius-------------------------------------------------------------------------------------
        public MainMenu(Game1 game)
        {
            int x = TouchPanel.DisplayHeight / 4;
            Skait sk = new Skait();
            scale = sk.loadC()*1.02f;
            scaleText = sk.loadC() * 0.25f;

            Background = game.Content.Load<Texture2D>("Menu//Background");
            NewGame = game.Content.Load<Texture2D>("Menu//NewGame");
            Authors = game.Content.Load<Texture2D>("Menu//Authors"); 
            Exit = game.Content.Load<Texture2D>("Menu//Exit"); 
            About = game.Content.Load<Texture2D>("Menu//About");
 
            BGPos = new Vector2(-2, -2);
            NGpos = new Vector2(x, TouchPanel.DisplayHeight/5);
            ABPos = new Vector2(x, TouchPanel.DisplayHeight* 2 / 5);
            ATPos = new Vector2(x, TouchPanel.DisplayHeight* 3/ 5);
            EPos = new Vector2(x, TouchPanel.DisplayHeight *4/ 5);
           
            pasirinktis = 0;
            i = 0;
            isOn = true;
            on = false;
           

        }
        //----------------Update metodas-------------------------------------------------------------------------------------
        public void update(GameTime gameTime,Game1 game)
        {
            if (isOn == true && on == true) 
            {   
             if (i > 5){
                on = false;
                isOn = false;
                i = 0;
                if (pasirinktis == 4) 
                {
                    game.Exit();
                }
             }
             else { i++; }
            }
            else if (isOn == true)
            {  
                if (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    
                    int x = TouchPanel.DisplayWidth / 4-TouchPanel.DisplayWidth/8;
                    int xMax = TouchPanel.DisplayWidth * 3 / 4;
                    int dif = TouchPanel.DisplayHeight / 10;

                    if (((gs.Position.X >= x) && (gs.Position.X <= xMax)) && ((gs.Position.Y >= NGpos.Y - dif) && (gs.Position.Y <= ABPos.Y + NewGame.Height - dif)))
                    {
                        pasirinktis = 1;
                        on = true;
                    }
                    else if (((gs.Position.X > x) && (gs.Position.X <= xMax)) && (gs.Position.Y >= ABPos.Y - dif && gs.Position.Y <= ATPos.Y + About.Height - dif))
                    {
                        pasirinktis = 2;
                        on = true;
                    }
                    else if (((gs.Position.X > x) && (gs.Position.X <= xMax)) && (gs.Position.Y >= ATPos.Y - dif && gs.Position.Y <= EPos.Y + Authors.Height - dif))
                    {
                        pasirinktis = 3;
                        on = true; 
                    }
                    else if (((gs.Position.X > x) && (gs.Position.X <= xMax)) && (gs.Position.Y >= EPos.Y - dif && gs.Position.Y <= TouchPanel.DisplayHeight - dif)) 
                   {
                       pasirinktis = 4;
                       on = true;
                   }
                }
            }
            
        }

       
        //----------------Draw metodas-------------------------------------------------------------------------------------       
        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (isOn == true)
            {
                spriteBatch.Draw(Background, BGPos, null, Color.White, 0f, new Vector2(0, 0), scale*1.2f, SpriteEffects.None, 0.0f);
                if (on == true) 
                {
                    switch (pasirinktis)
                    {
                        case 1:
                            spriteBatch.Draw(NewGame, NGpos, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(About, ABPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Authors, ATPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Exit, EPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            break;
                        case 2:

                            spriteBatch.Draw(NewGame, NGpos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(About, ABPos, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Authors, ATPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Exit, EPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            break;
                        case 3:

                            spriteBatch.Draw(NewGame, NGpos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(About, ABPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Authors, ATPos, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Exit, EPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            break;
                        case 4:
                         
                            spriteBatch.Draw(NewGame, NGpos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(About, ABPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Authors, ATPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            spriteBatch.Draw(Exit, EPos, null, Color.Yellow, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                            break;
                       
                    }
                }
                else
                {
                    spriteBatch.Draw(NewGame, NGpos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(About, ABPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(Authors, ATPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(Exit, EPos, null, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
                }
            }
        }
//---------------------------------------------------------------------------------------------------------
        public int select() 
        { 
            return pasirinktis; 
        }
 //---------------------------------------------------------------------------------------------------------------
        public bool isON()
        {
           return isOn;
        }
//-----------------------------------------------------------------------------------------------------------------------
        public void MenuOn(bool a) 
        { 
            isOn = a;
            pasirinktis = 0;
            i = 0;
            isOn = true;
            on = false;
        }
//-----------------------------------------------------------------------------------------------------------------------
    }
}
