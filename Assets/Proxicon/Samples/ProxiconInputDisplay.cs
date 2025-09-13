using UnityEngine;

public class ProxiconInputDisplay : MonoBehaviour
{
    [SerializeField] bool _showButtons = true;
    [SerializeField] bool _showToggles = true;
    [SerializeField] bool _showKnobs = true;
    [SerializeField] int _columns = 4;

    void OnGUI()
    {
        if (Proxicon.current == null)
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Proxicon device not found");
            return;
        }

        int yOffset = 10;

        if (_showButtons)
        {
            GUI.Label(new Rect(10, yOffset, 100, 20), "Buttons:");
            yOffset += 25;
            DrawInputGrid(yOffset, "B", (i) => ProxiconAPI.GetButton(i) ? "1" : "0",
                         (i) => ProxiconAPI.GetButton(i) ? Color.red : Color.white);
            yOffset += 80;
        }

        if (_showToggles)
        {
            GUI.Label(new Rect(10, yOffset, 100, 20), "Toggles:");
            yOffset += 25;
            DrawInputGrid(yOffset, "T", (i) => ProxiconAPI.GetToggle(i) ? "ON" : "OFF",
                         (i) => ProxiconAPI.GetToggle(i) ? Color.yellow : Color.white);
            yOffset += 80;
        }

        if (_showKnobs)
        {
            GUI.Label(new Rect(10, yOffset, 100, 20), "Knobs:");
            yOffset += 25;
            DrawInputGrid(yOffset, "K", (i) => ProxiconAPI.GetKnob(i).ToString("F2"),
                         (i) => Color.Lerp(Color.white, Color.green, ProxiconAPI.GetKnob(i)));
            yOffset += 80;
        }

        DrawInputSystemValues();
    }

    void DrawInputGrid(int yOffset, string prefix, System.Func<int, string> getValue, System.Func<int, Color> getColor)
    {
        for (int i = 1; i <= 16; i++)
        {
            int row = (i - 1) / _columns;
            int col = (i - 1) % _columns;

            int x = 10 + col * 60;
            int y = yOffset + row * 25;

            GUI.color = getColor(i);
            GUI.Label(new Rect(x, y, 55, 20), $"{prefix}{i:D2}: {getValue(i)}");
        }
        GUI.color = Color.white;
    }

    void DrawInputSystemValues()
    {
        GUI.Label(new Rect(300, 10, 200, 20), "Input System Values:");

        int yOffset = 35;
        var device = Proxicon.current;

        GUI.Label(new Rect(300, yOffset, 200, 20), $"Button1: {device.button1.ReadValue():F2}");
        yOffset += 20;
        GUI.Label(new Rect(300, yOffset, 200, 20), $"Toggle1: {device.toggle1.ReadValue():F2}");
        yOffset += 20;
        GUI.Label(new Rect(300, yOffset, 200, 20), $"Knob1: {device.knob1.ReadValue():F2}");
        yOffset += 30;

        GUI.Label(new Rect(300, yOffset, 200, 20), "Recent Events:");
        yOffset += 20;

        if (device.button1.wasPressedThisFrame)
            GUI.Label(new Rect(300, yOffset, 200, 20), "Button1 Pressed!");
        if (device.button1.wasReleasedThisFrame)
            GUI.Label(new Rect(300, yOffset, 200, 20), "Button1 Released!");
    }
}