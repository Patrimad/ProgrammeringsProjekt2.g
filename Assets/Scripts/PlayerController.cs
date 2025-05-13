using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    
    public int maxBlood = 100;
    public int currentBlood;
    public TextMeshProUGUI bloodText;

    public BarScript healthBar;
    public BarScript bloodBar;

    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public int dashBloodCost = 10;

    private float lastDashTime;
    private float dashTimeRemaining;
    private bool isDashing;
    private Vector3 dashDirection;

    private Vector3 _input;
    private Rigidbody rb;

    void Start()
    {
        currentHealth = maxHealth;
        currentBlood = maxBlood;
        healthText.text = $"{currentHealth}/{maxHealth}";
        healthBar.SetMaxhealth(maxHealth);
        bloodBar.SetMaxhealth(maxBlood);
        bloodText.text = $"{currentBlood}/{maxBlood}";
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    public void GiveHealth(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
    public void GiveBlood(int amount)
    {
        currentBlood += amount;
        bloodBar.SetMaxhealth(currentBlood);
        bloodText.text = $"{currentBlood}/{maxBlood}";
    }

    public void TakeBlood(int amount)
    {
        currentBlood -= amount;
        bloodBar.SetMaxhealth(currentBlood);
        bloodText.text = $"{currentBlood}/{maxBlood}";
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        GatherInput();
        TryDash();
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void GatherInput()
    {
        Vector2 input = Keyboard.current != null ? new Vector2(
            (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
            (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0)
        ).normalized : Vector2.zero;

        _input = new Vector3(input.x, 0, input.y);
    }

    private void TryDash()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame &&
            Time.time >= lastDashTime + dashCooldown &&
            _input != Vector3.zero &&
            currentBlood >= dashBloodCost)
        {
            isDashing = true;
            dashTimeRemaining = dashDuration;
            lastDashTime = Time.time;
            dashDirection = _input.normalized; // Lock direction
            currentBlood -= dashBloodCost;
            bloodBar.SetHealth(currentBlood);
            bloodText.text = $"{currentBlood}/{maxBlood}";
        }
    }

    private void Move()
    {
        if (_input == Vector3.zero && !isDashing) return;

        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 rotatedInput = isDashing ? isoMatrix.MultiplyPoint3x4(dashDirection) : isoMatrix.MultiplyPoint3x4(_input);
        Vector3 moveDirection = rotatedInput;

        if (isDashing)
        {
            rb.MovePosition(rb.position + moveDirection * dashSpeed * Time.fixedDeltaTime);
            dashTimeRemaining -= Time.fixedDeltaTime;
            if (dashTimeRemaining <= 0f)
                isDashing = false;
        }
        else
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 rotatedInput = isoMatrix.MultiplyPoint3x4(_input);
        Quaternion targetRotation = Quaternion.LookRotation(rotatedInput, Vector3.up);
        rb.rotation = targetRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing)
        {
            isDashing = false;
            dashTimeRemaining = 0f;
        }
    }
}