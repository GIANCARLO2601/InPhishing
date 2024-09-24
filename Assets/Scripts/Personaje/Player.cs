using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 defaultStartPosition; // Assign this in the Inspector for the starting position

    private void Start()
    {
        // Check if returning from combat
        if (GameManager.Instance != null && GameManager.Instance.isReturningFromCombat)
        {
            // Restore the saved position if returning from combat
            if (PlayerPrefs.HasKey("PlayerX"))
            {
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                float z = PlayerPrefs.GetFloat("PlayerZ");
                transform.position = new Vector3(x, y, z);
                Debug.Log("Restoring Player Position after Combat: " + transform.position);
            }
        }
        else
        {
            // Start at the default position if not returning from combat
            transform.position = defaultStartPosition;
            Debug.Log("Starting Player at Default Position: " + defaultStartPosition);
        }
    }
}
