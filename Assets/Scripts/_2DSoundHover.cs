using UnityEngine;

public class _2DSoundHover : MonoBehaviour
{
    public string soundClip; // The name of the sound to play
    private bool isHovered = false;

    void Update()
    {
        // Perform a raycast from the mouse pointer in 2D space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            // Check if the raycast hits the object this script is attached to
            if (hit.transform == transform)
            {
                if (!isHovered) // If the mouse is hovering for the first time
                {
                    if (!string.IsNullOrEmpty(soundClip)) // Ensure the sound name is valid
                    {
                        AudioManager.Instance.PlaySFXLong(soundClip);
                    }
                    isHovered = true;
                }
            }
            else if (isHovered) // If the mouse is no longer hovering over the object
            {
                if (!string.IsNullOrEmpty(soundClip)) // Ensure the sound name is valid
                {
                    AudioManager.Instance.StopSFXLong(soundClip);
                }
                isHovered = false;
            }
        }
        else if (isHovered) // Reset hover status if raycast doesn't hit anything
        {
            isHovered = false;
            if (!string.IsNullOrEmpty(soundClip)) // Ensure the sound name is valid
            {
                AudioManager.Instance.StopSFXLong(soundClip);
            }
        }
    }
}
