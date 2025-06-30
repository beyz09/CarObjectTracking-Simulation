using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Yön giriþlerini al
        float moveX = Input.GetAxis("Horizontal"); // A/D veya Sol/Sað ok
        float moveZ = Input.GetAxis("Vertical");   // W/S veya Yukarý/Aþaðý ok

        // Pozisyonu güncelle
        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
