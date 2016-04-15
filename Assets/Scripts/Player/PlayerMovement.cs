using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float stealthDistance = 5f;

    public Color normColor = new Color(64f, 64f, 64f, 255f);
    public Color stealthColor = new Color(208f, 208f, 208f, 255f);

    public bool isStealth = false;
    private float rotationSmoothing = 10f;
    

    public float stealthConfidenceSubtractFactor = 2; /// <summary>
    /// The amount of confidence subtracted per second when in stealth.
    /// </summary>

    float fullConfidenceHealthAddFactor = 0.5f; /// <summary>
                                               /// The amount of heath add per second when confidence is 100.
                                               /// </summary>

    Vector3 movement;
    Animation anim;

    Rigidbody playerRigidBody;
    Transform player;
    SkinnedMeshRenderer playerModel;

    GameObject[] stealthObjects;
    Game game;
    int floorMask;
    float camRayLength = 100f;

    int oldDamagePerHit; /// <summary>
    /// The damage to reset to when the player leaves stealth.
    /// </summary>

    void Awake()
    {
        game = Game.GetGame();
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animation>();
        playerRigidBody = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<SkinnedMeshRenderer>();
        stealthObjects = GameObject.FindGameObjectsWithTag("Stealth");

        oldDamagePerHit = player.GetComponent<PlayerAttacking>().damagePerHit;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); //Raw axis returns either -1,0,1
        float v = Input.GetAxisRaw("Vertical");


        Move(h, v);
        DiscreteTurning(h, v);
        //Turning ();
        //Animating (h, v);
    }

    void Update()
    {
        if (Game.GetGame().GetConfidence() == 100)
            Game.GetGame().AddScare(Time.deltaTime * fullConfidenceHealthAddFactor);
                

        foreach (var obj in stealthObjects)
        {
            float objX = obj.transform.position.x,
            objZ = obj.transform.position.z,

            distanceFromPlayer = Mathf.Pow(player.position.x - objX, 2) + Mathf.Pow(player.position.z - objZ, 2);
            distanceFromPlayer = Mathf.Sqrt(distanceFromPlayer);

            if (distanceFromPlayer <= stealthDistance)
            {
                //Debug.Log("Player is stealth!");

                //Change Colors
                isStealth = true;
                SetStealth(isStealth);

                Game.GetGame().SubtractConfidence(stealthConfidenceSubtractFactor * Time.deltaTime);
                
                this.GetComponent<PlayerAttacking>().damagePerHit = 0;

                return;
            }
            else
            {
                //Debug.Log("Player isn't stealth!");
                this.GetComponent<PlayerAttacking>().damagePerHit = oldDamagePerHit;

                isStealth = false;
                SetStealth(isStealth);
            }
        }

        
        float h = Input.GetAxisRaw("Horizontal"); //Raw axis returns either -1,0,1
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
            anim.CrossFade("Walk");
        else
            anim.CrossFade("Wait");

    }
    void SetStealth(bool stealth)
    {
        playerModel.sharedMaterials[0].SetColor("_Color", stealth ? stealthColor : normColor);
        playerModel.sharedMaterials[1].SetColor("_Color", stealth ? stealthColor : normColor);

        isStealth = stealth;
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void DiscreteTurning(float h, float v)
    {
        float rotationY = 0;

        //Sorry for the conditional logic block
        if (v == 1)
        {
            if (h == 0)
                rotationY = 0;
            else if (h == 1)
                rotationY = 45;
            else if (h == -1)
                rotationY = -45;
        }
        else if (v == -1)
        {

            if (h == 0)
                rotationY = 180;
            else if (h == 1)
                rotationY = 135;
            else if (h == -1)
                rotationY = -135;
        }
        else if (v == 0)
        {
            if (h == 0)
                return;
            else if (h == 1)
                rotationY = 90;
            else if (h == -1)
                rotationY = -90;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotationY, 0), Time.deltaTime * rotationSmoothing);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MediKit"))
        {
            if (game.GetScare()<100)
            {
                game.AddScare(10);
				Destroy (other.gameObject);
            }

                
        }
    }
}
