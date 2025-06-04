using UnityEngine;

public class VehicleFollowCamera : MonoBehaviour
{
    [Header("Takip Ayarları")]
    public Transform target; // Takip edilecek araç
    public Vector3 offset = new Vector3(0, 5, -7); // Kameradan araca olan mesafe
    
    [Header("Hareket Ayarları")]
    public float followSpeed = 2f; // Takip hızı
    public float rotationSpeed = 2f; // Dönüş hızı
    
    [Header("Opsiyonel Ayarlar")]
    public bool smoothFollow = true; // Yumuşak takip
    public bool lookAtTarget = true; // Hedefe bakma
    
    private Vector3 velocity = Vector3.zero; // SmoothDamp için gerekli
    
    void Start()
    {
        // Eğer target atanmamışsa, "Vehicle" tag'li objeyi bul
        if (target == null)
        {
            GameObject vehicle = GameObject.FindWithTag("Vehicle");
            if (vehicle != null)
                target = vehicle.transform;
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Hedef pozisyon hesapla (araç pozisyonu + offset)
        Vector3 desiredPosition = target.position + offset;
        
        if (smoothFollow)
        {
            // Yumuşak takip
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / followSpeed);
        }
        else
        {
            // Doğrudan takip
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
        
        // Kameranın araca bakmasını sağla
        if (lookAtTarget)
        {
            Vector3 lookDirection = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

// Araç kontrol scripti (örnek)
public class VehicleController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Eğer Rigidbody yoksa ekle
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }
    
    void Update()
    {
        HandleMovement();
    }
    
    void HandleMovement()
    {
        // WASD girişlerini al
        float horizontal = Input.GetAxis("Horizontal"); // A-D
        float vertical = Input.GetAxis("Vertical");     // W-S
        
        // Hareket vektörü hesapla
        Vector3 movement = transform.forward * vertical * moveSpeed;
        
        // Rigidbody ile hareket
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        
        // Dönüş (A-D tuşları ile)
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        }
    }
}

// Alternatif basit takip kamerası
public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -7);
    public float smoothTime = 0.3f;
    
    private Vector3 velocity = Vector3.zero;
    
    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        // Kamerayı hedefe çevir
        transform.LookAt(target);
    }
}