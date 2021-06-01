using UnityEngine;

namespace haccr
{
    public class HookSpawner : MonoBehaviour
    {
        public Transform player;
        public GameObject hook;
        public LayerMask mask;
        public float range = 100f;

        void Start()
        {
            SpawnHooks();
        }

        void SpawnHooks()
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(player.position, range, mask);
            int nearby = collisions.Length;
            int totalHooks = 3;
            for (int i = 0; i < totalHooks - nearby; i++)
            {
                bool hasSpawned = false;
                bool safeArea = true;
                while (!hasSpawned)
                {
                    Vector3 hookPos = new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
                    Collider2D[] nearbyHooks = Physics2D.OverlapCircleAll(hookPos, range, mask);
                    foreach (Collider2D collision in nearbyHooks)
                    {
                        if ((hookPos - collision.gameObject.transform.position).magnitude < 3) safeArea = false;
                    }
                    if ((hookPos - player.position).magnitude < 3) safeArea = false;
                    if (safeArea)
                    {
                        Instantiate(GetHookType(), hookPos, Quaternion.identity);
                        hasSpawned = true;
                    }
                }
            }
        }

        Object GetHookType()
        {
            return hook;
        }
    }
}
