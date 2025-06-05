using UnityEngine;

public class AracOtonom : MonoBehaviour
{
    public GameObject veriAlici;
    public Camera aracKamerasi;
    public float hiz = 5f;

    void Update()
    {
        if (veriAlici == null || aracKamerasi == null) return;

        VeriAlici alici = veriAlici.GetComponent<VeriAlici>();
        Vector2 hedefEkranPoz = alici.hedefPozisyon;

        // Ekran koordinatlarını dünya koordinatına çevir
        Vector3 hedefWorldPoz = aracKamerasi.ScreenToWorldPoint(
            new Vector3(hedefEkranPoz.x, hedefEkranPoz.y, 10f)  // Z mesafesi kameradan hedefe olan uzaklıktır
        );

        hedefWorldPoz.y = transform.position.y; // Y düzlemi sabit kalsın

        Vector3 yön = (hedefWorldPoz - transform.position).normalized;
        transform.Translate(yön * hiz * Time.deltaTime, Space.World);
    }
}
