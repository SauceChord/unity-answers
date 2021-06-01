using System.Linq;
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
        public int spawnAttempts = 16;
        public Circle spawnCircle = new Circle(3f);

        void Update()
        {
            SpawnHooks();
        }

        void OnDrawGizmosSelected()
        {
            for (int i = 0; i < spawnAttempts; ++i)
            {
                Vector3 from = spawnCircle.UnitToPoint((float)i / spawnAttempts);
                Vector3 to = spawnCircle.UnitToPoint((float)(i + 1) / spawnAttempts);

                Gizmos.DrawLine(from + transform.position, to + transform.position);
                Gizmos.DrawWireSphere(from + transform.position, 0.1f);
            }
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
            Collider2D[] collisions = Physics2D.OverlapCircleAll(player.position, spawnCircle.radius, mask);
            int nearby = collisions.Length;
            int numHooksToSpawn = totalHooks - nearby;
            return numHooksToSpawn;
        }

        void AttemptToSpawnHook()
        {
            int[] spawnIndices = GetRandomSpawnIndices();
            foreach (int i in spawnIndices)
            {
                Vector3 hookPos = spawnCircle.UnitToPoint((float)i / spawnIndices.Length);
                hookPos += transform.position;

                if (!IsColliding(hookPos))
                {
                    Instantiate(GetHookType(), hookPos, Quaternion.identity);
                    break;
                }
            }
        }

        int[] GetRandomSpawnIndices()
        {
            return Enumerable
                .Range(0, spawnAttempts)
                .OrderBy(i => Random.Range(int.MinValue, int.MaxValue))
                .ToArray();
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
            Collider2D[] nearbyHooks = Physics2D.OverlapCircleAll(hookPos, spawnCircle.radius, mask);
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
