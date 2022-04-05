using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_PlayerInputs : MonoBehaviour
{
    private Vector2 startPos;
    public int minimumSwipeDistance = 50;
    private bool fingerDown;

    // Update is called once per frame
    void Update()
    {
#if PLATFORM_ANDROID

        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.touches[0].position.y >= startPos.y + minimumSwipeDistance)
            {
                fingerDown = false;
                Debug.Log("Upwards Swipe");
            }
        }

#else
        #region testing on pc

        if (fingerDown == false && Input.GetKeyDown(KeyCode.Mouse0))
            {
                startPos = Input.mousePosition;
                fingerDown = true;
            }

            if (fingerDown)
            {
                if (Input.mousePosition.y >= startPos.y + minimumSwipeDistance)
                {
                    fingerDown = false;
                    Debug.Log("Upwards Swipe");
                }
            }

            if (fingerDown && Input.GetKeyUp(KeyCode.Mouse0))
            {
                fingerDown = false;
            }

    #endregion
#endif
        


    }
}
