using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("키 바인딩")]
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode grabKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode interactionKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode runKey = KeyCode.LeftAlt;

    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 10f; 
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController controller;
    private float verticalVelocity;  // 수직 속도(중력 누적용)

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(upKey))
            z += 1f;
        if (Input.GetKey(downKey))
            z -= 1f;
        if (Input.GetKey(leftKey))
            x -= 1f;
        if (Input.GetKey(rightKey))
            x += 1f;

        if (Input.GetKeyDown(runKey))
            moveSpeed = 20f;

        if (Input.GetKey(grabKey))
        {
            Debug.Log("그랩 키 누름");
        }

        if(Input.GetKeyDown(interactionKey))
        {
            Debug.Log("상호작용 키 누름");
        }

        Vector3 moveInput = new Vector3(x, 0f, z).normalized;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = moveInput * moveSpeed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);

        if (moveInput.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}