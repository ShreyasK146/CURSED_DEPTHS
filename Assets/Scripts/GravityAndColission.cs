//not in use but it was used to check if item dropped hits the ground 
using UnityEngine;

public class GravityAndColission : MonoBehaviour
{
    public LayerMask mask;

    private void FixedUpdate()
    {
        CheckGroundColission();
    }

    private void CheckGroundColission()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out RaycastHit hit, Mathf.Infinity, mask)) 
        {
            //Debug.Log("Object is colliding with the ground.");
        }
    }
}
