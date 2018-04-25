/***************************** RunCollection.cs *****************************
 * Desc: Begin collecting data from Extra Life until told to stop
 * Created By: Jacob Dockter
 * Last Edited On: 04/16/2018
 **********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunCollection : MonoBehaviour {
    
    // Properties -----------------------------------------------------------------------------------
    public GameObject PathEditor;
    public GameObject TeamInfo;

    Button RunButton;
    public bool isCollecting = false;
    ColorBlock cb;
    
    // Methods --------------------------------------------------------------------------------------
    void Start () {
        RunButton = gameObject.GetComponent<Button>();
        cb = RunButton.colors;
        cb.normalColor = Color.green;
        RunButton.colors = cb;
        RunButton.onClick.AddListener(ToggleCollection);
    }

    // Start Collecting or Stop Collecting data from Extra Life Json
    void ToggleCollection()
    {
        // if the program isn't collecting data
        if (!isCollecting)
        {
            // Toggle PathEditor
            PathEditor.GetComponent<FilePathHandler>().ToggleInteractable();

            // Run Updates
            TeamInfo.GetComponent<Extra_Life_Info_Handler>().UpdateData();

            // Change button color to red
            cb.normalColor = Color.red;
            cb.highlightedColor = Color.red;
            RunButton.colors = cb;

            // Change text to Stop
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "Stop";

            // set isCollecting to true
            isCollecting = true;
        }
        else
        {
            // Toggle Path Editor
            PathEditor.GetComponent<FilePathHandler>().ToggleInteractable();

            // Change Color to Green
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
            RunButton.colors = cb;

            // Change text to Run
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "Run";

            // set isCollecting to false
            isCollecting = false;
        }
    }
}
