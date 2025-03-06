using UnityEngine;
using TMPro;

public class MovementSpeed : MonoBehaviour
{
    [Header("UI Element")]
    [SerializeField] TextMeshProUGUI targetText;  // Assign your TextMeshProUGUI element here

    [Header("Value Source")]
    [SerializeField] PlayerController player;     // Reference to the script that holds the value

    [Header("Display Options")]
    [SerializeField] string format = "F1"; // e.g., one decimal place

    void Update()
    {
        if (targetText != null && player != null)
        {
            // Update the text to match the value from the ValueHolder script.
            targetText.text = "Speed: " + (player.moveSpeed).ToString(format);
        }
    }
}