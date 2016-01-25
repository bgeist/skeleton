using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6;

	Vector3 pos;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLen = 100;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move (float h, float v)
	{
		pos.Set (h, 0, v);
		pos = pos.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + pos);
	}

	void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLen, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v) {
		bool walking = h != 0 || v != 0;

		print("walking=" + walking);

		anim.SetBool ("IsWalking", walking);
	}
}