using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;

    private InputSystem_Actions inputSystemActions;
    private Vector3 _input;
    private CharacterController _characterController;

    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        _characterController = GetComponent<CharacterController>();
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
        Look();
        Move();
    }

    private void GatherInput()
    {
        Vector2 input = inputSystemActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(input.x, 0, input.y);
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        // Apply 45° isometric rotation to movement input
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 rotatedInput = isoMatrix.MultiplyPoint3x4(_input);

        // Instantly face the direction
        Quaternion targetRotation = Quaternion.LookRotation(rotatedInput, Vector3.up);
        transform.rotation = targetRotation;
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _input.magnitude * moveSpeed * Time.deltaTime;
        _characterController.Move(moveDirection);
    }
}