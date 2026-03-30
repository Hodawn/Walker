using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 cameraMoveInput;
    public float moveSpeed = 6.0f;
    public float cameraSpeed = 30.0f;
    public float jumpPower = 250.0f;
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
        cameraMoveInput = value.Get<Vector2>();         // 마우스, 또는 게임패드의 움직임을 받고 저장한다
    }

    private void OnJump(InputValue value)               // 점프 감지
    {
        if(! isJump)
        {
            rb.AddForce(transform.up * jumpPower);
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

        // rb.AddRelativeForce(moveInput.x * moveSpeed * Time.deltaTime, 0, moveInput.y * moveSpeed * Time.deltaTime);
        // 이동 후 바로 멈추지 않고 미끄러짐

        playerCameraX.transform.Rotate(0, cameraMoveInput.x * cameraSpeed * Time.deltaTime, 0);
        playerCameraY.transform.Rotate(-cameraMoveInput.y * cameraSpeed * Time.deltaTime, 0, 0);
        // Debug.Log(playerCameraY.transform.rotation.x);

        // -90 ~90 -0.7~0.7
        // 90 180 -180 -90 -0.7~0.7  ??

        if (playerCameraY.transform.rotation.x > 0.7f || playerCameraY.transform.rotation.x < -0.7f)
        {
            playerCameraY.transform.Rotate(cameraMoveInput.y * cameraSpeed * Time.deltaTime, 0, 0);
        }
    }
}
