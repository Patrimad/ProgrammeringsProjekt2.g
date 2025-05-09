using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;

    private InputSystem_Actions inputSystemActions;
    private Vector3 _input;
    private Rigidbody rb;

    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        inputSystemActions.Enable();
    }

    private void OnDisable()
    {
        inputSystemActions.Disable();
    }

    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void GatherInput()
    {
        Vector2 input = inputSystemActions.Player.Move.ReadValue<Vector2>().normalized;
        _input = new Vector3(input.x, 0, input.y);
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;
        
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 rotatedInput = isoMatrix.MultiplyPoint3x4(_input);

        Quaternion targetRotation = Quaternion.LookRotation(rotatedInput, Vector3.up);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    private void Move()
    {
        if (_input == Vector3.zero) return;
        
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 rotatedInput = isoMatrix.MultiplyPoint3x4(_input);

        Vector3 moveDirection = rotatedInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }
}