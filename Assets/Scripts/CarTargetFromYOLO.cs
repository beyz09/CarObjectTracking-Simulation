using UnityEngine;

public class CarTargetFromYOLO : MonoBehaviour
{
    public UDPReceiver udpReceiver;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    // Kamera çözünürlüðünün merkezi — Python tarafýnda ayarla: 320x240 ise 160,120
    public float ekranOrtasiX = 160;
    public float ekranOrtasiY = 120;
    public float worldScale = 0.05f; // Deneyerek ayarla

    void Update()
    {
        if (udpReceiver == null) return;

        float cx = udpReceiver.receivedTarget.x;
        float cy = udpReceiver.receivedTarget.y;

        // Sadece hedef (cx,cy) mantýklýysa harekete izin ver
        if (cx <= 0.1f && cy <= 0.1f)
        {
            Debug.Log("Beklemede: Henüz hedef algýlanmadý. Araç sabit.");
            return;
        }

        float hedefX = (cx - ekranOrtasiX) * worldScale;
        float hedefZ = (cy - ekranOrtasiY) * worldScale;
        Vector3 hedef = new Vector3(hedefX, 0, hedefZ);

        Debug.Log($"Hedef var! UDP Target: ({cx:0.00}, {cy:0.00}) => Unity hedef: ({hedef.x:0.00}, {hedef.z:0.00})");

        Debug.DrawLine(transform.position, hedef, Color.red);

        if (Vector3.Distance(transform.position, hedef) > 0.5f)
        {
            Vector3 direction = (hedef - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
