using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sensetivityX;
    public float sensetivityY;
    public float moveSpeed;
    public Camera headCamera;
    
    CharacterController characterController;
    float xRotation;
    float yRotation;
    Vector3 velocity;

    void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensetivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensetivityY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f); 
        headCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void FixedUpdate() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        var moveDir = transform.forward * moveZ + transform.right * moveX;
        moveDir = moveDir.normalized * moveSpeed * Time.deltaTime;
        characterController.Move(moveDir);
    }
}
