using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace climbing.Utility
{
    class Timer
    {
        float currentTime;
        float limitTime;
        static Random rand = new Random();

        public Timer()
        {
            currentTime = 0.0f;
        }
        public Timer(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }

        public void ResetTimer()
        {
            currentTime = 0.0f;
        }
        
        public void Initialize()
        {
            currentTime = limitTime;
        }

        public void ResetCounter()
        {
            currentTime = rand.Next(180, 480);
        }

        public void Update()
        {
            currentTime = currentTime + 1.0f;
        }

        public void DecreaseTime()
        {
            currentTime = currentTime - 1.0f;
        }

        public float Now()
        {
            return currentTime;
        }

        public void Change(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();   
        }

        public bool IsTime()
        {
            return currentTime <= 0.0f;
        }

        public void UpdateGauge()
        {
            currentTime += currentTime + (1.0f / 3.0f);
        }
    }
}