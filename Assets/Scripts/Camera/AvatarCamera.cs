using UnityEngine;
using System.Collections;

public class AvatarCamera : MonoBehaviour {

    public Transform target;
    public float smoothing = 5.0f;
    Vector3 offset;
   


    void Start()
    {
        offset = transform.position - target.position;
    
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        float height = 2f * GetComponent<Camera>().orthographicSize;
        float width = height * GetComponent<Camera>().aspect;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        if (transform.position.x < width)
            transform.RotateAround(target.position, Vector3.up, 0);

        
    }

}
