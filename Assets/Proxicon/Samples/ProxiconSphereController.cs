using UnityEngine;

public class ProxiconSphereController : MonoBehaviour
{
    [SerializeField] GameObject _spherePrefab;
    [SerializeField] Transform _spawnParent;
    [SerializeField] float _spawnRadius = 5f;
    [SerializeField] float _minHeight = 0f;
    [SerializeField] float _maxHeight = 10f;

    SphereInputMapper[] _spheres;

    void Start()
    {
        SpawnSpheres();
    }

    void SpawnSpheres()
    {
        _spheres = new SphereInputMapper[16];

        for (int i = 0; i < 16; i++)
        {
            float angle = (i / 16f) * 2 * Mathf.PI;
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * _spawnRadius,
                _maxHeight,
                Mathf.Sin(angle) * _spawnRadius
            );

            GameObject sphere = Instantiate(_spherePrefab, position, Quaternion.identity, _spawnParent);
            sphere.name = $"Sphere_{i + 1:D2}";

            var mapper = sphere.GetComponent<SphereInputMapper>();
            if (mapper == null)
                mapper = sphere.AddComponent<SphereInputMapper>();

            mapper.Initialize(i + 1, _minHeight, _maxHeight);
            _spheres[i] = mapper;
        }
    }

    void Update()
    {
        if (_spheres == null) return;

        for (int i = 0; i < _spheres.Length; i++)
        {
            if (_spheres[i] != null)
                _spheres[i].UpdateProxiconInput();
        }
    }

    void OnDrawGizmos()
    {
        if (_spawnParent == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_spawnParent.position, _spawnRadius);

        Gizmos.color = Color.green;
        for (int i = 0; i < 16; i++)
        {
            float angle = (i / 16f) * 2 * Mathf.PI;
            Vector3 position = _spawnParent.position + new Vector3(
                Mathf.Cos(angle) * _spawnRadius,
                0,
                Mathf.Sin(angle) * _spawnRadius
            );
            Gizmos.DrawWireSphere(position, 0.2f);
        }
    }
}