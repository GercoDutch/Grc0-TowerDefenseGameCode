using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector2 horizontalBounds, verticalBounds;

    private float _x, _y;
    [SerializeField] private float moveSpeed;

    private bool isMoving;
    [SerializeField] private bool canMove;

    private void Update()
    {
        if(canMove)
        {
            _x = Input.GetAxisRaw("Horizontal");
            _y = Input.GetAxisRaw("Vertical");

            if (transform.position.z < horizontalBounds.x && _x < 0)    _x = 0;
            if (transform.position.z > horizontalBounds.y && _x > 0)    _x = 0;
            if (transform.position.x < verticalBounds.x && _y > 0)      _y = 0;
            if (transform.position.x > verticalBounds.y && _y < 0)      _y = 0;

            isMoving = _x != 0 || _y != 0;
        }
    }

    private void FixedUpdate()
    {
        if(isMoving && canMove)
            transform.position += (transform.right * _x + Vector3.right * -_y) * moveSpeed * Time.deltaTime;
    }
}