using UnityEngine;

public class CarManualControl : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 80f;

    void Update()
    {
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime; // W/S veya yukarý/aþaðý ok
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime; // A/D veya sað/sol ok

        // Ýleri/geri hareket
        transform.Translate(Vector3.forward * move);

        // Yana döndürme
        transform.Rotate(Vector3.up, turn);
    }
}
