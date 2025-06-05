using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class CameraSender : MonoBehaviour
{
    public Camera cam;
    private RenderTexture renderTexture;
    private Texture2D texture2D;
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24);
        texture2D = new Texture2D(256, 256, TextureFormat.RGB24, false);
        cam.targetTexture = renderTexture;

        try
        {
            client = new TcpClient("127.0.0.1", 9999); // Python tarafý burayý dinleyecek
            stream = client.GetStream();
        }
        catch (Exception e)
        {
            Debug.LogError("Baðlantý kurulamadý: " + e.Message);
        }
    }

    void Update()
    {
        if (stream != null && stream.CanWrite)
        {
            RenderTexture.active = renderTexture;
            cam.Render();
            texture2D.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            texture2D.Apply();
            byte[] imageBytes = texture2D.EncodeToJPG();

            try
            {
                byte[] lengthPrefix = BitConverter.GetBytes(imageBytes.Length);
                stream.Write(lengthPrefix, 0, lengthPrefix.Length);
                stream.Write(imageBytes, 0, imageBytes.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("Görüntü gönderilemedi: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
    }
}
