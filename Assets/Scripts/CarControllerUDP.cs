using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class CarControllerUDP : MonoBehaviour
{
    public float speed = 2f;
    public float turnSpeed = 60f;
    public int listenPort = 5006;
    public int frameWidth = 224;  // Gönderilen kamera geniþliði
    public int frameHeight = 224; // Gönderilen kamera yüksekliði

    volatile int targetX = 0;
    volatile int targetY = 0;

    Thread udpThread;

    void Start()
    {
        udpThread = new Thread(new ThreadStart(ListenUDP));
        udpThread.IsBackground = true;
        udpThread.Start();
    }

    void Update()
    {
        // Ekran merkezini bul (kendi gönderdiðimiz kameraya göre)
        int centerX = frameWidth / 2;

        // Gelen x koordinatýna göre yön belirle
        float steer = 0f;
        if (targetX > 0) // Sýfýrsa, top yok demektir
        {
            float diff = targetX - centerX;
            steer = Mathf.Clamp(diff / centerX, -1f, 1f);
        }

        // Araç rotasyonu (sol/sað)
        transform.Rotate(0, steer * turnSpeed * Time.deltaTime, 0);

        // Araç ileri hareket
        if (targetX > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void ListenUDP()
    {
        UdpClient client = new UdpClient(listenPort);
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, listenPort);
        while (true)
        {
            try
            {
                byte[] data = client.Receive(ref anyIP);
                if (data.Length == 8)
                {
                    targetX = System.BitConverter.ToInt32(data, 0);
                    targetY = System.BitConverter.ToInt32(data, 4);
                }
            }
            catch { }
        }
    }

    private void OnApplicationQuit()
    {
        if (udpThread != null && udpThread.IsAlive)
            udpThread.Abort();
    }
}
