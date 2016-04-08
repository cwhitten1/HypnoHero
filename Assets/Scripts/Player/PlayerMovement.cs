using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
    public float stealthDistance = 5f;
    public bool isStealth = false;
	private float rotationSmoothing = 10f;
    

	Vector3 movement;
	Animation anim;
	Rigidbody playerRigidBody;
    Transform player;               // Reference to the player's position.
    GameObject[] stealthObjects;

    int floorMask;
	float camRayLength = 100f;

	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animation> ();
		playerRigidBody = GetComponent<Rigidbody> ();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        stealthObjects = GameObject.FindGameObjectsWithTag("Stealth");
    }

	void FixedUpdate(){
		float h = Input.GetAxisRaw("Horizontal"); //Raw axis returns either -1,0,1
		float v = Input.GetAxisRaw("Vertical");


        Move (h, v);
		DiscreteTurning (h, v);
		//Turning ();
		//Animating (h, v);
	}

	void Update(){
        foreach (var obj in stealthObjects)
        {
            float objX = obj.transform.position.x,
            objZ = obj.transform.position.z,

            distanceFromPlayer = Mathf.Pow(player.position.x - objX, 2) + Mathf.Pow(player.position.z - objZ, 2);
            distanceFromPlayer = Mathf.Sqrt(distanceFromPlayer);

            if (distanceFromPlayer <= stealthDistance)
            {
                Debug.Log("Player is stealth!");
                isStealth = true;
                return;
            }
            else {
                isStealth = false;
                Debug.Log("Player isn't stealth!");
            }
        }

		float h = Input.GetAxisRaw("Horizontal"); //Raw axis returns either -1,0,1
		float v = Input.GetAxisRaw("Vertical"); 

		if (h != 0 || v != 0)
			anim.CrossFade ("Walk");
		else
			anim.CrossFade ("Wait");

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
		
}
