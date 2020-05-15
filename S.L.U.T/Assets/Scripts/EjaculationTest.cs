using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjaculationTest : MonoBehaviour
{
    public Rigidbody Babies;
    public GameObject Target;
    public LayerMask Layer;
    public Transform shootPoint;

    private Camera cam;

    
    void Start()
    {
        cam = Camera.main;
    }

   
    void Update()
    {
        Ejaculate();
    }

    void Ejaculate() //Marina
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit, 100f, Layer))
        {
            Target.SetActive(true);
            Target.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 VectorOfVelocity = CalculateVelocity(hit.point, shootPoint.position, 1f);
            transform.rotation = Quaternion.LookRotation(VectorOfVelocity);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody Obj = Instantiate(Babies, shootPoint.position, Quaternion.identity);
                Obj.velocity = VectorOfVelocity;
            }
        }

        else
        {
            Target.SetActive(false);
        }
    }

    Vector3 CalculateVelocity (Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float verticalDistance = distance.y;
        float horizontalDistance = distanceXZ.magnitude;

        float verticalVelocity = verticalDistance / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        float horizontalVelocity = horizontalDistance / time;

        Vector3 result = distanceXZ.normalized;
        result *= horizontalVelocity;
        result.y = verticalVelocity;

        return result;
    }
}
