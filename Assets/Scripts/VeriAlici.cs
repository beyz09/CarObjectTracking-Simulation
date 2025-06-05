using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class VeriAlici : MonoBehaviour
{
    public string ip = "127.0.0.1";
    public int port = 9999;
    public Vector2 hedefPozisyon;

    TcpClient client;
    NetworkStream stream;
    Thread veriThread;

    void Start()
    {
        veriThread = new Thread(VeriDinle);
        veriThread.IsBackground = true;
        veriThread.Start();
    }

    void VeriDinle()
    {
        try
        {
            client = new TcpClient(ip, port);
            stream = client.GetStream();

            byte[] buffer = new byte[1024];

            while (true)
            {
                int byteCount = stream.Read(buffer, 0, buffer.Length);
                string veri = Encoding.ASCII.GetString(buffer, 0, byteCount);

                string[] parcalar = veri.Split(',');
                if (parcalar.Length == 2)
                {
                    float x = float.Parse(parcalar[0]);
                    float y = float.Parse(parcalar[1]);
                    hedefPozisyon = new Vector2(x, y);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Socket hatası: " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        veriThread?.Abort();
        stream?.Close();
        client?.Close();
    }
}
