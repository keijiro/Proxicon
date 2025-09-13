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
        bool isFalling = _rigidbody.linearVelocity.y < 0;
        ProxiconAPI.SetToggle(_inputIndex, isFalling);

        if (!_wasInContact && _isInContact)
        {
            ProxiconAPI.PressButton(_inputIndex);
        }
        else if (_wasInContact && !_isInContact)
        {
            ProxiconAPI.ReleaseButton(_inputIndex);
        }
    }

}