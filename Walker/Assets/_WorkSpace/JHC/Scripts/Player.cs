using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 moveInput = Vector2.zero;
    private Vector2 cameraMoveInput;
    private float maxRotationY = 88.0f;

    public float moveSpeed = 6.0f;
    public float mouseSensitivity = 0.2f;
    public float jumpPower = 5.0f;
    private bool isJump = false;
    public GameObject playerCameraX;
    public GameObject playerCameraY;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.identity;       // 시작할 때 항상 정면을 보도록 초기화
        isJump = false;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();               // WASD 또는 방향키의 입력을 받고 저장한다
    }

    private void OnLook(InputValue value)
    {
        cameraMoveInput += value.Get<Vector2>() * mouseSensitivity;         // 마우스, 또는 게임패드의 움직임을 받고 저장한다
        cameraMoveInput.y = Mathf.Clamp(cameraMoveInput.y, -maxRotationY, maxRotationY);
    }

    private void OnJump(InputValue value)               // 점프 감지
    {
        if(! isJump)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveInput.x * moveSpeed * Time.deltaTime, 0, moveInput.y * moveSpeed * Time.deltaTime);

        playerCameraX.transform.localRotation = Quaternion.AngleAxis(cameraMoveInput.x, Vector3.up);
        playerCameraY.transform.localRotation = Quaternion.AngleAxis(cameraMoveInput.y, Vector3.left);


        // rb.AddRelativeForce(moveInput.x * moveSpeed * Time.deltaTime, 0, moveInput.y * moveSpeed * Time.deltaTime);
        // 이동 후 바로 멈추지 않고 미끄러짐
    }
}
