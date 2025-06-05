using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float wheelRadius = 0.3f; // Tekerlek yarıçapı (ayarlanabilir)
    public float suspensionDistance = 0.2f; // Süspansiyon mesafesi (ayarlanabilir)
    public float wheelMass = 20f; // Tekerlek kütlesi (ayarlanabilir)

    private WheelCollider frontLeftWheelCollider;
    private WheelCollider frontRightWheelCollider;
    private WheelCollider rearLeftWheelCollider;
    private WheelCollider rearRightWheelCollider;

    private Rigidbody rb;

    void Start()
    {
        // Rigidbody ekle (eğer yoksa)
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 1500f; // Aracın kütlesi (ayarlanabilir)
            rb.drag = 0.1f;
            rb.angularDrag = 0.05f;
        }

        // WheelCollider'ları ekle ve ayarla
        SetupWheel(frontLeftWheelTransform, ref frontLeftWheelCollider);
        SetupWheel(frontRightWheelTransform, ref frontRightWheelCollider);
        SetupWheel(rearLeftWheelTransform, ref rearRightWheelCollider);
        SetupWheel(rearRightWheelTransform, ref rearRightWheelCollider);
    }

    void SetupWheel(Transform wheelTransform, ref WheelCollider wheelCollider)
    {
        if (wheelTransform != null)
        {
            wheelCollider = wheelTransform.gameObject.AddComponent<WheelCollider>();
            wheelCollider.radius = wheelRadius;
            wheelCollider.suspensionDistance = suspensionDistance;
            wheelCollider.mass = wheelMass;

            // Süspansiyon ayarları (örnek değerler)
            JointSpring spring = new JointSpring();
            spring.spring = 35000f; // Yay sertliği (ayarlanabilir)
            spring.damper = 4500f; // Amortisör sönümlemesi (ayarlanabilir)
            spring.targetPosition = 0.5f; // Hedef süspansiyon pozisyonu (ayarlanabilir)
            wheelCollider.suspensionSpring = spring;

            // Sürtünme ayarları (örnek değerler)
            WheelFrictionCurve forwardFriction = new WheelFrictionCurve();
            forwardFriction.extremumSlip = 0.4f;
            forwardFriction.extremumValue = 10000f;
            forwardFriction.asymptoteValue = 10000f;
            forwardFriction.stiffness = 1f;
            wheelCollider.forwardFriction = forwardFriction;

            WheelFrictionCurve sidewaysFriction = new WheelFrictionCurve();
            sidewaysFriction.extremumSlip = 0.2f;
            sidewaysFriction.extremumValue = 10000f;
            sidewaysFriction.asymptoteValue = 10000f;
            sidewaysFriction.stiffness = 1f;
            wheelCollider.sidewaysFriction = sidewaysFriction;

            Debug.Log($"WheelCollider added and configured for: {wheelTransform.name}");
        }
        else
        {
            Debug.LogError("Wheel transform is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Burada tekerlek rotasyonu gibi görsel güncellemeler yapılabilir
        // veya araba kontrol kodları eklenebilir.
        // Bu script sadece WheelCollider ekleme ve ayarlama içindir.
    }
}