using UnityEngine;

namespace cooperplant4
{
    public class Player : MonoBehaviour
    {
        public int speed = 500;

        Rigidbody2D rb;
        bool isFrameToChop = false;
        Timer chopTimer = new Timer()
        {
            interval = 1f,
            time = 0f
        };

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                chopTimer.Restart();
            else if (Input.GetKey(KeyCode.Space))
                isFrameToChop = chopTimer.EvaluateFrame();
            else if (Input.GetKeyUp(KeyCode.Space))
                isFrameToChop = false;

            ApplyMovementVelocity();
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (isFrameToChop)
            {
                Tree tree = collision.gameObject.GetComponent<Tree>();
                if (tree)
                {
                    Score score = tree.Chop();
                    Debug.Log(score);
                }
            }
        }

        void ApplyMovementVelocity()
        {
            Vector2 vel = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
                vel.x -= Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.D))
                vel.x += Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.S))
                vel.y -= Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.W))
                vel.y += Time.deltaTime * speed;
            rb.velocity = vel;
        }
    }
}
