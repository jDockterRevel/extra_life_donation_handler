/***************************** Extra_Life_API.cs *****************************
 * Desc: Holds the methods for retrieving data from the extra life site.
 * Created By: Jacob Dockter
 * Last Edited On: 04/25/2018
 ************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Extra_Life_API : MonoBehaviour {

    // Retrieve team info information from Extra Life
    public IEnumerator GetDonationTotal(string TeamID, System.Action<string> donationTotalSuccess)
    {
        string url = "https://www.extra-life.org/index.cfm?fuseaction=donordrive.team&teamID=" + TeamID + "&format=json";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.responseCode.ToString());
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            donationTotalSuccess(jsonString);
        }
    }

    // Retrieve Participant Donation information from Extra Life
    public IEnumerator GetParticipantDonations(string currentParticipant, System.Action<string> participantDonationSuccess)
    {
        string url = "https://www.extra-life.org/index.cfm?fuseaction=donorDrive.participantDonations&participantID=" + currentParticipant + "&format=json";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.responseCode.ToString());
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            participantDonationSuccess(jsonString);
        }
    }

    // Initial request to make sure team exists, and pull that teams data
    public IEnumerator GetTeamInfo(GameObject InvalidIDText, InputField TeamIDInputField, System.Action<string> TeamParticipants)
    {
        string TeamID = TeamIDInputField.text;
        string url = "https://www.extra-life.org/index.cfm?fuseaction=donorDrive.teamParticipants&teamID=" + TeamID + "&format=json";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            InvalidIDText.SetActive(true);
        }
        else
        {
            TeamParticipants(www.downloadHandler.text);
        }
    }
}
