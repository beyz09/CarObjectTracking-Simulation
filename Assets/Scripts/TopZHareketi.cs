using UnityEngine;

public class TopSagaHareket : MonoBehaviour
{
    public float hiz = 2f; // Ýstediðin gibi ayarlayabilirsin

    void Update()
    {
        // Sadece X ekseninde ileriye doðru hareket ettir
        transform.position += new Vector3(hiz * Time.deltaTime, 0, 0);
    }
}
