using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace haccr
{
    [Serializable]
    public class Circle
    {
        public float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public Vector2 RadianToPoint(float radian)
        {
            float x = Mathf.Cos(radian);
            float y = Mathf.Sin(radian);
            return new Vector2(x, y) * radius;
        }

        public Vector2 UnitToPoint(float unit)
        {
            float radian = unit * Mathf.PI * 2;
            float x = Mathf.Cos(radian);
            float y = Mathf.Sin(radian);
            return new Vector2(x, y) * radius;
        }
    }
}
