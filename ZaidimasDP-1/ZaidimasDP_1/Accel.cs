using System;
using Microsoft.Xna.Framework;
using Microsoft.Devices.Sensors;

namespace ZaidimasDP_1
{
    class Accel
    {
        private Accelerometer acc;
        private Vector3 accReading;
        //----------------------------------------------------------------------
        public void init()
        {

            acc = new Accelerometer();
            acc.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(acc_ReadingChanged);
            acc.Start();
        }
        //---------------------------------------------------------------------------------
        void acc_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            accReading.X = (float)e.X;
            accReading.Y = (float)e.Y;
            accReading.Z = (float)e.Z;
        }
        //------------------------------------------------------------------
        public Vector3 update(GameTime gameTime)
        {
            return accReading;
        }
        //-----------------------------------------------------------------
    }
}

