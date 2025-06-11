using UnityEngine;

public class CarFollowSphere : MonoBehaviour
{
    public Transform sphere;           // Inspector'dan atayacaksýn!
    public float speed = 5f;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (sphere == null) return;

        Vector3 hedef = new Vector3(sphere.position.x, 0, sphere.position.z);

        if (Vector3.Distance(transform.position, hedef) > 0.5f)
        {
            Vector3 direction = (hedef - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
