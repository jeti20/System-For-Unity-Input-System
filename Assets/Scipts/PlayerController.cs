using UnityEngine;

namespace Pairdot
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;

        private Vector2 moveDirection;

        private bool isJumping;

        private void Start()
        {
            input.MoveEvent += HandleMove;

            input.JumpEvent += HandleJump;
            input.JumpCancelledEvent += HandleCancelledJump;         
        }

        private void Update()
        {
            Move();
            Jump();
        }

        private void HandleMove(Vector2 dir)
        {
            moveDirection = dir;
        }

        private void HandleJump()
        {
            isJumping = true;
        }

        private void HandleCancelledJump()
        {
            isJumping = false;
        }

        private void Move()
        {
            if (moveDirection == Vector2.zero)
            {
                return;
            }

            transform.position += new Vector3(x: moveDirection.y, y:0, z: -moveDirection.x) * (speed * Time.deltaTime);
        }

        private void Jump()
        {
            if (isJumping)
            {
                transform.position += new Vector3(x: 0, y: 1, z: 0) * (jumpSpeed * Time.deltaTime);
            }
        }

    }
}
