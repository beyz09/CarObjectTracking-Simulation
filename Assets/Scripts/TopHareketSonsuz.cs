using UnityEngine;

public class TopHareketSonsuz : MonoBehaviour
{
    public float a = 5f;  // X geni�li�i
    public float b = 3f;  // Z derinli�i
    public float h�z = 1f;
    private float zaman;

    void Update()
    {
        zaman += Time.deltaTime * h�z;
        float x = a * Mathf.Sin(zaman);
        float z = b * Mathf.Sin(zaman) * Mathf.Cos(zaman);

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
