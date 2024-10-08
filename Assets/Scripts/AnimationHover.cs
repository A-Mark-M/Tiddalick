using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHover : MonoBehaviour

{
    public Animator animator;
    private bool isHovered = false;

    void Update()
    {
        // Perform a raycast from the mouse pointer to detect the object
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hits the object this script is attached to
            if (hit.transform == transform)
            {
                if (!isHovered) // If the mouse is hovering for the first time
                {
                    animator.SetBool("IsHovered", true); // Start the animation
                    isHovered = true;
                }
            }
            else
            {
                if (isHovered) // If the mouse is no longer hovering
                {
                    animator.SetBool("IsHovered", false); // Stop or reverse the animation
                    isHovered = false;
                }
            }
        }
        else
        {
            // If no object is hit by the raycast, ensure animation is stopped
            if (isHovered)
            {
                animator.SetBool("IsHovered", false);
                isHovered = false;
            }
        }
    }
}
