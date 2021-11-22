using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightControl : MonoBehaviour
{

    [SerializeField, Range(0f, 1f)] float time_of_day;
    [SerializeField] float day_duration = 60f; // 60 секунд на сутки по умолчанию

    [SerializeField] GameObject sun;
    Light sun_light;
    [SerializeField] AnimationCurve sun_light_curve;
    [SerializeField] float sun_max_intensity = 2f;

    [SerializeField] Material day_skybox;
    [SerializeField] Material night_skybox;


    private void Awake()
    {
        sun_light = sun.GetComponent<Light>();
    }
    // Start is called before the first frame update
    void Start()
    {
        time_of_day = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time_of_day = Mathf.Repeat(time_of_day + Time.deltaTime / day_duration, 1f);

        sun_light.transform.rotation = Quaternion.Euler(time_of_day * 360f, 0f, 0f);
        sun_light.intensity = sun_light_curve.Evaluate(time_of_day) * sun_max_intensity;

        RenderSettings.skybox.Lerp(night_skybox, day_skybox, sun_light_curve.Evaluate(time_of_day));
        DynamicGI.UpdateEnvironment();
    }
}
