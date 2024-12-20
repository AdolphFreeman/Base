using UnityEngine;

namespace AFLibrary.Action
{
    public class CommonAction2D
    {
        public static void MoveWithAcceleration2D(Rigidbody2D rb, bool pressKey, float moveDir, float faceDir, float maxSpeed, 
            float maxAcceleration, float maxDeceleration, float maxTurnSpeed)
        {
            Vector2 desiredVelocity = new Vector2(moveDir, 0) * Mathf.Max(maxSpeed, 0);
            Vector2 velocity = rb.velocity;
            float maxSpeedChange;

            if (pressKey)
            {
                if ((int)Mathf.Sign(moveDir) != (int)Mathf.Sign(faceDir))
                    maxSpeedChange = maxTurnSpeed * Time.deltaTime;
                else
                    maxSpeedChange = maxAcceleration * Time.deltaTime;
            }
            else
                maxSpeedChange = maxDeceleration * Time.deltaTime;

            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            rb.velocity = velocity;
        }

        public static void MoveWithoutAcceleration(Rigidbody2D rb, bool pressKey, int moveDir, float maxSpeed)
        {
            Vector2 desiredVelocity = new Vector2(moveDir, 0) * Mathf.Max(maxSpeed, 0);
            
            if (pressKey)
                rb.velocity = new Vector2(desiredVelocity.x, rb.velocity.y);
        }

        public static void Jump(Rigidbody2D rb, float jumpForce)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        public static void ChangeDirection(SpriteRenderer renderer, float moveDirection, float faceDirection, out float faceDir)
        {
            float dir = faceDirection;
            if ((int)moveDirection != (int)faceDirection & moveDirection != 0)
            {
                renderer.flipX = !renderer.flipX;
                dir *= -1;
            }

            faceDir = dir;
        }
    }
}
