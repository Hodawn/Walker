using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 cameraMoveInput;
    public float moveSpeed = 7.0f;
    public float cameraSpeed = 30.0f;
    public GameObject playerCameraX;
    public GameObject playerCameraY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        cameraMoveInput = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveInput.x * moveSpeed * Time.deltaTime, 0, moveInput.y * moveSpeed * Time.deltaTime);
        
        playerCameraX.transform.Rotate(0, cameraMoveInput.x * cameraSpeed * Time.deltaTime, 0);
        playerCameraY.transform.Rotate(-cameraMoveInput.y * cameraSpeed * Time.deltaTime, 0, 0);
    }
}
