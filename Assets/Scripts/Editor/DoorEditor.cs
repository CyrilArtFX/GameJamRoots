using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Door door = (Door)target;

        DrawDefaultInspector();
        
        if(GUILayout.Button("Set End Position"))
        {
            door.RecordEndPosition();
        }
    }
}
