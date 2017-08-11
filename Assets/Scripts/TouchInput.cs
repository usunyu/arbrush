using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public PaintManager paintManager;
    private float tapElapsed = -1f;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (paintManager == null)
        {
            throw new System.Exception("Paint Manager not attached!");
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (tapElapsed >= 0f)
        {
            tapElapsed += Time.deltaTime;
        }
        foreach (Touch touch in Input.touches)
        {
            if (touch.tapCount == 2 && tapElapsed < 0f)
            {
                tapElapsed = 0f;
                TriggerDoubleTap();
            }
        }
        if (tapElapsed >= 1f)
        {
            // Reset
            tapElapsed = -1f;
        }
    }
    /// <summary>
    /// Triggers the double tap.
    /// </summary>
    private void TriggerDoubleTap()
    {
        paintManager.FreeFall();
    }
}
