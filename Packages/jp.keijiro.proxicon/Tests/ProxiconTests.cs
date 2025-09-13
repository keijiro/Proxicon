using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class ProxiconTests
{
    Proxicon _device;

    [SetUp]
    public void Setup()
    {
        _device = Proxicon.current;
        if (_device == null)
        {
            _device = InputSystem.AddDevice<Proxicon>();
        }
        ProxiconAPI.ResetAll();
    }

    [TearDown]
    public void TearDown()
    {
        ProxiconAPI.ResetAll();
    }

    [Test]
    public void DeviceIsRegistered()
    {
        Assert.IsNotNull(Proxicon.current);
        Assert.IsTrue(InputSystem.devices.Contains(Proxicon.current));
    }

    [Test]
    public void ButtonsCanBePressed()
    {
        for (int i = 1; i <= 16; i++)
        {
            ProxiconAPI.SetButton(i, true);
            ProxiconAPI.UpdateDevice();

            Assert.IsTrue(ProxiconAPI.GetButton(i), $"Button {i} should be pressed");

            ProxiconAPI.SetButton(i, false);
            ProxiconAPI.UpdateDevice();

            Assert.IsFalse(ProxiconAPI.GetButton(i), $"Button {i} should be released");
        }
    }

    [UnityTest]
    public IEnumerator ButtonStateIsReflectedInInputSystem()
    {
        ProxiconAPI.PressButton(1);
        yield return null;

        Assert.IsTrue(_device.button1.isPressed);
        Assert.AreEqual(1f, _device.button1.ReadValue());

        ProxiconAPI.ReleaseButton(1);
        yield return null;

        Assert.IsFalse(_device.button1.isPressed);
        Assert.AreEqual(0f, _device.button1.ReadValue());
    }

    [Test]
    public void TogglesCanBeSwitched()
    {
        for (int i = 1; i <= 16; i++)
        {
            var initialState = ProxiconAPI.GetToggle(i);
            Assert.IsFalse(initialState, $"Toggle {i} should start as false");

            ProxiconAPI.SetToggle(i, true);
            ProxiconAPI.UpdateDevice();
            Assert.IsTrue(ProxiconAPI.GetToggle(i), $"Toggle {i} should be true");

            ProxiconAPI.SetToggle(i, false);
            ProxiconAPI.UpdateDevice();
            Assert.IsFalse(ProxiconAPI.GetToggle(i), $"Toggle {i} should be false");
        }
    }

    [UnityTest]
    public IEnumerator ToggleStateIsReflectedInInputSystem()
    {
        ProxiconAPI.SetToggle(1, true);
        ProxiconAPI.UpdateDevice();
        yield return null;

        Assert.IsTrue(_device.toggle1.isPressed);

        ProxiconAPI.ToggleSwitch(1);
        yield return null;

        Assert.IsFalse(_device.toggle1.isPressed);
    }

    [Test]
    public void KnobsCanBeSet()
    {
        for (int i = 1; i <= 16; i++)
        {
            ProxiconAPI.SetKnob(i, 0.5f);
            ProxiconAPI.UpdateDevice();

            Assert.AreEqual(0.5f, ProxiconAPI.GetKnob(i), 0.001f, $"Knob {i} should be 0.5");

            ProxiconAPI.SetKnob(i, 1.0f);
            ProxiconAPI.UpdateDevice();

            Assert.AreEqual(1.0f, ProxiconAPI.GetKnob(i), 0.001f, $"Knob {i} should be 1.0");

            ProxiconAPI.SetKnob(i, 0.0f);
            ProxiconAPI.UpdateDevice();

            Assert.AreEqual(0.0f, ProxiconAPI.GetKnob(i), 0.001f, $"Knob {i} should be 0.0");
        }
    }

    [UnityTest]
    public IEnumerator KnobStateIsReflectedInInputSystem()
    {
        ProxiconAPI.SetKnob(1, 0.75f);
        ProxiconAPI.UpdateDevice();
        yield return null;

        Assert.AreEqual(0.75f, _device.knob1.ReadValue(), 0.001f);

        ProxiconAPI.SetKnob(1, 0.25f);
        ProxiconAPI.UpdateDevice();
        yield return null;

        Assert.AreEqual(0.25f, _device.knob1.ReadValue(), 0.001f);
    }

    [Test]
    public void KnobValuesAreClamped()
    {
        ProxiconAPI.SetKnob(1, 2.0f);
        Assert.AreEqual(1.0f, ProxiconAPI.GetKnob(1), 0.001f, "Knob should be clamped to 1.0");

        ProxiconAPI.SetKnob(1, -1.0f);
        Assert.AreEqual(0.0f, ProxiconAPI.GetKnob(1), 0.001f, "Knob should be clamped to 0.0");
    }

    [Test]
    public void InvalidIndicesAreHandled()
    {
        ProxiconAPI.SetButton(0, true);
        ProxiconAPI.SetButton(17, true);
        Assert.IsFalse(ProxiconAPI.GetButton(0));
        Assert.IsFalse(ProxiconAPI.GetButton(17));

        ProxiconAPI.SetToggle(0, true);
        ProxiconAPI.SetToggle(17, true);
        Assert.IsFalse(ProxiconAPI.GetToggle(0));
        Assert.IsFalse(ProxiconAPI.GetToggle(17));

        ProxiconAPI.SetKnob(0, 0.5f);
        ProxiconAPI.SetKnob(17, 0.5f);
        Assert.AreEqual(0f, ProxiconAPI.GetKnob(0));
        Assert.AreEqual(0f, ProxiconAPI.GetKnob(17));
    }

    [UnityTest]
    public IEnumerator ResetAllClearsAllInputs()
    {
        for (int i = 1; i <= 16; i++)
        {
            ProxiconAPI.SetButton(i, true);
            ProxiconAPI.SetToggle(i, true);
            ProxiconAPI.SetKnob(i, 0.5f);
        }
        ProxiconAPI.UpdateDevice();
        yield return null;

        ProxiconAPI.ResetAll();
        yield return null;

        for (int i = 1; i <= 16; i++)
        {
            Assert.IsFalse(ProxiconAPI.GetButton(i), $"Button {i} should be reset");
            Assert.IsFalse(ProxiconAPI.GetToggle(i), $"Toggle {i} should be reset");
            Assert.AreEqual(0f, ProxiconAPI.GetKnob(i), 0.001f, $"Knob {i} should be reset");
        }

        Assert.IsFalse(_device.button1.isPressed);
        Assert.IsFalse(_device.toggle1.isPressed);
        Assert.AreEqual(0f, _device.knob1.ReadValue(), 0.001f);
    }

    [UnityTest]
    public IEnumerator MultipleInputsCanBeSetSimultaneously()
    {
        ProxiconAPI.SetButton(1, true);
        ProxiconAPI.SetButton(5, true);
        ProxiconAPI.SetToggle(3, true);
        ProxiconAPI.SetToggle(7, true);
        ProxiconAPI.SetKnob(2, 0.3f);
        ProxiconAPI.SetKnob(8, 0.7f);
        ProxiconAPI.UpdateDevice();

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