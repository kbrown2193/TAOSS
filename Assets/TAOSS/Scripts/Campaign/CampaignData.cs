using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampaignData
{
    public string campaignKey; // identifier, something like UNIVERSE_STORYLINE_CAMPAIGN ... KB_TAOSS_C0
    public string campaignName; // user saved campaign name
    public QuestData[] questDatas;
}
