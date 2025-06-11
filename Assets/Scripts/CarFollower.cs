using UnityEngine;

public class CarFollower : MonoBehaviour
{
    public UDPReceiver udpReceiver; // Inspector’dan baðlayacaksýn
    public float speed = 5f;
    public float rotationSpeed = 5f;

    // Ekran pikselini sahneye dönüþtürmek için:
    public float screenToWorldScale = 0.05f; // Kendi sahnen için gerekirse deðiþtir
    public float cameraOffsetY = 0; // Aracýn yer yüksekliði (genellikle 0)

    void Update()
    {
        if (udpReceiver == null) return;
        Vector3 target = udpReceiver.receivedTarget;

        // Gelen cx/cy ekran ortasýna göre normalize edilir (320x240 için 160,120)
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
