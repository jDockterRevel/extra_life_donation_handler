/***************************** GoalHandler.cs *****************************
 * Desc: Set's the Goal Progress bar based on the information passed from
 * the Extra Life Site
 * Created By: Jacob Dockter
 * Last Edited On: 04/25/2018
 **********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalHandler : MonoBehaviour {

    // Properties ----------------------------------------------------------------------------------------------
    public Text ProgressText;
    public Slider ProgressBar;

    // Methods -------------------------------------------------------------------------------------------------

    // Set the Goal Information
	public void SetGoalInformation(float totalRaised, float goalAmount)
    {
        // Set the Progress Bar to the totalRaised / goalAmount
        ProgressBar.maxValue = goalAmount;
        ProgressBar.value = totalRaised;

        // Set the Progress Text to the totalRaised / goalAmount
        ProgressText.text = "$" + totalRaised.ToString() + "/" + goalAmount.ToString();
    }
}
