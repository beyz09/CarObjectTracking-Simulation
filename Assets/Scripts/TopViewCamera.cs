using UnityEngine;

public class TopDownFollow : MonoBehaviour
{
    public Transform target;   // Takip edilecek araç
    public Vector3 offset = new Vector3(0, 20, 0); // Yukarýdan bakýþ, mesafe ve yükseklik

    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Takip pozisyonu (sadece pozisyonu takip et, dönüþü deðil)
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Aþaðýya doðru bak
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
