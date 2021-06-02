using UnityEngine;

namespace cooperplant4
{
    public struct Timer
    {
        public float interval;
        public float time;

        public void Restart()
        {
            time = 0;
        }

        public bool EvaluateFrame()
        {
            bool isExpired = time + Time.deltaTime >= interval;
            time = Mathf.Repeat(time + Time.deltaTime, interval);
            return isExpired;
        }
    }
}