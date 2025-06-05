using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class CameraGonderici : MonoBehaviour
{
    public RenderTexture renderTexture;
    public string hedefIP = "127.0.0.1";
    public int hedefPort = 9999;

    private TcpClient client;
    private NetworkStream stream;
    private Texture2D ekranGoruntusu;
    private bool baglantiHazir = false;

    void Start()
    {
        ekranGoruntusu = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        try
        {
            client = new TcpClient(hedefIP, hedefPort);
            stream = client.GetStream();
            baglantiHazir = true;
            Debug.Log("[BAÐLANDI] Python'a baðlantý kuruldu.");
        }
        catch
        {
            Debug.LogError("[HATA] Python'a baðlanýlamadý.");
        }
    }

    void Update()
    {
        if (!baglantiHazir) return;

        RenderTexture.active = renderTexture;
        ekranGoruntusu.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        ekranGoruntusu.Apply();
        RenderTexture.active = null;

        byte[] pngBytes = ekranGoruntusu.EncodeToPNG();

        byte[] uzunluk = System.BitConverter.GetBytes(pngBytes.Length);
        if (System.BitConverter.IsLittleEndian)
            System.Array.Reverse(uzunluk); // big-endian uyumu

        try
        {
            stream.Write(uzunluk, 0, 4);
            stream.Write(pngBytes, 0, pngBytes.Length);
            stream.Flush();
        }
        catch
        {
            Debug.LogWarning("[UYARI] Veri gönderimi baþarýsýz.");
            baglantiHazir = false;
        }
    }

    void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
    }
}
