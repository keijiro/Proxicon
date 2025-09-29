using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public sealed class StateTest : MonoBehaviour
{
    [SerializeField] InputAction _action = null;

    void OnEnable()
        => _action.Enable();

    void OnDisable()
        => _action.Disable();

    void Update()
    {
        var state = _action.ReadValue<float>();

        var text = $"{_action.name}: {state}";
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Label>().text = text;

        var camera = FindFirstObjectByType<Camera>();
        camera.backgroundColor = Color.Lerp(Color.black, Color.blue, state);
    }

    async Awaitable Start()
    {
        await Awaitable.WaitForSecondsAsync(2);

        Proxicon.SetButton(1, true);
        Proxicon.UpdateDevice();
    }

}
