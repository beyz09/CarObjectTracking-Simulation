using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class CameraStreamer : MonoBehaviour
{
    public Camera cam;
    public int width = 320;
    public int height = 240;
    public int port = 5057;

    private TcpClient client;
    private NetworkStream stream;
    private Thread connectThread;
    private Texture2D tex;
    private RenderTexture rt;

    void Start()
    {
        tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        rt = new RenderTexture(width, height, 24);

        connectThread = new Thread(ConnectToPython);
        connectThread.IsBackground = true;
        connectThread.Start();
    }

    void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
        if (connectThread != null) connectThread.Abort();
    }

    void ConnectToPython()
    {
        try
        {
            client = new TcpClient("127.0.0.1", port);
            stream = client.GetStream();
            Debug.Log("Baðlandý!");
        }
        catch (Exception e)
        {
            Debug.Log("Socket Baðlantý Hatasý: " + e.Message);
        }
    }

    void LateUpdate()
    {
        if (stream == null || !stream.CanWrite) return;

        cam.targetTexture = rt;
        cam.Render();
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        cam.targetTexture = null;
        RenderTexture.active = null;

        byte[] jpg = tex.EncodeToJPG();

        // Baþa 4 byte uzunluk bilgisi koy (Python kolayca okuyabilsin diye)
        byte[] sizeBytes = BitConverter.GetBytes(jpg.Length);
        byte[] sendBytes = new byte[sizeBytes.Length + jpg.Length];
        Buffer.BlockCopy(sizeBytes, 0, sendBytes, 0, sizeBytes.Length);
        Buffer.BlockCopy(jpg, 0, sendBytes, sizeBytes.Length, jpg.Length);

        try
        {
            stream.Write(sendBytes, 0, sendBytes.Length);
        }
        catch { }
    }
}
