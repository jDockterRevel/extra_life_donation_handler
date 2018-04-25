/***************************** FilePathHandler.cs *****************************
 * Desc: Handles the editing of the file paths to which the information is saved.
 * Created By: Jacob Dockter
 * Last Edited On: 04/25/2018
 **********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilePathHandler : MonoBehaviour {

    // Properties ----------------------------------------------------------------------------------------------------------------------
    public InputField recentDonationPath;
    public InputField topDonationPath;
    public InputField totalDonationPath;
    public GameObject loadButtonObject;

    Button savePathButton;
    
    // Methods -------------------------------------------------------------------------------------------------------------------------
	void Start () {
        // Set Path Input Fields
        SetCurrentPathText();

        // Create Button Listener
        savePathButton = loadButtonObject.GetComponent<Button>();
        recentDonationPath.onValueChanged.AddListener(delegate { InputTextChanged(); });
        savePathButton.onClick.AddListener(SavePathData);
    }

    // Enable/Disable input fields for paths
    public void ToggleInteractable()
    {
        // Reset Path Fields to current
        SetCurrentPathText();

        // Toggle whether or not the forms on the Path Editor are interactable
        recentDonationPath.interactable = !recentDonationPath.interactable;
        topDonationPath.interactable = !topDonationPath.interactable;
        totalDonationPath.interactable = !totalDonationPath.interactable;
        loadButtonObject.GetComponent<Button>().interactable = false;
    }

    // Set the input fields to the player prefs saved variables
    void SetCurrentPathText()
    {
        string rDP = PlayerPrefs.GetString("recentDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\most_recent_donation.txt");
        recentDonationPath.text = rDP;

        // Get the current Top Donation Path, if it doesn't exist, default to desktop directory
        string tDP = PlayerPrefs.GetString("topDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\top_donation.txt");
        topDonationPath.text = tDP;

        // Get the current Total Donation Path, if it doesn't exist, deafult to desktop directory
        string totDP = PlayerPrefs.GetString("totalDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\donation_total.txt");
        totalDonationPath.text = totDP;
    }

    // If any field has changed, enable the button
    void InputTextChanged()
    {
        savePathButton.interactable = true;
    }

    // Save the text path inputs to the Path Player Prefs
    void SavePathData()
    {   // if the path exists, save it, if it doesn't reset to default
        // recent donations
        if(System.IO.Directory.Exists(recentDonationPath.text))
            PlayerPrefs.SetString("recentDonationPath", recentDonationPath.text);
        else
            PlayerPrefs.SetString("recentDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\most_recent_donation.txt");

        // top donations
        if (System.IO.Directory.Exists(topDonationPath.text))
            PlayerPrefs.SetString("topDonationPath", topDonationPath.text);
        else
            PlayerPrefs.SetString("recentDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\top_donation.txt");

        // total donations
        if (System.IO.Directory.Exists(totalDonationPath.text))
            PlayerPrefs.SetString("totalDonationPath", totalDonationPath.text);
        else
            PlayerPrefs.SetString("recentDonationPath", System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\donation_total.txt");

        // Save
        PlayerPrefs.Save();
    }
}
