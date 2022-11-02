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
    class Skait
    {
       private float scale;

       public Skait() 
       {
           double k;
           if (TouchPanel.DisplayWidth > TouchPanel.DisplayHeight)
           {
               k = (TouchPanel.DisplayWidth / 800);
           }
           else { k = TouchPanel.DisplayHeight / 800; }
           scale = (float)(k);
       }
//------------------------------------------
        public float loadC() { return scale; }
//---------------------------------------------
       
    }
}
