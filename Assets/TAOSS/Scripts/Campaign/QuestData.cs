using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    // identifying info
    public string questKey;

    // quest progression
    public bool isLocked;
    public bool isVisible;
    public bool isInProgress;
    public bool isCompleted;
    public int currentQuestStep;
    public int[] questOutcomes = new int[16]; // test defining 16 int outcomes...

}

/// <summary>
/// Which index corresponds to what data in questOutcomes of QuestData
/// </summary>
[System.Serializable]
public enum QuestOutcome
{
    Completion, // 0 for not, 1 for yes,
    CompletionTimeSeconds, // quest completion timme in seconds (s)
    CompletionTimeMS, // quest completion time in miliseconds  (ms)
    StartTimeSeconds, // save the start time so we can calculate the duration (compltionTime - startTime) (s)
    StartTimeMilliseconds, // save the start time so we can calculate the duration (compltionTime - startTime) (mS)
    OutcomeSuccessType, // 0 for neutral, 1 for good, -1 for bad ...
    OutcomeSuccessValue, // a value associated with this type of OutcomeSuccessType
    OutcomeGoodType, // 0 for neutral, 1 for good, -1 for bad ...
    OutcomeGoodValue, // a value associated with this type of OutcomeGoodType
    RESERVED_00, // extra data for now... want a consistent 16 ints per quest?
    RESERVED_01,
    RESERVED_02,
    RESERVED_03,
    RESERVED_04,
    RESERVED_05,
    RESERVED_06,
}
