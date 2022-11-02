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
using zaidimasDP;

namespace ZaidimasDP_1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Accel acc= new Accel();
        Skait sk = new Skait();

           
        
        MainMenu menuPagrindinis;
  
        AboutMenu menuApie;

            rearplane valdikliai;
            engine variklis;
            Autorius autorius;
            WindTunnel sparnas;
            WindShape formaIV;
            WingBorder borteliai;  
        Zaidimas games; 

        //Texture2D loadScreen;
    


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            acc.init();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           


            menuPagrindinis = new MainMenu(this);
                 menuApie = new AboutMenu(this);
                  valdikliai = new rearplane(this);
                  autorius = new Autorius(this);
                  sparnas = new WindTunnel(this);
                  variklis = new engine(this); 
                  formaIV = new WindShape(this); 
                borteliai = new WingBorder(this); 
                games = new Zaidimas(this);
            
            // TODO: use this.Content to load your game content here
        }

              /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
           
            // TODO: Unload any non ContentManager content here
        }
        

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Vector3 duom;

            TouchPanel.EnabledGestures = GestureType.Tap |
            GestureType.DoubleTap | GestureType.Hold;

           duom= acc.update(gameTime);
           
         
            //k =(int)(duom.Y / Math.Abs(duom.Y));

          if (menuPagrindinis.isON() == true)
           {
               menuPagrindinis.update(gameTime, this);
           }
              else 
              {
                  if (menuPagrindinis.isON() == false && menuPagrindinis.select() == 1) 
                  { 
                      if (games.isLoaded==false) 
                      {
                          games.OnGame();
                      }
                      games.Update(gameTime, duom, this);  
                      if (games.globalIsOn == false)
                      {
                          menuPagrindinis.MenuOn(true);
                      }
                       } 
                       else if (menuPagrindinis.isON() == false && menuPagrindinis.select() == 2)
                       {
                           if (menuApie.isON() == false && menuApie.pasirinktis()==0) { menuApie.On(true); }

                           if (menuApie.isON() == true)
                           {
                               menuApie.Update(gameTime);
                               backToMenu();
                           }
                           else if(menuApie.pasirinktis()>0) 
                           {
                               aboutUpdate(gameTime,duom);
                              backToApieMenu();
                           }

                       }
                       else if (menuPagrindinis.isON() == false && menuPagrindinis.select() == 3)
                       {
                           if (autorius.isOn == false) { autorius.On(true); }
                           autorius.Update(gameTime);
                           if (autorius.isOn == false) { menuPagrindinis.MenuOn(true); }
                       }

                   } 
                      base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin();
           menuPagrindinis.Draw(spriteBatch);
            menuApie.Draw(spriteBatch);
            autorius.Draw(spriteBatch);
            valdikliai.Draw(spriteBatch);
            variklis.Draw(spriteBatch);
            sparnas.Draw(spriteBatch); 
            formaIV.Draw(spriteBatch);
            borteliai.Draw(spriteBatch); 
           games.Draw(spriteBatch);
           // spriteBatch.DrawString(Content.Load<SpriteFont>("font//font"), k.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        
//---------------------------------------------------------------------------------------------
     private void backToMenu() 
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                menuPagrindinis.MenuOn(true);
            }
        }
 //------------------------------------------------------------
       private void backToApieMenu()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                menuApie.On(true);
            }
        }
//-------------------------------------------------------
        private void aboutUpdate(GameTime gameTime,Vector3 duom)
        {
            switch(menuApie.pasirinktis())
            {
                case 1:
                    if (valdikliai.isOn == false) { valdikliai.On(true); }
                    valdikliai.Update(gameTime,duom);
                    break;
                case 2:
                    if (variklis.isOn == false) { variklis.On(true); }
                    variklis.Update(gameTime);
                    break;
                case 3:
                    if (sparnas.isOn == false) { sparnas.On(true); }
                    sparnas.update(gameTime);
                    break;
                case 4:
                    if (borteliai.isOn == false) { borteliai.On(true); }
                    borteliai.Update(gameTime);
                    break;
                case 5:
                    if (formaIV.isOn == false) { formaIV.On(true); }
                        formaIV.update(gameTime);
                    break;

            }
            
        } 
    }
//---------------------------------------------------------------
     
    }

