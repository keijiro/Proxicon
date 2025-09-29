using UnityEngine;
using UnityEngine.InputSystem;

public static class Proxicon
{
    static ProxiconState _state;
    static bool _stateChanged;

    public static bool IsDirty => _stateChanged;
    public static ProxiconState CurrentState => _state;

    public static void ResetDirtyFlag()
    {
        _stateChanged = false;
    }

    public static void SetButton(int index, bool value)
    { if (index < 0 || index > 15) return;

        var mask = (ushort)(1 << index);
        if (value)
            _state.buttons |= mask;
        else
            _state.buttons &= (ushort)~mask;

        _stateChanged = true;
    }

    public static void SetToggle(int index, bool value)
    {
        if (index < 0 || index > 15) return;

        var mask = (ushort)(1 << index);
        if (value)
            _state.toggles |= mask;
        else
            _state.toggles &= (ushort)~mask;

        _stateChanged = true;
    }

    public static void SetKnob(int index, float value)
    {
        if (index < 0 || index > 15) return;

        value = Mathf.Clamp01(value);

        switch (index)
        {
            case 0: _state.knob0 = value; break;
            case 1: _state.knob1 = value; break;
            case 2: _state.knob2 = value; break;
            case 3: _state.knob3 = value; break;
            case 4: _state.knob4 = value; break;
            case 5: _state.knob5 = value; break;
            case 6: _state.knob6 = value; break;
            case 7: _state.knob7 = value; break;
            case 8: _state.knob8 = value; break;
            case 9: _state.knob9 = value; break;
            case 10: _state.knob10 = value; break;
            case 11: _state.knob11 = value; break;
            case 12: _state.knob12 = value; break;
            case 13: _state.knob13 = value; break;
            case 14: _state.knob14 = value; break;
            case 15: _state.knob15 = value; break;
        }

        _stateChanged = true;
    }

    public static bool GetButton(int index)
    {
        if (index < 0 || index > 15) return false;
        return (_state.buttons & (1 << index)) != 0;
    }

    public static bool GetToggle(int index)
    {
        if (index < 0 || index > 15) return false;
        return (_state.toggles & (1 << index)) != 0;
    }

    public static float GetKnob(int index)
    {
        if (index < 0 || index > 15) return 0f;

        return index switch
        {
            0 => _state.knob0,
            1 => _state.knob1,
            2 => _state.knob2,
            3 => _state.knob3,
            4 => _state.knob4,
            5 => _state.knob5,
            6 => _state.knob6,
            7 => _state.knob7,
            8 => _state.knob8,
            9 => _state.knob9,
            10 => _state.knob10,
            11 => _state.knob11,
            12 => _state.knob12,
            13 => _state.knob13,
            14 => _state.knob14,
            15 => _state.knob15,
            _ => 0f
        };
    }

    public static void PressButton(int index)
    {
        SetButton(index, true);
    }

    public static void ReleaseButton(int index)
    {
        SetButton(index, false);
    }

    public static void ToggleSwitch(int index)
    {
        SetToggle(index, !GetToggle(index));
    }

    public static void ResetAll()
    {
        _state = default;
        _stateChanged = true;
    }
}