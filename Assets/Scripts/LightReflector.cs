using UnityEngine;
using System.Collections.Generic;

public class LightReflector : MonoBehaviour
{
    private Dictionary<LightSource, LightSource> lightReflections = new();

    [SerializeField]
    private GameObject lightSourcePrefab;

    void FixedUpdate()
    {
        foreach (LightSource originSource in lightReflections.Keys)
        {
            if (!lightReflections[originSource].ReceiveEmission)
            {
                Destroy(lightReflections[originSource].gameObject);
                lightReflections.Remove(originSource);
            }
            else
            {
                lightReflections[originSource].ReceiveEmission = false;
            }
        }
    }

    public void ReceiveLight(Vector3 impactPoint, Vector3 lightDirection, Vector3 impactNormal, LightSource lightSource)
    {
        if (!lightReflections.ContainsKey(lightSource))
        {
            lightReflections.Add(lightSource, Instantiate(lightSourcePrefab).GetComponent<LightSource>());
        }
        lightReflections[lightSource].ReceiveEmission = true;

        Vector3 reflection_direction = Vector3.Reflect(lightDirection, impactNormal);
        reflection_direction.Normalize();

        lightReflections[lightSource].SetStartPointAndDirection(impactPoint, reflection_direction);
    }
}