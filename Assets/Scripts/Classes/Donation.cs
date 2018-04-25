/***************************** Donation.cs *****************************
 * Desc: Holds an Extra Life Donation and it's information
 * Created By: Jacob Dockter
 * Last Edited On: 11/03/2017
 **********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.ComponentModel;

[System.Serializable]
public class Donation {
    [JsonProperty(PropertyName = "message", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
    [DefaultValue("No Message")]
    public string message;
    public string createdOn;
    public long timestamp;
    public string avatarImageURL;
    [JsonProperty(PropertyName = "donationAmount", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
    [DefaultValue("0")]
    public string donationAmount;
    [JsonProperty(PropertyName = "donorName", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
    [DefaultValue("Anonymous")]
    public string donorName { get; set; }
}
