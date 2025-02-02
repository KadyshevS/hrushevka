using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{
	[Header("Essentials")]
	public CharacterController cc;
	public Camera headCamera;
	public float mouseSensetivity;
	[Header("Movement")]
	public float maxSpeed;
	public float moveAcceleration;
	[Header("Jumping")]
	public float jumpHeight;
	public float jumpAcceleration;
	public float jumpDuration;
	[Header("Falling")]
	public float fallAcceleration;
	public float fallMaxSpeed;
	public float distanceToGround;
	[Header("Crouching")]
	public float crouchLength;
	public float crouchDuration;
	public float crouchAcceleration;
	[Header("Rising")]
	public float raiseDuration;
	public float raiseAcceleration;
	
	Vector2 headRotation;
	Vector3 velocity;
	float jumpTime;
	float crouchTime;
	float raiseTime;
	float cameraCrouchYPos;
	bool isJumping;
	bool isCrouching;
	bool isCrouched;
	bool isRaising;
	bool isRaised;
	bool IsGround;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		headRotation.x = transform.localRotation.x;
		headRotation.y = headCamera.transform.localRotation.y;
		cameraCrouchYPos = headCamera.transform.localPosition.y;
		isJumping = false;
		isCrouching = false;
		IsGround = false;
		isCrouched = false;
		isRaising = false;
		isRaised = true;
		raiseTime = 0f;
		jumpTime = 0f;
	}

	void Update()
	{
		IsGround = isGrounded;

		float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensetivity;
		float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensetivity;

		headRotation.x -= mouseY;
		headRotation.y += mouseX;
		headRotation.x = Mathf.Clamp(headRotation.x, -90f, 90f);

		transform.localRotation = Quaternion.Euler(0.0f, headRotation.y, 0.0f);
		headCamera.transform.localRotation = Quaternion.Euler(headRotation.x, 0f, 0f);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping) {
			isJumping = true;
		}

		if (Input.GetKeyDown(KeyCode.C) && isGrounded) {
			if (!isCrouching && !isCrouched) {
				isRaised = false;
				isRaising = false;
				isCrouching = true;
			}
			else {
				isCrouched = false;
				isCrouching = false;
				isRaising = true;
			}
		}

		if (isJumping) {
			jumpTime += Time.deltaTime;
			if (jumpTime >= jumpDuration)
				isJumping = false;
		}
		else {
			jumpTime = 0f;
		}
		if (isCrouching) {
			crouchTime += Time.deltaTime;
			if (crouchTime >= crouchDuration) {
				isCrouching = false;
				isCrouched = true;
			}
		}
		else {
			crouchTime = 0f;
		}

		if (isRaising) {
			raiseTime += Time.deltaTime;
			if (raiseTime >= raiseDuration) {
				isRaising = false;
				isRaised = true;
			}
		}
		else {
			raiseTime = 0f;
		}
	}

	void FixedUpdate()
	{
		float vAxis = Input.GetAxisRaw("Vertical");
		float hAxis = Input.GetAxisRaw("Horizontal");

		if (isJumping) {
			velocity.y = jumpHeight;
		}
		else {
			if (!cc.isGrounded) {
				velocity.y = Mathf.Lerp(velocity.y, -fallMaxSpeed, fallAcceleration);
			}
			else {
				velocity.y = 0f;
			}
		}
		
		if (isCrouching) {
			headCamera.transform.localPosition = new Vector3 (
				headCamera.transform.localPosition.x,
				Mathf.Lerp(headCamera.transform.localPosition.y, cameraCrouchYPos-crouchLength, crouchAcceleration),
				headCamera.transform.localPosition.z
			);
		}
		if (isCrouched) {
			headCamera.transform.localPosition = new Vector3 (
				headCamera.transform.localPosition.x,
				cameraCrouchYPos-crouchLength,
				headCamera.transform.localPosition.z
			);
		}
		if (isRaising) {
			headCamera.transform.localPosition = new Vector3 (
				headCamera.transform.localPosition.x,
				Mathf.Lerp(headCamera.transform.localPosition.y, cameraCrouchYPos, raiseAcceleration),
				headCamera.transform.localPosition.z
			);
		}
		if (isRaised) {
			headCamera.transform.localPosition = new Vector3 (
				headCamera.transform.localPosition.x,
				cameraCrouchYPos,
				headCamera.transform.localPosition.z
			);
		}

		velocity.x = Mathf.Lerp(velocity.x, maxSpeed*hAxis, moveAcceleration);
		velocity.z = Mathf.Lerp(velocity.z, maxSpeed*vAxis, moveAcceleration);

		cc.Move(
			transform.right * velocity.x + 
			transform.forward * velocity.z +
			transform.up * velocity.y
		);
	}

	bool isGrounded {
		get {
			Vector3 groundPos = transform.localPosition - new Vector3(0f, cc.height/2f, 0f);
			Vector3 groundDir = Vector3.down;
			RaycastHit rhit;
			if (Physics.Raycast(groundPos, groundDir, out rhit, distanceToGround)) {
				Debug.Log("Grounded on: " + rhit.transform.name);
				return true;
			}
			return false;
		}
	}

	void OnDrawGizmos()
	{
		Vector3 groundPos = transform.localPosition - new Vector3(0f, cc.height/2f, 0f);
		Vector3 groundDir = Vector3.down * distanceToGround;
		Gizmos.DrawRay(groundPos, groundDir);
	}
}
