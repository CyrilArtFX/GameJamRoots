using UnityEngine;
using System.Collections.Generic;

public class LightReflector : MonoBehaviour
{
    private Dictionary<LightSource, LightSource> lightReflections = new();

    [SerializeField]
    private GameObject lightSourcePrefab;

    void FixedUpdate()
    {
        List<LightSource> reflectionsToRemove = new();

        foreach (LightSource originSource in lightReflections.Keys)
        {
            if (!lightReflections[originSource].ReceiveEmission)
            {
                reflectionsToRemove.Add(originSource);
            }
            else
            {
                lightReflections[originSource].ReceiveEmission = false;
            }
        }

        if (reflectionsToRemove.Count > 0)
        {
            for (int i = reflectionsToRemove.Count - 1; i >= 0; i--)
            {
                Destroy(lightReflections[reflectionsToRemove[i]].gameObject);
                lightReflections.Remove(reflectionsToRemove[i]);
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