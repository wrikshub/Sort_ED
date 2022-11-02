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
        
        EditorGUILayout.Space();
        var rect = EditorGUILayout.BeginHorizontal();
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        var guiStyle = new GUIStyle();
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        GUILayout.Label("Sort Control", guiStyle);
        
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        sortManager.ShouldSort = GUILayout.Toggle(sortManager.ShouldSort, "Toggle Sorting");
        GUILayout.Label("Balls: " + sortManager.balls.Count);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Start")) { sortManager.StartSimulation(); }

        string pauseButtonText = sortManager.Running ? "Pause" : "  Run  ";
        
        if (GUILayout.Button(pauseButtonText)) { sortManager.PauseSimulation(); }
        if (GUILayout.Button("Stop")) { sortManager.StopSimulation(); }
        
        EditorGUILayout.EndHorizontal();
    }
}