using UnityEngine;

namespace cooperplant4
{
    public class Tree : MonoBehaviour
    {
        public int scoreOnHit = 10;
        public int scoreOnFell = 100;
        public int hitsToFell = 3;

        public Score Chop()
        {
            if (--hitsToFell == 0)
            {
                Destroy(gameObject);
                return new Score(scoreOnFell, "Felled tree");
            }
            else
            {
                return new Score(scoreOnHit, "Hit tree");
            }
        }
    }
}