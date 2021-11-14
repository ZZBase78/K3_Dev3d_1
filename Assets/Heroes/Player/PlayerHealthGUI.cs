using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthGUI : MonoBehaviour
{

    public Color color;

    private GUIStyle style;

    public void ColorChange()
    {
        style.normal.background = MakeTexture();
    }

    private void Awake()
    {
        color.r = 1;
        color.g = 0;
        color.b = 0;
        color.a = 1;

        style = new GUIStyle();
        ColorChange();
    }

    Texture2D MakeTexture()
    {
        Color[] pix = new Color[2 * 2];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = color;
        }
        Texture2D result = new Texture2D(2, 2);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    Rect PRect(float left_x, float left_y, float right_x, float right_y)
    {
        float leftx = Screen.width / 100f * left_x;
        float lefty = Screen.height / 100f * left_y;
        float rightx = Screen.width / 100f * right_x;
        float righty = Screen.height / 100f * right_y;

        float width = rightx - leftx;
        float hight = righty - lefty;

        return new Rect(leftx, lefty, width, hight);
    }

    private void OnGUI()
    {
        GUI.Box(PRect(70, 95, 99, 99), "", style);
    }

}
