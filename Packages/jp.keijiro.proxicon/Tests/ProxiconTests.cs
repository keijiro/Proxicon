using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class ProxiconTests
{
    ProxiconDevice _device;

    [SetUp]
    public void Setup()
    {
        _device = ProxiconDevice.current;
        if (_device == null)
        {
            _device = InputSystem.AddDevice<ProxiconDevice>();
        }
        Proxicon.ResetAll();
    }

    [TearDown]
    public void TearDown()
    {
        Proxicon.ResetAll();
    }

    [Test]
    public void DeviceIsRegistered()
    {
        Assert.IsNotNull(ProxiconDevice.current);
        Assert.IsTrue(InputSystem.devices.Contains(ProxiconDevice.current));
    }

    [Test]
    public void ButtonsCanBePressed()
    {
        for (int i = 1; i <= 16; i++)
        {
            Proxicon.SetButton(i, true);
            Proxicon.UpdateDevice();

            Assert.IsTrue(Proxicon.GetButton(i), $"Button {i} should be pressed");

            Proxicon.SetButton(i, false);
            Proxicon.UpdateDevice();

            Assert.IsFalse(Proxicon.GetButton(i), $"Button {i} should be released");
        }
    }

    [UnityTest]
    public IEnumerator ButtonStateIsReflectedInInputSystem()
    {
        Proxicon.PressButton(1);
        yield return null;

        Assert.IsTrue(_device.button1.isPressed);
        Assert.AreEqual(1f, _device.button1.ReadValue());

        Proxicon.ReleaseButton(1);
        yield return null;

        Assert.IsFalse(_device.button1.isPressed);
        Assert.AreEqual(0f, _device.button1.ReadValue());
    }

    [Test]
    public void TogglesCanBeSwitched()
    {
        for (int i = 1; i <= 16; i++)
        {
            var initialState = Proxicon.GetToggle(i);
            Assert.IsFalse(initialState, $"Toggle {i} should start as false");

            Proxicon.SetToggle(i, true);
            Proxicon.UpdateDevice();
            Assert.IsTrue(Proxicon.GetToggle(i), $"Toggle {i} should be true");

            Proxicon.SetToggle(i, false);
            Proxicon.UpdateDevice();
            Assert.IsFalse(Proxicon.GetToggle(i), $"Toggle {i} should be false");
        }
    }

    [UnityTest]
    public IEnumerator ToggleStateIsReflectedInInputSystem()
    {
        Proxicon.SetToggle(1, true);
        Proxicon.UpdateDevice();
        yield return null;

        Assert.IsTrue(_device.toggle1.isPressed);

        Proxicon.ToggleSwitch(1);
        yield return null;

        Assert.IsFalse(_device.toggle1.isPressed);
    }

    [Test]
    public void KnobsCanBeSet()
    {
        for (int i = 1; i <= 16; i++)
        {
            Proxicon.SetKnob(i, 0.5f);
            Proxicon.UpdateDevice();

            Assert.AreEqual(0.5f, Proxicon.GetKnob(i), 0.001f, $"Knob {i} should be 0.5");

            Proxicon.SetKnob(i, 1.0f);
            Proxicon.UpdateDevice();

            Assert.AreEqual(1.0f, Proxicon.GetKnob(i), 0.001f, $"Knob {i} should be 1.0");

            Proxicon.SetKnob(i, 0.0f);
            Proxicon.UpdateDevice();

            Assert.AreEqual(0.0f, Proxicon.GetKnob(i), 0.001f, $"Knob {i} should be 0.0");
        }
    }

    [UnityTest]
    public IEnumerator KnobStateIsReflectedInInputSystem()
    {
        Proxicon.SetKnob(1, 0.75f);
        Proxicon.UpdateDevice();
        yield return null;

        Assert.AreEqual(0.75f, _device.knob1.ReadValue(), 0.001f);

        Proxicon.SetKnob(1, 0.25f);
        Proxicon.UpdateDevice();
        yield return null;

        Assert.AreEqual(0.25f, _device.knob1.ReadValue(), 0.001f);
    }

    [Test]
    public void KnobValuesAreClamped()
    {
        Proxicon.SetKnob(1, 2.0f);
        Assert.AreEqual(1.0f, Proxicon.GetKnob(1), 0.001f, "Knob should be clamped to 1.0");

        Proxicon.SetKnob(1, -1.0f);
        Assert.AreEqual(0.0f, Proxicon.GetKnob(1), 0.001f, "Knob should be clamped to 0.0");
    }

    [Test]
    public void InvalidIndicesAreHandled()
    {
        Proxicon.SetButton(0, true);
        Proxicon.SetButton(17, true);
        Assert.IsFalse(Proxicon.GetButton(0));
        Assert.IsFalse(Proxicon.GetButton(17));

        Proxicon.SetToggle(0, true);
        Proxicon.SetToggle(17, true);
        Assert.IsFalse(Proxicon.GetToggle(0));
        Assert.IsFalse(Proxicon.GetToggle(17));

        Proxicon.SetKnob(0, 0.5f);
        Proxicon.SetKnob(17, 0.5f);
        Assert.AreEqual(0f, Proxicon.GetKnob(0));
        Assert.AreEqual(0f, Proxicon.GetKnob(17));
    }

    [UnityTest]
    public IEnumerator ResetAllClearsAllInputs()
    {
        for (int i = 1; i <= 16; i++)
        {
            Proxicon.SetButton(i, true);
            Proxicon.SetToggle(i, true);
            Proxicon.SetKnob(i, 0.5f);
        }
        Proxicon.UpdateDevice();
        yield return null;

        Proxicon.ResetAll();
        yield return null;

        for (int i = 1; i <= 16; i++)
        {
            Assert.IsFalse(Proxicon.GetButton(i), $"Button {i} should be reset");
            Assert.IsFalse(Proxicon.GetToggle(i), $"Toggle {i} should be reset");
            Assert.AreEqual(0f, Proxicon.GetKnob(i), 0.001f, $"Knob {i} should be reset");
        }

        Assert.IsFalse(_device.button1.isPressed);
        Assert.IsFalse(_device.toggle1.isPressed);
        Assert.AreEqual(0f, _device.knob1.ReadValue(), 0.001f);
    }

    [UnityTest]
    public IEnumerator MultipleInputsCanBeSetSimultaneously()
    {
        Proxicon.SetButton(1, true);
        Proxicon.SetButton(5, true);
        Proxicon.SetToggle(3, true);
        Proxicon.SetToggle(7, true);
        Proxicon.SetKnob(2, 0.3f);
        Proxicon.SetKnob(8, 0.7f);
        Proxicon.UpdateDevice();

        yield return null;

        Assert.IsTrue(_device.button1.isPressed);
        Assert.IsTrue(_device.button5.isPressed);
        Assert.IsFalse(_device.button2.isPressed);

        Assert.IsTrue(_device.toggle3.isPressed);
        Assert.IsTrue(_device.toggle7.isPressed);
        Assert.IsFalse(_device.toggle1.isPressed);

        Assert.AreEqual(0.3f, _device.knob2.ReadValue(), 0.001f);
        Assert.AreEqual(0.7f, _device.knob8.ReadValue(), 0.001f);
        Assert.AreEqual(0f, _device.knob1.ReadValue(), 0.001f);
    }
}