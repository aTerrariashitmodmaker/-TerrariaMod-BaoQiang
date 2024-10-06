using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 爆枪英雄.常用类和结构体
{
    public static class Easing
    {
        private  static float EaseInQuad(float t)
        {
            return t * t;
        }

        public   static float EaseOutQuad(float t)
        {
            return 1 - (1 - t) * (1 - t);
        }

        private  static float EaseInOutQuad(float t)
        {
            return (float)(t < 0.5 ? 2 * t * t : 1 - Math.Pow(-2 * t + 2, 2) / 2);
        }
        public static Vector2 EaseOutLerp(Vector2 start, Vector2 end, float t)
        {
            return Vector2.Lerp(start, end, EaseOutQuad(t));
        }
    }

   
}
