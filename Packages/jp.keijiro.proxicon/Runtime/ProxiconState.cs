using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Sequential)]
public struct ProxiconState : IInputStateTypeInfo
{
    public FourCC format => new FourCC('P', 'R', 'X', 'C');

    [InputControl(name = "button0",  layout = "Button", bit = 0)]
    [InputControl(name = "button1",  layout = "Button", bit = 1)]
    [InputControl(name = "button2",  layout = "Button", bit = 2)]
    [InputControl(name = "button3",  layout = "Button", bit = 3)]
    [InputControl(name = "button4",  layout = "Button", bit = 4)]
    [InputControl(name = "button5",  layout = "Button", bit = 5)]
    [InputControl(name = "button6",  layout = "Button", bit = 6)]
    [InputControl(name = "button7",  layout = "Button", bit = 7)]
    [InputControl(name = "button8",  layout = "Button", bit = 8)]
    [InputControl(name = "button9",  layout = "Button", bit = 9)]
    [InputControl(name = "button10", layout = "Button", bit = 10)]
    [InputControl(name = "button11", layout = "Button", bit = 11)]
    [InputControl(name = "button12", layout = "Button", bit = 12)]
    [InputControl(name = "button13", layout = "Button", bit = 13)]
    [InputControl(name = "button14", layout = "Button", bit = 14)]
    [InputControl(name = "button15", layout = "Button", bit = 15)]
    public ushort buttons;

    [InputControl(name = "toggle0",  layout = "Button", bit = 0)]
    [InputControl(name = "toggle1",  layout = "Button", bit = 1)]
    [InputControl(name = "toggle2",  layout = "Button", bit = 2)]
    [InputControl(name = "toggle3",  layout = "Button", bit = 3)]
    [InputControl(name = "toggle4",  layout = "Button", bit = 4)]
    [InputControl(name = "toggle5",  layout = "Button", bit = 5)]
    [InputControl(name = "toggle6",  layout = "Button", bit = 6)]
    [InputControl(name = "toggle7",  layout = "Button", bit = 7)]
    [InputControl(name = "toggle8",  layout = "Button", bit = 8)]
    [InputControl(name = "toggle9",  layout = "Button", bit = 9)]
    [InputControl(name = "toggle10", layout = "Button", bit = 10)]
    [InputControl(name = "toggle11", layout = "Button", bit = 11)]
    [InputControl(name = "toggle12", layout = "Button", bit = 12)]
    [InputControl(name = "toggle13", layout = "Button", bit = 13)]
    [InputControl(name = "toggle14", layout = "Button", bit = 14)]
    [InputControl(name = "toggle15", layout = "Button", bit = 15)]
    public ushort toggles;

    [InputControl(name = "knob0",  layout = "Axis")] public float knob0;
    [InputControl(name = "knob1",  layout = "Axis")] public float knob1;
    [InputControl(name = "knob2",  layout = "Axis")] public float knob2;
    [InputControl(name = "knob3",  layout = "Axis")] public float knob3;
    [InputControl(name = "knob4",  layout = "Axis")] public float knob4;
    [InputControl(name = "knob5",  layout = "Axis")] public float knob5;
    [InputControl(name = "knob6",  layout = "Axis")] public float knob6;
    [InputControl(name = "knob7",  layout = "Axis")] public float knob7;
    [InputControl(name = "knob8",  layout = "Axis")] public float knob8;
    [InputControl(name = "knob9",  layout = "Axis")] public float knob9;
    [InputControl(name = "knob10", layout = "Axis")] public float knob10;
    [InputControl(name = "knob11", layout = "Axis")] public float knob11;
    [InputControl(name = "knob12", layout = "Axis")] public float knob12;
    [InputControl(name = "knob13", layout = "Axis")] public float knob13;
    [InputControl(name = "knob14", layout = "Axis")] public float knob14;
    [InputControl(name = "knob15", layout = "Axis")] public float knob15;
}
