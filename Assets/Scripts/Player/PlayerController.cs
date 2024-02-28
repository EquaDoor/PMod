using UnityEngine;

public class PlayerController : Damageable
{
	private CharacterController controller;
	[SerializeField] private float speed = 12f;
	[SerializeField] private float jumpForce = 3f;
	[SerializeField] private float gravity = -9.8f;
    private Vector3 velocity;

	[SerializeField] private Transform gCheck;
	[SerializeField] private float gDist = 0.4f;
	[SerializeField] private LayerMask gMask;
    private bool isGrounded;


	private void Start()
    {
        // создание ссылки на контроллер при старте игры автоматически
        controller = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(gCheck.position, gDist, gMask);

        if(isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal"); // нажатие A,D
        float z = Input.GetAxis("Vertical"); // нажатие W,S
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) 
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public override void TakeDamage(float _damage) => base.TakeDamage(_damage);
    public override void Die() => UnityEngine.SceneManagement.SceneManager.LoadScene(0);
}