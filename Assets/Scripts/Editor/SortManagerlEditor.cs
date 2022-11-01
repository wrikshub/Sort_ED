using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SortManager))]
public class SortManagerEditor : UnityEditor.Editor
{
    private SortManager sortManager;

    void OnSceneGUI()
    {
        EditorApplication.QueuePlayerLoopUpdate();
        if (Event.current.type == EventType.Repaint)
        {
            SceneView.RepaintAll();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        sortManager = target as SortManager;

        EditorApplication.QueuePlayerLoopUpdate();

        if (GUILayout.Button("Start"))
        {
            sortManager.StartSimulation();
        }

        if (GUILayout.Button("Pause/Resume"))
        {
            sortManager.PauseSimulation();
        }

        if (GUILayout.Button("Stop"))
        {
            sortManager.StopSimulation();
        }
    }
}