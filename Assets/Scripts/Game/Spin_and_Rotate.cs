using UnityEngine;
using System.Collections;

public class Spin_and_Rotate : MonoBehaviour {

    public float speed = 10f;
    public float rotSpeed = 60f;

    void Update()
    {
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
