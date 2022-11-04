using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SortRecorder : MonoBehaviour
{
    [SerializeField] private string filename;
    [SerializeField] private string identifier = "{0} FPS";
    private UnityEngine.Profiling.Profiler profiler;

    private void Start()
    {
        WriteResultsToFile();
    }

    private void WriteResultsToFile()
    {
        using (StreamWriter streamWriter = new StreamWriter(filename))
        {
            streamWriter.Write("Circle Count");
            

            //if (0 < columnIdentifiers.Count)
            //{
            //    streamWriter.Write("; ");
            //}
        }
    }
}