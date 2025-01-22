using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
	public Camera headCamera;
	public float mouseSensetivity;
	public float maxSpeed;
	public float acceleration;
	
	private CharacterController cc;
	Vector2 headRotation;
	Vector3 velocity;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		headRotation.x = transform.localRotation.x;
		headRotation.y = headCamera.transform.localRotation.y;
		cc = GetComponent<CharacterController>();
	}

	void Update()
	{
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensetivity;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensetivity;

		headRotation.x -= mouseY;
		headRotation.y += mouseX;
		headRotation.x = Mathf.Clamp(headRotation.x, -90f, 90f);

		transform.localRotation = Quaternion.Euler(0.0f, headRotation.y, 0.0f);
		headCamera.transform.localRotation = Quaternion.Euler(headRotation.x, 0f, 0f);
	}

	void FixedUpdate()
	{
		float vAxis = Input.GetAxisRaw("Vertical");
		float hAxis = Input.GetAxisRaw("Horizontal");

		velocity.x = Mathf.Lerp(velocity.x, maxSpeed*hAxis, acceleration);
		velocity.z = Mathf.Lerp(velocity.z, maxSpeed*vAxis, acceleration);

		cc.Move(transform.right * velocity.x + transform.forward * velocity.z);
	}
}
