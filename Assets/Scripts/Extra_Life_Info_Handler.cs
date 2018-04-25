/***************************** Extra_Life_Info_Handler.cs *****************************
 * Desc: Built for Extra as a simple way to distribute Extra Life Information for the Streaming
 * Includes: Top D, Most Recent Donation, List of Most 3 recent donation, and the total amount raised
 * Three text files are created on the desktop to be used for streaming, the application autogenerates these
 * files as often as it can and updates the 3 most recent donations at the same time.
 * Created By: Jacob Dockter
 * Last Edited On: 04/25/2018
 **********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class Extra_Life_Info_Handler : Extra_Life_API {

    // Properties ------------------------------------------------------------------------------------------------------
    
    string theTeamID; // Our Team ID is '35737' for 2017
    List<string> participants; // List of all participant ids

    double topDonationAmount = 0.0; // Holds top D
    string topDonationName = ""; // Holds the name of the top Donator

    double mostRecentDonationAmount = 0.0; // Holds most recent D
    string mostRecentDonationName = ""; // Holds most recent donator name
    long mostRecentDonationDate = 0; // Holds most recent donation date

    int participantsCount = 0;
    int maxParticipants = 0;

    // top three most recent donations
    List<Donation> recentThreeDonations;

    // Text Object
    public Text threeRecentDonationsText;

    // RunButton
    public GameObject RunButtonObject;

    // GoalHandler
    public GameObject GoalHandler;

    // Methods ------------------------------------------------------------------------------------------------------------

    // After successfully authenticating with a team, pass the team participants and the team ID to this object
    public void SetTeamInfo(string TeamID, string TeamParticipants)
    {
        theTeamID = TeamID;
        participants = new List<string>();
        recentThreeDonations = new List<Donation>();

        // Retrieve the participants from json
        List<Team_Participant> data;
        data = JsonConvert.DeserializeObject<List<Team_Participant>>(TeamParticipants);

        foreach (Team_Participant p in data)
        {
            participants.Add(p.participantID.ToString());
        }

        maxParticipants = participants.Count;

        if(maxParticipants == 0)
        {
            threeRecentDonationsText.text = "No Participants.";
            RunButtonObject.GetComponent<Button>().interactable = false;
        }
    }

    // Write the top donation and recent donation to text files
    void WriteDonationInfo()
    {
        // write the files
        string mrdPath = PlayerPrefs.GetString("recentDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\most_recent_donation.txt");
        string topDPath = PlayerPrefs.GetString("topDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\top_donation.txt");

        string topD = topDonationName + ": $" + topDonationAmount;
        string recentD = mostRecentDonationName + ": $" + mostRecentDonationAmount;

        File.WriteAllText(mrdPath, recentD);
        File.WriteAllText(topDPath, topD);

        // Update Top 3 Recent Donations Text
        threeRecentDonationsText.text = "";

        for (int i = 0; i < recentThreeDonations.Count; i++)
        {
            string tempText = "";

            // Set the Position
            string number = (i + 1).ToString();

            // Add the amount
            if (recentThreeDonations[i].donationAmount != null)
            {
                tempText = number + ". $" + recentThreeDonations[i].donationAmount + " - ";
            }
            else
            {
                tempText += number + ". $0.00 - ";
            }

            // Add the Name
            if (recentThreeDonations[i].donorName != null)
            {
                tempText += recentThreeDonations[i].donorName + ":" + '\n';
            }
            else
            {
                tempText += "Anonymous:" + '\n';
            }

            // Add the Message
            if (recentThreeDonations[i].message != null)
            {
                tempText += recentThreeDonations[i].message + '\n';
            }
            else
            {
                tempText += "No Message" + '\n';
            }
            threeRecentDonationsText.alignment = TextAnchor.UpperLeft;
            threeRecentDonationsText.text += tempText;
        }
    }

    // Handle text file creation
    public void UpdateData()
    {
        // Run the function to re-run this function after completion
        StartCoroutine(SetInfoAndRerunUpdate());

        // Print Donation Total to a Text File
        StartCoroutine(GetDonationTotal(theTeamID, donationTotalSuccess =>
        {

            Team_Info data = new Team_Info();
            // create or update the donation total text file
            data = JsonConvert.DeserializeObject<Team_Info>(donationTotalSuccess);

            // Set the information on the goal handler
            GoalHandler.GetComponent<GoalHandler>().SetGoalInformation(data.totalRaisedAmount, data.fundraisingGoal);

            string path = PlayerPrefs.GetString("totalDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\donation_total.txt");

            string donationTotal = "$" + data.totalRaisedAmount;

            File.WriteAllText(path, donationTotal);
        }));

        // Handle Participant Donations
        foreach (string participant in participants)
        {
            StartCoroutine(GetParticipantDonations(participant, participantDonationSuccess =>
            {
                List<Donation> data;
                data = JsonConvert.DeserializeObject<List<Donation>>(participantDonationSuccess);
                if(data.Count > 0)
                {
                    foreach(Donation d in data)
                    {
                        //Handle Top Donation
                        if (d.donationAmount != null)
                        {
                            double temp = double.Parse(d.donationAmount);

                            if (temp > topDonationAmount)
                            {
                                topDonationAmount = temp;
                                if (d.donorName == null)
                                {
                                    topDonationName = "Anonymous";
                                }
                                else
                                {
                                    topDonationName = d.donorName;
                                }
                            }
                        }

                        // Handle Most recent donation
                        if (d.timestamp > mostRecentDonationDate)
                        {
                            mostRecentDonationDate = d.timestamp;
                            if (d.donationAmount != null)
                            {
                                mostRecentDonationAmount = double.Parse(d.donationAmount);
                            }
                            else
                            {
                                mostRecentDonationAmount = 0;
                            }
                            if (d.donorName == null)
                            {
                                mostRecentDonationName = "Anonymous";
                            }
                            else
                            {

                                mostRecentDonationName = d.donorName;
                            }
                        }

                        // Handle 3 Most Recent Donations
                        // If there are no donations in here yet, just pull the first 3
                        if (recentThreeDonations.Count < 3 && !recentThreeDonations.Contains(d))
                        {
                            recentThreeDonations.Add(d);
                        }
                        else
                        {
                            // If the recent donations list is already 3, check if the timestamp is recent
                            // If the new timestamp is greater(newer) the the last one in the list
                            // remove the first one from the list, and add the new one to the list
                            Donation x = recentThreeDonations[2]; // Most recent donation

                            if (d.timestamp > x.timestamp)
                            {
                                recentThreeDonations[0] = recentThreeDonations[1];
                                recentThreeDonations[1] = x;
                                recentThreeDonations[2] = d;
                            }

                        }
                        
                    }
                }

                participantsCount += 1;

            }));
        }
    }

    // Waits until the information is updated, saves everything, then runs it again.
    IEnumerator SetInfoAndRerunUpdate()
    {
        yield return new WaitUntil(() => maxParticipants == participantsCount);

        // Write to Text Files
        WriteDonationInfo();
        
        // Reset participants count
        participantsCount = 0;

        // Run it again if the tool is still running
        if (RunButtonObject.GetComponent<RunCollection>().isCollecting)
        {
            UpdateData();
        }
    }
}
