using UnityEngine;

public class TopSagaHareket : MonoBehaviour
{
    public float hiz = 2f; // �stedi�in gibi ayarlayabilirsin

    void Update()
    {
        // Sadece X ekseninde ileriye do�ru hareket ettir
        transform.position += new Vector3(hiz * Time.deltaTime, 0, 0);
    }
}
