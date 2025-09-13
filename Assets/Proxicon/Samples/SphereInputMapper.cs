using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class SphereInputMapper : MonoBehaviour
{
    [SerializeField] int _inputIndex = 1;
    [SerializeField] float _minHeight = 0f;
    [SerializeField] float _maxHeight = 10f;
    [SerializeField] LayerMask _contactLayers = -1;

    Rigidbody _rigidbody;
    bool _wasInContact = false;
    bool _isInContact = false;
    int _contactCount = 0;

    public void Initialize(int inputIndex, float minHeight, float maxHeight)
    {
        _inputIndex = inputIndex;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_rigidbody == null)
        {
            Debug.LogError($"SphereInputMapper on {gameObject.name} requires a Rigidbody component");
            enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsInContactLayer(other.gameObject.layer))
        {
            _contactCount++;
            UpdateContactState();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsInContactLayer(other.gameObject.layer))
        {
            _contactCount = Mathf.Max(0, _contactCount - 1);
            UpdateContactState();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsInContactLayer(collision.gameObject.layer))
        {
            _contactCount++;
            UpdateContactState();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (IsInContactLayer(collision.gameObject.layer))
        {
            _contactCount = Mathf.Max(0, _contactCount - 1);
            UpdateContactState();
        }
    }

    bool IsInContactLayer(int layer)
    {
        return (_contactLayers.value & (1 << layer)) != 0;
    }

    void UpdateContactState()
    {
        _wasInContact = _isInContact;
        _isInContact = _contactCount > 0;
    }

    public void UpdateProxiconInput()
    {
        if (_rigidbody == null) return;

        UpdateHeightInput();
        UpdateContactInputs();
    }

    void UpdateHeightInput()
    {
        float height = transform.position.y;
        float normalizedHeight = Mathf.InverseLerp(_minHeight, _maxHeight, height);
        normalizedHeight = Mathf.Clamp01(normalizedHeight);

        ProxiconAPI.SetKnob(_inputIndex, normalizedHeight);
    }

    void UpdateContactInputs()
    {
        ProxiconAPI.SetToggle(_inputIndex, _isInContact);

        if (!_wasInContact && _isInContact)
        {
            ProxiconAPI.PressButton(_inputIndex);
        }
        else if (_wasInContact && !_isInContact)
        {
            ProxiconAPI.ReleaseButton(_inputIndex);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _isInContact ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        float height = transform.position.y;
        float normalizedHeight = Mathf.InverseLerp(_minHeight, _maxHeight, height);

        Gizmos.color = Color.blue;
        Vector3 barStart = transform.position + Vector3.right * 1f;
        Vector3 barEnd = barStart + Vector3.up * normalizedHeight * 2f;
        Gizmos.DrawLine(barStart, barEnd);

        Gizmos.DrawWireCube(barEnd + Vector3.up * 0.1f, Vector3.one * 0.1f);
    }

    void OnGUI()
    {
        if (!Application.isPlaying) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1f);
        if (screenPos.z > 0)
        {
            float height = transform.position.y;
            float normalizedHeight = Mathf.InverseLerp(_minHeight, _maxHeight, height);

            GUI.color = _isInContact ? Color.red : Color.white;
            GUI.Label(new Rect(screenPos.x - 50, Screen.height - screenPos.y - 40, 100, 40),
                $"Sphere {_inputIndex}\nH: {normalizedHeight:F2}\nContact: {_isInContact}");
        }
    }
}