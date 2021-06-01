using UnityEngine;

namespace haccr
{
    public class HookSpawner : MonoBehaviour
    {
        public Transform player;
        public GameObject hook;
        public LayerMask mask;
        public float range = 100f;
        public float collisionDistance = 3f;
        public int spawnAttempts = 10;
        void Start()
        {
            SpawnHooks();
        }

        void SpawnHooks()
        {
            int hooks = CountHooksToSpawn();
            for (int i = 0; i < hooks; ++i)
                AttemptToSpawnHook();
        }

        int CountHooksToSpawn()
        {
            const int totalHooks = 3;
            Collider2D[] collisions = Physics2D.OverlapCircleAll(player.position, range, mask);
            int nearby = collisions.Length;
            int numHooksToSpawn = totalHooks - nearby;
            return numHooksToSpawn;
        }

        void AttemptToSpawnHook()
        {
            for (int i = 0; i < spawnAttempts; ++i)
            {
                Vector3 hookPos = GetRandomPosition();
                if (!IsColliding(hookPos))
                {
                    Instantiate(GetHookType(), hookPos, Quaternion.identity);
                    break;
                }
            }
        }

        Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0);
        }

        bool IsColliding(Vector3 hookPos)
        {
            return IsCollidingWithPhysics(hookPos) 
                || IsCollidingWithPlayer(hookPos);
        }

        bool IsCollidingWithPlayer(Vector3 hookPos)
        {
            Vector3 distance = hookPos - player.position;
            return distance.magnitude < collisionDistance;
        }

        bool IsCollidingWithPhysics(Vector3 hookPos)
        {
            Collider2D[] nearbyHooks = Physics2D.OverlapCircleAll(hookPos, range, mask);
            foreach (Collider2D collision in nearbyHooks)
            {
                Vector3 distance = hookPos - collision.transform.position;
                if (distance.magnitude < collisionDistance) 
                    return true;
            }
            return false;
        }

        Object GetHookType()
        {
            return hook;
        }
    }
}
