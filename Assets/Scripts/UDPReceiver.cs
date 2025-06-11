using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPReceiver : MonoBehaviour
{
    public int listenPort = 5055;
    public Vector2 receivedTarget = Vector2.zero;

    UdpClient client;
    IPEndPoint endPoint;

    void Start()
    {
        client = new UdpClient(listenPort);
        endPoint = new IPEndPoint(IPAddress.Any, listenPort);
        client.BeginReceive(ReceiveCallback, null);
    }

    void ReceiveCallback(System.IAsyncResult ar)
    {
        try
        {
            byte[] data = client.EndReceive(ar, ref endPoint);
            string message = Encoding.UTF8.GetString(data).Trim();
            Debug.Log("Gelen UDP Mesajý: " + message);

            string[] parts = message.Split(',');

            float x = 0, y = 0;
            if (parts.Length == 2)
            {
                float.TryParse(parts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out x);
                float.TryParse(parts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out y);
            }
            else
            {
                Debug.LogWarning("UDPReceiver: Beklenmeyen veri formatý: " + message);
            }

            receivedTarget = new Vector2(x, y);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("UDPReceiver Exception: " + ex.Message);
        }
        finally
        {
            client.BeginReceive(ReceiveCallback, null);
        }
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}
