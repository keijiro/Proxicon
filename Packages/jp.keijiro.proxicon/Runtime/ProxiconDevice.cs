using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[InputControlLayout(stateType = typeof(ProxiconState))]
public class ProxiconDevice : InputDevice
{
    public ButtonControl button1 { get; private set; }
    public ButtonControl button2 { get; private set; }
    public ButtonControl button3 { get; private set; }
    public ButtonControl button4 { get; private set; }
    public ButtonControl button5 { get; private set; }
    public ButtonControl button6 { get; private set; }
    public ButtonControl button7 { get; private set; }
    public ButtonControl button8 { get; private set; }
    public ButtonControl button9 { get; private set; }
    public ButtonControl button10 { get; private set; }
    public ButtonControl button11 { get; private set; }
    public ButtonControl button12 { get; private set; }
    public ButtonControl button13 { get; private set; }
    public ButtonControl button14 { get; private set; }
    public ButtonControl button15 { get; private set; }
    public ButtonControl button16 { get; private set; }

    public ButtonControl toggle1 { get; private set; }
    public ButtonControl toggle2 { get; private set; }
    public ButtonControl toggle3 { get; private set; }
    public ButtonControl toggle4 { get; private set; }
    public ButtonControl toggle5 { get; private set; }
    public ButtonControl toggle6 { get; private set; }
    public ButtonControl toggle7 { get; private set; }
    public ButtonControl toggle8 { get; private set; }
    public ButtonControl toggle9 { get; private set; }
    public ButtonControl toggle10 { get; private set; }
    public ButtonControl toggle11 { get; private set; }
    public ButtonControl toggle12 { get; private set; }
    public ButtonControl toggle13 { get; private set; }
    public ButtonControl toggle14 { get; private set; }
    public ButtonControl toggle15 { get; private set; }
    public ButtonControl toggle16 { get; private set; }

    public AxisControl knob1 { get; private set; }
    public AxisControl knob2 { get; private set; }
    public AxisControl knob3 { get; private set; }
    public AxisControl knob4 { get; private set; }
    public AxisControl knob5 { get; private set; }
    public AxisControl knob6 { get; private set; }
    public AxisControl knob7 { get; private set; }
    public AxisControl knob8 { get; private set; }
    public AxisControl knob9 { get; private set; }
    public AxisControl knob10 { get; private set; }
    public AxisControl knob11 { get; private set; }
    public AxisControl knob12 { get; private set; }
    public AxisControl knob13 { get; private set; }
    public AxisControl knob14 { get; private set; }
    public AxisControl knob15 { get; private set; }
    public AxisControl knob16 { get; private set; }

    public static ProxiconDevice current { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        button1 = GetChildControl<ButtonControl>("button1");
        button2 = GetChildControl<ButtonControl>("button2");
        button3 = GetChildControl<ButtonControl>("button3");
        button4 = GetChildControl<ButtonControl>("button4");
        button5 = GetChildControl<ButtonControl>("button5");
        button6 = GetChildControl<ButtonControl>("button6");
        button7 = GetChildControl<ButtonControl>("button7");
        button8 = GetChildControl<ButtonControl>("button8");
        button9 = GetChildControl<ButtonControl>("button9");
        button10 = GetChildControl<ButtonControl>("button10");
        button11 = GetChildControl<ButtonControl>("button11");
        button12 = GetChildControl<ButtonControl>("button12");
        button13 = GetChildControl<ButtonControl>("button13");
        button14 = GetChildControl<ButtonControl>("button14");
        button15 = GetChildControl<ButtonControl>("button15");
        button16 = GetChildControl<ButtonControl>("button16");

        toggle1 = GetChildControl<ButtonControl>("toggle1");
        toggle2 = GetChildControl<ButtonControl>("toggle2");
        toggle3 = GetChildControl<ButtonControl>("toggle3");
        toggle4 = GetChildControl<ButtonControl>("toggle4");
        toggle5 = GetChildControl<ButtonControl>("toggle5");
        toggle6 = GetChildControl<ButtonControl>("toggle6");
        toggle7 = GetChildControl<ButtonControl>("toggle7");
        toggle8 = GetChildControl<ButtonControl>("toggle8");
        toggle9 = GetChildControl<ButtonControl>("toggle9");
        toggle10 = GetChildControl<ButtonControl>("toggle10");
        toggle11 = GetChildControl<ButtonControl>("toggle11");
        toggle12 = GetChildControl<ButtonControl>("toggle12");
        toggle13 = GetChildControl<ButtonControl>("toggle13");
        toggle14 = GetChildControl<ButtonControl>("toggle14");
        toggle15 = GetChildControl<ButtonControl>("toggle15");
        toggle16 = GetChildControl<ButtonControl>("toggle16");

        knob1 = GetChildControl<AxisControl>("knob1");
        knob2 = GetChildControl<AxisControl>("knob2");
        knob3 = GetChildControl<AxisControl>("knob3");
        knob4 = GetChildControl<AxisControl>("knob4");
        knob5 = GetChildControl<AxisControl>("knob5");
        knob6 = GetChildControl<AxisControl>("knob6");
        knob7 = GetChildControl<AxisControl>("knob7");
        knob8 = GetChildControl<AxisControl>("knob8");
        knob9 = GetChildControl<AxisControl>("knob9");
        knob10 = GetChildControl<AxisControl>("knob10");
        knob11 = GetChildControl<AxisControl>("knob11");
        knob12 = GetChildControl<AxisControl>("knob12");
        knob13 = GetChildControl<AxisControl>("knob13");
        knob14 = GetChildControl<AxisControl>("knob14");
        knob15 = GetChildControl<AxisControl>("knob15");
        knob16 = GetChildControl<AxisControl>("knob16");
    }

    public override void MakeCurrent()
    {
        base.MakeCurrent();
        current = this;
    }

    protected override void OnRemoved()
    {
        base.OnRemoved();
        if (current == this)
            current = null;
    }

#if UNITY_EDITOR

    static ProxiconDevice()
    {
        InputSystem.RegisterLayout<ProxiconDevice>();
        if (current == null) InputSystem.AddDevice<ProxiconDevice>();
    }

#else

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterLayout<ProxiconDevice>();
        if (current == null) InputSystem.AddDevice<ProxiconDevice>();
    }

#endif
}

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = 68)]
public struct ProxiconState : IInputStateTypeInfo
{
    public FourCC format => new FourCC('P', 'R', 'X', 'C');

    [System.Runtime.InteropServices.FieldOffset(0)]
    [InputControl(name = "button1", layout = "Button", bit = 0)]
    [InputControl(name = "button2", layout = "Button", bit = 1)]
    [InputControl(name = "button3", layout = "Button", bit = 2)]
    [InputControl(name = "button4", layout = "Button", bit = 3)]
    [InputControl(name = "button5", layout = "Button", bit = 4)]
    [InputControl(name = "button6", layout = "Button", bit = 5)]
    [InputControl(name = "button7", layout = "Button", bit = 6)]
    [InputControl(name = "button8", layout = "Button", bit = 7)]
    [InputControl(name = "button9", layout = "Button", bit = 8)]
    [InputControl(name = "button10", layout = "Button", bit = 9)]
    [InputControl(name = "button11", layout = "Button", bit = 10)]
    [InputControl(name = "button12", layout = "Button", bit = 11)]
    [InputControl(name = "button13", layout = "Button", bit = 12)]
    [InputControl(name = "button14", layout = "Button", bit = 13)]
    [InputControl(name = "button15", layout = "Button", bit = 14)]
    [InputControl(name = "button16", layout = "Button", bit = 15)]
    public ushort buttons;

    [System.Runtime.InteropServices.FieldOffset(2)]
    [InputControl(name = "toggle1", layout = "Button", bit = 0)]
    [InputControl(name = "toggle2", layout = "Button", bit = 1)]
    [InputControl(name = "toggle3", layout = "Button", bit = 2)]
    [InputControl(name = "toggle4", layout = "Button", bit = 3)]
    [InputControl(name = "toggle5", layout = "Button", bit = 4)]
    [InputControl(name = "toggle6", layout = "Button", bit = 5)]
    [InputControl(name = "toggle7", layout = "Button", bit = 6)]
    [InputControl(name = "toggle8", layout = "Button", bit = 7)]
    [InputControl(name = "toggle9", layout = "Button", bit = 8)]
    [InputControl(name = "toggle10", layout = "Button", bit = 9)]
    [InputControl(name = "toggle11", layout = "Button", bit = 10)]
    [InputControl(name = "toggle12", layout = "Button", bit = 11)]
    [InputControl(name = "toggle13", layout = "Button", bit = 12)]
    [InputControl(name = "toggle14", layout = "Button", bit = 13)]
    [InputControl(name = "toggle15", layout = "Button", bit = 14)]
    [InputControl(name = "toggle16", layout = "Button", bit = 15)]
    public ushort toggles;

    [System.Runtime.InteropServices.FieldOffset(4), InputControl(name = "knob1", layout = "Axis")]
    public float knob1;
    [System.Runtime.InteropServices.FieldOffset(8), InputControl(name = "knob2", layout = "Axis")]
    public float knob2;
    [System.Runtime.InteropServices.FieldOffset(12), InputControl(name = "knob3", layout = "Axis")]
    public float knob3;
    [System.Runtime.InteropServices.FieldOffset(16), InputControl(name = "knob4", layout = "Axis")]
    public float knob4;
    [System.Runtime.InteropServices.FieldOffset(20), InputControl(name = "knob5", layout = "Axis")]
    public float knob5;
    [System.Runtime.InteropServices.FieldOffset(24), InputControl(name = "knob6", layout = "Axis")]
    public float knob6;
    [System.Runtime.InteropServices.FieldOffset(28), InputControl(name = "knob7", layout = "Axis")]
    public float knob7;
    [System.Runtime.InteropServices.FieldOffset(32), InputControl(name = "knob8", layout = "Axis")]
    public float knob8;
    [System.Runtime.InteropServices.FieldOffset(36), InputControl(name = "knob9", layout = "Axis")]
    public float knob9;
    [System.Runtime.InteropServices.FieldOffset(40), InputControl(name = "knob10", layout = "Axis")]
    public float knob10;
    [System.Runtime.InteropServices.FieldOffset(44), InputControl(name = "knob11", layout = "Axis")]
    public float knob11;
    [System.Runtime.InteropServices.FieldOffset(48), InputControl(name = "knob12", layout = "Axis")]
    public float knob12;
    [System.Runtime.InteropServices.FieldOffset(52), InputControl(name = "knob13", layout = "Axis")]
    public float knob13;
    [System.Runtime.InteropServices.FieldOffset(56), InputControl(name = "knob14", layout = "Axis")]
    public float knob14;
    [System.Runtime.InteropServices.FieldOffset(60), InputControl(name = "knob15", layout = "Axis")]
    public float knob15;
    [System.Runtime.InteropServices.FieldOffset(64), InputControl(name = "knob16", layout = "Axis")]
    public float knob16;
}