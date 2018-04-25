/***************************** Team_Info.cs *****************************
 * Desc: Holds the Extra Life Team Information
 * Created By: Jacob Dockter
 * Last Edited On: 11/03/2017
 **********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Team_Info {
    public float totalRaisedAmount; // Really only need this
    public float fundraisingGoal;
    public string createdOn;
    public long timestamp;
    public string avatarImageUrl;
    public int teamID;
    public string name;
}
