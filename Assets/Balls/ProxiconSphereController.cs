using UnityEngine;

public class ProxiconSphereController : MonoBehaviour
{
    [SerializeField] GameObject _spherePrefab = null;
    [SerializeField] Transform _spawnParent = null;
    [SerializeField] float _spawnRadius = 5f;
    [SerializeField] float _minHeight = 0f;
    [SerializeField] float _maxHeight = 10f;
    [SerializeField] float _spawnHeightVariation = 2f;

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
            float randomHeight = _maxHeight + Random.Range(-_spawnHeightVariation, _spawnHeightVariation);
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * _spawnRadius,
                randomHeight,
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

}