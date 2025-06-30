using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.IO;

public class CameraCaptureUDP : MonoBehaviour
{
    public Camera cam;           // Sahneden atayacaðýz
    public int sendWidth = 224;  // Küçük gönder, hýz için
    public int sendHeight = 224;
    public string ip = "127.0.0.1";
    public int port = 5005;

    private UdpClient udpClient;

    void Start()
    {
        udpClient = new UdpClient();
    }

    void Update()
    {
        // Her frame'de ekran görüntüsü al
        RenderTexture rt = new RenderTexture(sendWidth, sendHeight, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(sendWidth, sendHeight, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, sendWidth, sendHeight), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] imgBytes = screenShot.EncodeToJPG();
        udpClient.Send(imgBytes, imgBytes.Length, ip, port); // UDP ile gönder

        Destroy(screenShot);
    }
}
