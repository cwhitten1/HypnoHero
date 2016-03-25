using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	private float rotationSmoothing = 10f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;
	int floorMask;
	float camRayLength = 100f;

	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		float h = Input.GetAxisRaw("Horizontal"); //Raw axis returns either -1,0,1
		float v = Input.GetAxisRaw("Vertical"); 

		Move (h, v);
		DiscreteTurning (h, v);
		//Turning ();
		//Animating (h, v);
	}

	void Move(float h, float v){
		movement.Set (h, 0, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidBody.MovePosition (transform.position + movement);
	}

	void Turning(){
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody.MoveRotation (newRotation);
		}
	}

	void DiscreteTurning(float h, float v){
		float rotationY = 0;

		//Sorry for the conditional logic block
		if (v == 1) {
			if (h == 0)
				rotationY = 0;
			else if (h == 1)
				rotationY = 45;
			else if (h == -1)
				rotationY = -45;
		} 
		else if (v == -1) {
			
			if (h == 0)
				rotationY = 180;
			else if (h == 1)
				rotationY = 135;
			else if (h == -1)
				rotationY = -135;
		} 
		else if (v == 0){
			if (h == 0)
				return;
			else if (h == 1)
				rotationY = 90;
			else if (h == -1)
				rotationY = -90;
		}
			
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, rotationY, 0), Time.deltaTime * rotationSmoothing);
	}

	void Animating(float h, float v){
		bool walking = h != 0f || v != 0f;

		anim.SetBool ("IsWalking", walking);
	}
}
