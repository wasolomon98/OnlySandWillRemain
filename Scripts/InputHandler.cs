using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    /*
    public TMP_InputField input_field; // Reference to the Input Field
    public TMP_Text name_display;       // Reference to the Text component for displaying the name

    public TMP_Text activity_display;
    public ActivityManager activity_manager;

    private void Start()
    {
        // Clear the input field at start
        input_field.text = "";
        
        // Add listener for when the user presses Enter
        input_field.onEndEdit.AddListener(OnInputSubmit);
    }

    private void FixedUpdate() 
    {
  
    }

    private void OnInputSubmit(string input)
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Check if Enter was pressed
        {
            ParseInput(input);
            input_field.text = ""; // Clear the input field after processing
        }     
    }

    private void ParseInput(string input)
    {
        // Check for 'set_name [NAME]' command
        if (input.StartsWith("set_name "))
        {
            string name = input.Substring("set_name ".Length).Trim();
            UpdateNameDisplay(name);
        }
        else if (input.StartsWith("set_activity "))
        {
            string activity = input.Substring("set_activity ".Length).Trim();
            SetActivity(activity);
        } 
        else
        {

        }
    }
    
    private void SetActivity(string activity)
    {
        if (activity == "loiter")
        {
            activity_manager.SetNewActivity(ActivityManager.Activity.Loitering);
        }
        else if (activity == "train ") 
        {
            activity_manager.SetNewActivity(ActivityManager.Activity.Train);
        }
        else
        {
            Debug.Log("Error: Activity not recognized");
        }
    }

    private void UpdateNameDisplay(string name)
    {
        name_display.text = $"Name: {name}"; // Update the text component
    }
    */
}
