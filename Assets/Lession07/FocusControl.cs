using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FocusControl : MonoBehaviour
{

    public GameObject _cam;

    public PostProcessProfile profile;

    public LayerMask layerMask;

    DepthOfField depth;

    public float speed_change = 1f;

    // Start is called before the first frame update
    void Start()
    {
        depth = profile.GetSetting<DepthOfField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            depth.active = !depth.active;
        }

        if (depth.active)
        {
            RaycastHit hit;

            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, Mathf.Infinity, layerMask.value, QueryTriggerInteraction.Ignore))
            {

                depth.focusDistance.value = Mathf.Lerp(depth.focusDistance.value, hit.distance, Time.deltaTime * speed_change);

                //depth.focusDistance.value = hit.distance;
            }
        }
    }
}
