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
    class Zaidimas
    {
        private Move background;
        private airplane airpl;
        public bool isOn, isLoaded, globalIsOn;
        public int i;
        private float scale;
        private Texture2D loadScreen;
        private PauseGame pause;
        private birds pauks;

        // kliutys paukšiai

        public Zaidimas(Game1 game)
        {
            isLoaded = false;
            isOn = false;
            i = 0;
            Skait sk = new Skait();
            scale = sk.loadC();
            loadScreen = game.Content.Load<Texture2D>("LoadScreen//loadScreen_1");
            globalIsOn = true;

        }
        private void Load(Game1 game)
        {
            background = new Move(game);
            airpl = new airplane(game);
            pause = new PauseGame(game);
             pauks = new birds(game);

            isLoaded = true;
            i = 0;
        }

        public void Update(GameTime gameTime, Vector3 duom, Game1 game)
        {
            if (isOn == true)
            {

                if (isLoaded == true)
                {
                    airpl.Update(gameTime, duom);
                    background.Update(gameTime, duom, airpl.speedValue());
                    pauks.Update(gameTime,duom,airpl.getPos(),airpl.aukstisValue(),airpl.nuotolisValue(),airpl.speedValue());
                    if (pauks.isControlValue() == false) { airpl.isControl = false; }

                }

                else if (isLoaded == false)
                {
                    if (i > 1)
                    {
                        Load(game);
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            if (isLoaded == true)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && (pause.curentState == 0 || pause.curentState == 1))
                {
                    if (pause.isOn == false)
                    {
                        pause.On(true);
                    }
                    else
                    {
                        pause.On(false);
                    }
                    OnResume(pause.gameIsOn);
                }

                pause.Update(gameTime);
                if (pause.curentState == 2)
                {
                    Destroy();
                }
                else if (pause.curentState == 1)
                {
                    OnResume(true);
                    pause.curentState = 0;
                }
            }
        }
        //------------------------------------------------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isOn == true)
            {
                if (isLoaded == true)
                {
                    background.Draw(spriteBatch);
                    airpl.Draw(spriteBatch);
                    pauks.Draw(spriteBatch);
                    

                }
                else
                {
                    spriteBatch.Draw(loadScreen, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), scale * 0.75f, SpriteEffects.None, 0.0f);
                }
            }
            else if (isLoaded == true && pause.isOn == true)
            {
                pause.Draw(spriteBatch);
            }
        }
        //------------------------------------------------
        public void Destroy()
        {

            isLoaded = false;
            isOn = false;
            airpl = null;
            background = null;
            globalIsOn = false;
        }
        public void OnResume(bool log)
        {
            isOn = log;
            airpl.OnResume(isOn);
            background.OnResume(isOn);
            pauks.onResume(isOn);
        }
        public void OnGame()
        {
            isOn = true;
            globalIsOn = true;
        }
        public bool pauseValue()
        {
            return pause.isOn;
        }
        //------------------------------------------------------------
        
    }
}
