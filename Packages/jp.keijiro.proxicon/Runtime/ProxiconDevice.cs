using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[InputControlLayout(stateType = typeof(ProxiconState))]
public class ProxiconDevice : InputDevice, IInputUpdateCallbackReceiver
{
    public ButtonControl button0  { get; private set; }
    public ButtonControl button1  { get; private set; }
    public ButtonControl button2  { get; private set; }
    public ButtonControl button3  { get; private set; }
    public ButtonControl button4  { get; private set; }
    public ButtonControl button5  { get; private set; }
    public ButtonControl button6  { get; private set; }
    public ButtonControl button7  { get; private set; }
    public ButtonControl button8  { get; private set; }
    public ButtonControl button9  { get; private set; }
    public ButtonControl button10 { get; private set; }
    public ButtonControl button11 { get; private set; }
    public ButtonControl button12 { get; private set; }
    public ButtonControl button13 { get; private set; }
    public ButtonControl button14 { get; private set; }
    public ButtonControl button15 { get; private set; }

    public ButtonControl toggle0  { get; private set; }
    public ButtonControl toggle1  { get; private set; }
    public ButtonControl toggle2  { get; private set; }
    public ButtonControl toggle3  { get; private set; }
    public ButtonControl toggle4  { get; private set; }
    public ButtonControl toggle5  { get; private set; }
    public ButtonControl toggle6  { get; private set; }
    public ButtonControl toggle7  { get; private set; }
    public ButtonControl toggle8  { get; private set; }
    public ButtonControl toggle9  { get; private set; }
    public ButtonControl toggle10 { get; private set; }
    public ButtonControl toggle11 { get; private set; }
    public ButtonControl toggle12 { get; private set; }
    public ButtonControl toggle13 { get; private set; }
    public ButtonControl toggle14 { get; private set; }
    public ButtonControl toggle15 { get; private set; }

    public AxisControl knob0  { get; private set; }
    public AxisControl knob1  { get; private set; }
    public AxisControl knob2  { get; private set; }
    public AxisControl knob3  { get; private set; }
    public AxisControl knob4  { get; private set; }
    public AxisControl knob5  { get; private set; }
    public AxisControl knob6  { get; private set; }
    public AxisControl knob7  { get; private set; }
    public AxisControl knob8  { get; private set; }
    public AxisControl knob9  { get; private set; }
    public AxisControl knob10 { get; private set; }
    public AxisControl knob11 { get; private set; }
    public AxisControl knob12 { get; private set; }
    public AxisControl knob13 { get; private set; }
    public AxisControl knob14 { get; private set; }
    public AxisControl knob15 { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        button0  = GetChildControl<ButtonControl>("button0");
        button1  = GetChildControl<ButtonControl>("button1");
        button2  = GetChildControl<ButtonControl>("button2");
        button3  = GetChildControl<ButtonControl>("button3");
        button4  = GetChildControl<ButtonControl>("button4");
        button5  = GetChildControl<ButtonControl>("button5");
        button6  = GetChildControl<ButtonControl>("button6");
        button7  = GetChildControl<ButtonControl>("button7");
        button8  = GetChildControl<ButtonControl>("button8");
        button9  = GetChildControl<ButtonControl>("button9");
        button10 = GetChildControl<ButtonControl>("button10");
        button11 = GetChildControl<ButtonControl>("button11");
        button12 = GetChildControl<ButtonControl>("button12");
        button13 = GetChildControl<ButtonControl>("button13");
        button14 = GetChildControl<ButtonControl>("button14");
        button15 = GetChildControl<ButtonControl>("button15");

        toggle0  = GetChildControl<ButtonControl>("toggle0");
        toggle1  = GetChildControl<ButtonControl>("toggle1");
        toggle2  = GetChildControl<ButtonControl>("toggle2");
        toggle3  = GetChildControl<ButtonControl>("toggle3");
        toggle4  = GetChildControl<ButtonControl>("toggle4");
        toggle5  = GetChildControl<ButtonControl>("toggle5");
        toggle6  = GetChildControl<ButtonControl>("toggle6");
        toggle7  = GetChildControl<ButtonControl>("toggle7");
        toggle8  = GetChildControl<ButtonControl>("toggle8");
        toggle9  = GetChildControl<ButtonControl>("toggle9");
        toggle10 = GetChildControl<ButtonControl>("toggle10");
        toggle11 = GetChildControl<ButtonControl>("toggle11");
        toggle12 = GetChildControl<ButtonControl>("toggle12");
        toggle13 = GetChildControl<ButtonControl>("toggle13");
        toggle14 = GetChildControl<ButtonControl>("toggle14");
        toggle15 = GetChildControl<ButtonControl>("toggle15");

        knob0  = GetChildControl<AxisControl>("knob0");
        knob1  = GetChildControl<AxisControl>("knob1");
        knob2  = GetChildControl<AxisControl>("knob2");
        knob3  = GetChildControl<AxisControl>("knob3");
        knob4  = GetChildControl<AxisControl>("knob4");
        knob5  = GetChildControl<AxisControl>("knob5");
        knob6  = GetChildControl<AxisControl>("knob6");
        knob7  = GetChildControl<AxisControl>("knob7");
        knob8  = GetChildControl<AxisControl>("knob8");
        knob9  = GetChildControl<AxisControl>("knob9");
        knob10 = GetChildControl<AxisControl>("knob10");
        knob11 = GetChildControl<AxisControl>("knob11");
        knob12 = GetChildControl<AxisControl>("knob12");
        knob13 = GetChildControl<AxisControl>("knob13");
        knob14 = GetChildControl<AxisControl>("knob14");
        knob15 = GetChildControl<AxisControl>("knob15");
    }

    public void OnUpdate()
    {
        if (!Proxicon.IsDirty) return;

        var device = InputSystem.GetDevice<ProxiconDevice>();
        if (device == null) return;

        InputSystem.QueueStateEvent(device, Proxicon.CurrentState);
        Proxicon.ResetDirtyFlag();
    }

    protected override unsafe long ExecuteCommand(InputDeviceCommand* commandPtr)
    {
        if (commandPtr->type == RequestSyncCommand.Type)
            return InputDeviceCommand.GenericSuccess;
        return base.ExecuteCommand(commandPtr);
    }

    static ProxiconDevice()
    {
        InputSystem.RegisterLayout<ProxiconDevice>();
        var device = InputSystem.GetDevice<ProxiconDevice>();
        if (device != null) return;
        InputSystem.AddDevice<ProxiconDevice>("Proxicon");
    }

#if !UNITY_EDITOR

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize() {}

#endif
}