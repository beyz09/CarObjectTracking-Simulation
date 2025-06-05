using UnityEngine;

public class TopHareket : MonoBehaviour
{
    public float yaricap = 3f;
    public float hiz = 1f;
    private float aci = 0f;
    private Vector3 merkezNokta;

    void Start()
    {
        merkezNokta = transform.position;
    }

    void FixedUpdate()
    {
        aci += hiz * Time.fixedDeltaTime;
        float x = Mathf.Cos(aci) * yaricap;
        float z = Mathf.Sin(aci) * yaricap;
        transform.position = new Vector3(merkezNokta.x + x, transform.position.y, merkezNokta.z + z);
    }
}
