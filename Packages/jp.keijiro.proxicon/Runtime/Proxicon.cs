using UnityEngine;

public static class Proxicon
{
    public static bool IsDirty { get; private set; }

    public static ProxiconState CurrentState => _state;

    public static void ResetDirtyFlag()
      => IsDirty = false;

    public static void SetButton(int index, bool value)
    {
        if (index < 0 || index > 15) return;

        var mask = (ushort)(1 << index);
        if (value)
            _state.buttons |= mask;
        else
            _state.buttons &= (ushort)~mask;

        IsDirty = true;
    }

    public static void SetToggle(int index, bool value)
    {
        if (index < 0 || index > 15) return;

        var mask = (ushort)(1 << index);
        if (value)
            _state.toggles |= mask;
        else
            _state.toggles &= (ushort)~mask;

        IsDirty = true;
    }

    public static unsafe void SetKnob(int index, float value)
    {
        if (index < 0 || index > 15) return;

        value = Mathf.Clamp01(value);

        fixed (ushort* knobs = _state.knobs)
        {
            knobs[index] = (ushort)(value * 65535f);
        }

        IsDirty = true;
    }

    static ProxiconState _state;
}