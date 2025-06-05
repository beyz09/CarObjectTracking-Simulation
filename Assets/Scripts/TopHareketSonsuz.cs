using UnityEngine;

public class TopHareketSonsuz : MonoBehaviour
{
    public float a = 5f;  // X geniþliði
    public float b = 3f;  // Z derinliði
    public float hýz = 1f;
    private float zaman;

    void Update()
    {
        zaman += Time.deltaTime * hýz;
        float x = a * Mathf.Sin(zaman);
        float z = b * Mathf.Sin(zaman) * Mathf.Cos(zaman);

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
