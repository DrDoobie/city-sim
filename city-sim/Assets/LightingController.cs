using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingController : MonoBehaviour
{
    [Range(0.0f, 24.0f)]
    public float timeOfDay;
    public float cycleSpeed;
    public Light directionalLight;
    public LightingPreset lightingPreset;

    void Update()
    {
        if(lightingPreset == null)
            return;

        if(Application.isPlaying)
        {
            timeOfDay += (Time.deltaTime * cycleSpeed);

            timeOfDay %= 24.0f;

            UpdateLighting(timeOfDay / 24.0f);

            return;
        }

        UpdateLighting (timeOfDay / 24.0f);
    }

    void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightingPreset.ambientColor.Evaluate(timePercent);

        //RenderSettings.fogColor = lightingPreset.fogColor.Evaluate(timePercent);

        if(directionalLight  != null)
        {
            directionalLight.color = lightingPreset.directionalColor.Evaluate(timePercent);

            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360.0f - 90.0f), -170.0f, 0.0f));
        }
    }

    void OnValidate()
    {
        if(directionalLight != null)
            return;

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;

            return;
        }

        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach(Light light in lights)
        {
            if(light.type == LightType.Directional)
            {
                directionalLight = light;

                return;
            }
        }
    }
}
