using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomGUI : MonoBehaviour
{

    bool isSetup;
    Rect setup_window_rect;
    public int male;
    public int hair;
    public int head;
    public int torse;
    public int leg;
    public int sword;
    PlayerCustom playerCustom;

    PlayerHealthGUI health_bar_GUI;
    Color health_bar_color;

    private void Awake()
    {
        playerCustom = GetComponent<PlayerCustom>();
        playerCustom.isSword = true;

        health_bar_GUI = GetComponent<PlayerHealthGUI>();
        health_bar_color = health_bar_GUI.color;

    }

    private void SetupPlayer()
    {
        if (male == 0) playerCustom.male = true; else playerCustom.male = false;
        playerCustom.hair = hair;
        playerCustom.head = head;
        playerCustom.torse = torse;
        playerCustom.leg = leg;
        playerCustom.sword = sword;

        playerCustom.Custom_Changed();
    }

    private void Start()
    {
        isSetup = false;
        male = playerCustom.male ? 0 : 1;
        hair = playerCustom.hair;
        head = playerCustom.head;
        torse = playerCustom.torse;
        leg = playerCustom.leg;
        sword = playerCustom.sword;
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

    Rect PRect(Rect parent, float left_x, float left_y, float right_x, float right_y)
    {
        float leftx = parent.width / 100f * left_x;
        float lefty = parent.height / 100f * left_y;
        float rightx = parent.width / 100f * right_x;
        float righty = parent.height / 100f * right_y;

        float width = rightx - leftx;
        float hight = righty - lefty;

        return new Rect(leftx, lefty, width, hight);
    }
    Rect PRectABS(Rect parent, float left_x, float left_y, float right_x, float right_y)
    {
        float leftx = parent.width / 100f * left_x;
        float lefty = parent.height / 100f * left_y;
        float rightx = parent.width / 100f * right_x;
        float righty = parent.height / 100f * right_y;

        float width = rightx - leftx;
        float hight = righty - lefty;

        return new Rect(parent.x + leftx, parent.y + lefty, width, hight);
    }

    private void OnGUI()
    {

        if (GUI.Button(PRect(1.5f, 2.45f, 28.28f, 9.6f), "��������� ���������"))
        {
            isSetup = !isSetup;
        }

        if (isSetup)
        {
            setup_window_rect = GUI.Window(0, PRect(1.52f, 11.05f, 46.86f, 90f), SetupWindow, "���������");
            GUI.FocusWindow(0);
        }

    }

    void SetupWindow(int windowID)
    {
        //GUI.Button(PRect(setup_window_rect, 1, 1, 49, 10), "�������");
        //GUI.Button(PRect(setup_window_rect, 51, 11, 99, 20), "�������");

        male = GUI.Toolbar(PRect(setup_window_rect, 5, 5, 95, 10), male, new string[] { "�������", "�������" });
        if (GUI.changed) SetupPlayer();
        hair = GUI.Toolbar(PRect(setup_window_rect, 5, 13, 95, 18), hair, new string[] { "������ 1", "������ 2", "������ 3", "������ 4" });
        if (GUI.changed) SetupPlayer();
        head = GUI.Toolbar(PRect(setup_window_rect, 5, 21, 95, 26), head, new string[] { "������ 1", "������ 2", "������ 3" });
        if (GUI.changed) SetupPlayer();
        torse = GUI.Toolbar(PRect(setup_window_rect, 5, 29, 95, 34), torse, new string[] { "���� 1", "���� 2", "���� 3", "���� 4" });
        if (GUI.changed) SetupPlayer();
        leg = GUI.Toolbar(PRect(setup_window_rect, 5, 37, 95, 42), leg, new string[] { "���� 1", "���� 2", "���� 3", "���� 4" });
        if (GUI.changed) SetupPlayer();
        sword = GUI.SelectionGrid(PRect(setup_window_rect, 5, 45, 95, 55), sword, new string[] { "��� 1", "��� 2", "��� 3", "��� 4", "��� 5", "��� 6", "��� 7", "��� 8" }, 4);
        if (GUI.changed) SetupPlayer();

        GUI.Label(PRect(setup_window_rect, 5, 58, 95, 68), "���� ������ HP");

        health_bar_color = RGBSlider(PRect(setup_window_rect, 5, 65, 95, 85), health_bar_color);
        if (health_bar_color != health_bar_GUI.color)
        {
            health_bar_GUI.color = health_bar_color;
            health_bar_GUI.ColorChange();
        }
    }

    Color RGBSlider(Rect screenRect, Color rgb)
    {
        rgb.r = LabelSlider(PRectABS(screenRect, 0, 1, 100, 25), rgb.r, 1.0f, "Red");
        rgb.g = LabelSlider(PRectABS(screenRect, 0, 26, 100, 50), rgb.g, 1.0f, "Green");
        rgb.b = LabelSlider(PRectABS(screenRect, 0, 51, 100, 75), rgb.b, 1.0f, "Blue");
        rgb.a = LabelSlider(PRectABS(screenRect, 0, 76, 100, 100), rgb.a, 1.0f, "Alpha");
        return rgb;
    }
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText)
    {
        GUI.Label(PRectABS(screenRect, 0, 0, 30, 100), labelText);
        sliderValue = GUI.HorizontalSlider(PRectABS(screenRect, 31, 0, 100, 100), sliderValue, 0.0f, sliderMaxValue);
        return sliderValue;
    }
}
