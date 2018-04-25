/***************************** Extra_Life_Manager.cs *****************************
 * Desc: Built for Extra as a simple way to distribute Extra Life Information for the Streaming
 * Handles acceptance of Team ID to pull appropriate information.
 * Created By: Jacob Dockter
 * Last Edited On: 04/25/2018
 **********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Extra_Life_Manager : Extra_Life_API {

    // Properties -------------------------------------------------------------------------------------
    
    public InputField TeamIDInputField;
    public GameObject LoadButtonObject;
    public GameObject TeamInfo;
    public GameObject InvalidIDText;
    public GameObject PathEditor;
    public GameObject GoalContainer;
    public GameObject RunButton;

    Button LoadButton;

    // Methods ----------------------------------------------------------------------------------------

    // Set Button and InputField Actions at start
    void Start () {
        LoadButton = LoadButtonObject.GetComponent<Button>();
        TeamIDInputField.onValueChanged.AddListener(delegate { InputTextChanged(); });
        LoadButton.onClick.AddListener(SetTeamInfoData);
	}

    // If the Input Text is not Empty, enable the loading button
    void InputTextChanged()
    {
        if (TeamIDInputField.text.Length > 0)
            LoadButton.interactable = true;
        else
            LoadButton.interactable = false;
    }

    // Run the Setup to display the information based on the team and hdie the button
    void SetTeamInfoData()
    {
        StartCoroutine(GetTeamInfo(InvalidIDText, TeamIDInputField, TeamParticipants => {
            PathEditor.SetActive(true);
            GoalContainer.SetActive(true);
            RunButton.SetActive(true);
            TeamInfo.SetActive(true);
            TeamInfo.GetComponent<Extra_Life_Info_Handler>().SetTeamInfo(TeamIDInputField.text, TeamParticipants);
            gameObject.SetActive(false);
        }));
    }

}
