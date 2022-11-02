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
    class rearplane
    {
        private Texture2D eleron0, eleron2, eleron_2;
        private Texture2D tale1, tale0, tale_1;
        private Texture2D side0, sideK, sideD;
        private Texture2D backGround;
        private Vector2 Pos, midPos, origin, posBack;
        private SpriteFont font;
        private float scale, scaleB;
        private float angle;
        private short taleI, elerI, sideI, currentState;
        private int stepX, stepY;
        public bool isOn;
        public  bool isLoaded = false;
//----------------------------------------------------------------------------------------------------------------------
       public  rearplane (Game1 game) 
        {

            eleron0 = game.Content.Load<Texture2D>("RearPlane//Neutral");
            eleron2 = game.Content.Load<Texture2D>("RearPlane//Des2");
            eleron_2 = game.Content.Load<Texture2D>("RearPlane//Kair2");
            tale1 = game.Content.Load<Texture2D>("RearPlane//Up");
            tale0 = game.Content.Load<Texture2D>("RearPlane//Neut");
            tale_1 = game.Content.Load<Texture2D>("RearPlane//Down");
            backGround = game.Content.Load<Texture2D>("RearPlane//Dinamic");

            font = game.Content.Load<SpriteFont>("font//font");
            
            side0 = game.Content.Load<Texture2D>("RearPlane//trN");
            sideK = game.Content.Load<Texture2D>("RearPlane//trK");
            sideD = game.Content.Load<Texture2D>("RearPlane//trD");

           

            Skait sk = new Skait();
            scale = sk.loadC()*0.75f;
            scaleB = sk.loadC()*4.0f;
            
             
            Pos = new Vector2 (TouchPanel.DisplayWidth/2,TouchPanel.DisplayHeight/2);
            midPos = new Vector2(eleron0.Width / 2, eleron0.Height / 2);
           
            origin = Pos;

            posBack = new Vector2(backGround.Width / 2, backGround.Height / 2);
            


            isOn = false;

            taleI = 0;
            elerI = 0;
            sideI = 0;
            currentState = 0;

            
           stepY = TouchPanel.DisplayHeight / 50;
            stepX = TouchPanel.DisplayWidth / 50;

            isLoaded = true;
            
        }
//------------------------------------------------------------
        public void Update(GameTime gametime, Vector3 acc) 
        {

            if (isOn == true)
            {
                

                if (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gs = TouchPanel.ReadGesture();
                    if (gs.Position.X < TouchPanel.DisplayWidth / 3 && gs.Position.Y < TouchPanel.DisplayHeight / 3 && GestureType.Tap==gs.GestureType) //kairys virsus
                    {
                        currentState = 1;
                        sideI = 2;
                        taleI = 1;
                    }
                    else if (gs.Position.X > 2 * TouchPanel.DisplayWidth / 3 && gs.Position.Y > 2 * TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) //desnis apacia
                    {
                        currentState = 2;
                        sideI = 1;
                        taleI = 2;
                       
                    }
                    else if (gs.Position.X < TouchPanel.DisplayWidth / 3 && gs.Position.Y > 2 * TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) //kairys apacia
                    {
                        currentState = 3;
                        sideI = 2;
                        taleI = 2;
                        
                    }
                    else if (gs.Position.X > 2 * TouchPanel.DisplayWidth / 3 && gs.Position.Y < TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) // desnis apacia
                    {
                        currentState = 4;
                        sideI = 1;
                        taleI = 1;
                    }
                    else if (gs.Position.X > 2 * TouchPanel.DisplayWidth / 3 && gs.Position.Y > TouchPanel.DisplayHeight / 3 && gs.Position.Y < 2 * TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) // desnis
                    {
                        currentState = 5;
                        sideI = 1;
                        taleI = 0;
                    }
                    else if (gs.Position.X < TouchPanel.DisplayWidth / 3 && gs.Position.Y > TouchPanel.DisplayHeight / 3 && gs.Position.Y < 2 * TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) // kairis
                    {
                        currentState = 6;
                        sideI = 2;
                        taleI = 0;
                        
                    }
                    else if (gs.Position.X < 2 * TouchPanel.DisplayWidth / 3 && gs.Position.X > TouchPanel.DisplayWidth / 3 && gs.Position.Y < TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) // virsus
                    {
                        currentState = 7;
                        sideI = 0;
                        taleI = 1;
                    }
                    else if (gs.Position.X < 2 * TouchPanel.DisplayWidth / 3 && gs.Position.X > TouchPanel.DisplayWidth / 3 && gs.Position.Y > 2 * TouchPanel.DisplayHeight / 3 && GestureType.Tap == gs.GestureType) //apacia
                    {
                        currentState = 8;
                        sideI = 0;
                        taleI = 2;

                    }
                    else 
                    {
                        currentState = 0;
                        sideI = 0;
                        taleI = 0;
                    }

                }
                
                
                    if ( acc.Y <= -0.2f)
                    {
                        angle -= 0.03f; 
                        elerI = 1;
                    }
                    else if (acc.Y >= + 0.2f)
                    {
                        angle += 0.03f; 
                        elerI = 2;
                    }
                    else
                    {
                       
                        elerI = 0;
                    }


                   /* switch (currentState) 
                    {
                        case 0:
                            break;
                        case 1:
                            if ((posBack.X + stepX) < (backGround.Width - TouchPanel.DisplayWidth ))
                       {      
                        posBack.X += stepX;
                       }

                       if ((posBack.Y + stepY) < (backGround.Height - TouchPanel.DisplayHeight ))
                       {
                           posBack.Y += stepY;
                       }
                    
                            break;
                        case 2:
                            if (posBack.X - stepX > Pos.X  )
                            {
                                posBack.X -= stepX;
                            }
                            if (posBack.Y - stepY > Pos.Y  )
                            {
                                posBack.Y -= stepY;
                            }
                        
                    
                            break;
                        case 3:
                            if (posBack.Y - stepX > Pos.Y ) 
                            { 
                                posBack.Y -= stepY;
                            }
                            if ((posBack.X + stepX) < (backGround.Width - TouchPanel.DisplayWidth ))
                            {
                                posBack.X += stepX;
                            }
                            
                     
                            break;
                        case 4:
                            if ((posBack.Y + stepY) < (backGround.Height - TouchPanel.DisplayHeight))
                            {
                                posBack.Y += stepY;
                            }

                            if (posBack.X - stepX > Pos.X )
                        {
                            posBack.X -= stepX;
                        }

                         
                            break;
                        case 5:
                            if (posBack.X - stepX > Pos.X  )
                            {
                                posBack.X -= stepX;
                            }

                     
                            break;
                        case 6:
                            if ((posBack.X + stepX) < (backGround.Width - TouchPanel.DisplayWidth ))
                            {
                                posBack.X += stepX;
                            }

                       
                            break;
                        case 7:
                            if ((posBack.Y + stepY) < (backGround.Height - TouchPanel.DisplayHeight ))
                            {
                                posBack.Y += stepY;
                            }

                        
                            break;
                        case 8:
                            if (posBack.Y - stepY > Pos.Y  )
                            {
                                posBack.Y -= stepY;
                            }

                      
                            break;
                    } */

                switch (currentState) 
                    {
                        case 0:
                            break;
                        case 2:
                            if ((posBack.X + stepX) < (backGround.Width - TouchPanel.DisplayWidth ))
                       {      
                        posBack.X += stepX;
                       }

                       if ((posBack.Y + stepY) < (backGround.Height - TouchPanel.DisplayHeight ))
                       {
                           posBack.Y += stepY;
                       }
                    
                            break;
                        case 1:
                            if (posBack.X - stepX > Pos.X  )
                            {
                                posBack.X -= stepX;
                            }
                            if (posBack.Y - stepY > Pos.Y  )
                            {
                                posBack.Y -= stepY;
                            }
                        
                    
                            break;
                        case 4:
                            if (posBack.Y - stepX > Pos.Y ) 
                            { 
                                posBack.Y -= stepY;
                            }
                            if ((posBack.X + stepX) < (backGround.Width - TouchPanel.DisplayWidth))
                            {
                                posBack.X += stepX;
                            }
                            
                            
                     
                            break;
                        case 3:
                            if ((posBack.Y + stepY) < (backGround.Height - TouchPanel.DisplayHeight))
                            {
                                posBack.Y += stepY;
                            }

                            if (posBack.X - stepX > Pos.X )
                        {
                            posBack.X -= stepX;
                        }

                         
                            break;
                        case 6:
                            if (posBack.X - stepX > Pos.X  )
                            {
                                posBack.X -= stepX;
                            }

                     
                            break;
                        case 5:
                            if ((posBack.X + stepX) < (backGround.Width - TouchPanel.DisplayWidth ))
                            {
                                posBack.X += stepX;
                            }

                       
                            break;
                        case 8:
                            if ((posBack.Y + stepY) < (backGround.Height - TouchPanel.DisplayHeight ))
                            {
                                posBack.Y += stepY;
                            }

                        
                            break;
                        case 7:
                            if (posBack.Y - stepY > Pos.Y  )
                            {
                                posBack.Y -= stepY;
                            }

                      
                            break;
                    }

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    {
                        isOn = false;
                        On(isOn);
                    }
            }
 
        
        }
 //-------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            if(isOn==true)
            {
                spriteBatch.Draw(backGround, origin, null, Color.White, angle,posBack, scaleB, SpriteEffects.None, 0.0f); //background

            switch (taleI)
            {
                case 0:
                    spriteBatch.Draw(tale0, Pos, null, Color.White, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);
                    
                    break;
                case 1:
                    spriteBatch.Draw(tale1, Pos, null, Color.White, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);
                    
                    break;

                case 2:
                    spriteBatch.Draw(tale_1, Pos, null, Color.White, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);
                 
                    break;
            }
            
            switch(elerI)
            {
                case 0:
                    spriteBatch.Draw(eleron0, Pos, null, Color.WhiteSmoke, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);
                    
                    break;  
                case 1:
                    spriteBatch.Draw(eleron2, Pos, null, Color.WhiteSmoke, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);
                   
                    break;
                case 2:
                    spriteBatch.Draw(eleron_2, Pos, null, Color.WhiteSmoke, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);
                   
                    break;
            }

            switch (sideI)
            {
                case 0:
                    spriteBatch.Draw(side0, Pos, null, Color.WhiteSmoke, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);

                    break;
                case 1:
                    spriteBatch.Draw(sideK, Pos, null, Color.WhiteSmoke, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);

                    break;
                case 2:
                    spriteBatch.Draw(sideD, Pos, null, Color.WhiteSmoke, 0.0f, midPos, scale, SpriteEffects.None, 0.0f);

                    break;
            }
            }

        }
        //-----------------------------------------------------------------------------------------------------------------------
        public void On(bool log) 
        { 
            isOn = log;
            Pos = new Vector2(TouchPanel.DisplayWidth / 2, TouchPanel.DisplayHeight / 2);
            midPos = new Vector2(eleron0.Width / 2, eleron0.Height / 2);

            origin = Pos;

            posBack = new Vector2(backGround.Width / 2, backGround.Height / 2);

            taleI = 0;
            elerI = 0;
            sideI = 0;
            currentState = 0;

        }
        //---------------------------------------------------------------------------------------------------------------
        public void Destroy() // dar neisbandytas
        {
            isOn = false;
            tale_1 = null;
            tale0 = null;
            tale1 = null;
            eleron0 = null;
            eleron_2 = null;
            eleron2 = null;
            isLoaded = false;
        }
//-------------------------------------------------------------
    }
}
