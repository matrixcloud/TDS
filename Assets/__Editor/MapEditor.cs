using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor {
    public override void OnInspectorGUI() {
        MapGenerator generator = target as MapGenerator;

        if (DrawDefaultInspector()) {
            generator.GenerateMap();
        }

        if (GUILayout.Button("Generate Map")) {
            generator.GenerateMap();
        }

    }
}
