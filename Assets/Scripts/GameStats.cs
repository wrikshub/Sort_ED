using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public static class GameStats
{
    public static readonly ProfilerCategory Sortysort = ProfilerCategory.Scripts;

    public const string millisecond = "time";

    public static readonly ProfilerCounterValue<float> SortTime =
        new ProfilerCounterValue<float>(Sortysort, millisecond, ProfilerMarkerDataUnit.Count,
            ProfilerCounterOptions.FlushOnEndOfFrame | ProfilerCounterOptions.ResetToZeroOnFlush);
}
