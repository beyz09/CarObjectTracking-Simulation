using UnityEngine;

public class CarFollower : MonoBehaviour
{
    public UDPReceiver udpReceiver; // Inspector�dan ba�layacaks�n
    public float speed = 5f;
    public float rotationSpeed = 5f;

    // Ekran pikselini sahneye d�n��t�rmek i�in:
    public float screenToWorldScale = 0.05f; // Kendi sahnen i�in gerekirse de�i�tir
    public float cameraOffsetY = 0; // Arac�n yer y�ksekli�i (genellikle 0)

    void Update()
    {
        if (udpReceiver == null) return;
        Vector3 target = udpReceiver.receivedTarget;

        // Gelen cx/cy ekran ortas�na g�re normalize edilir (320x240 i�in 160,120)
        Vector3 sceneTarget = new Vector3(
            (target.x - 160) * screenToWorldScale,
            cameraOffsetY,
            (target.y - 120) * screenToWorldScale
        );

        if (Vector3.Distance(transform.position, sceneTarget) > 0.5f)
        {
            Vector3 direction = (sceneTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
