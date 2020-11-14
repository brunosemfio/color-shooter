using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Components

    private Rigidbody2D _rb;

    #endregion

    #region Inspector

    [SerializeField] private float moveSpeed;

    [SerializeField] private float turnSpeed;

    #endregion

    #region Private

    private Vector2 _dir;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // _dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _dir = Joystick.current.stick.ReadValue();
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.right * moveSpeed;
        _rb.angularVelocity = 0f;

        FaceDirection();
    }

    private void FaceDirection()
    {
        if (!(_dir.sqrMagnitude > 0)) return;

        var angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        var newAngle = Mathf.MoveTowardsAngle(_rb.rotation, angle, turnSpeed * Time.fixedDeltaTime);

        _rb.MoveRotation(newAngle);
    }
}