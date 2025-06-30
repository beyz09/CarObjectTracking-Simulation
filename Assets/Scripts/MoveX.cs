using UnityEngine;

public class MoveX : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Sadece X ekseninde hareket ettirir (pozitif X için saða, negatif X için sola)
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        // veya
        // transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
