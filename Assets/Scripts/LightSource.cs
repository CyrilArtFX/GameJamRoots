using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightSource : MonoBehaviour
{
    private LineRenderer line;

    [SerializeField]
    private bool moveablePureLight;

    private bool lightActivated;

    public bool Activated => lightActivated;

    [HideInInspector]
    public bool ReceiveEmission;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    void Start()
    {
        line.enabled = true;
        lightActivated = true;
    }

    void FixedUpdate()
    {
        if (!lightActivated) return;

        Vector3 ray_origin = transform.position;
        Vector3 ray_direction = transform.forward;

        if (Physics.Raycast(ray_origin, ray_direction, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out LightReflector reflector))
            {
                reflector.ReceiveLight(hit.point, ray_direction, hit.normal, this);
            }
        }

        if(moveablePureLight)
        {
            SetStartPointAndDirection(transform.position, transform.forward);
        }
    }

    public void SetStartPointAndDirection(Vector3 point, Vector3 direction)
    {
        transform.position = point;
        transform.forward = direction.normalized;
        line.SetPosition(0, point);
        
        if (Physics.Raycast(point, direction, out RaycastHit hit))
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, point + direction * 15.0f);
        }
    }
}
