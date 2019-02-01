using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDescription : MonoBehaviour
{
    public Transform canvas;

    void Update()
    {
        IsToggled();
    }

    public void Pause()
    {
        if (!canvas.gameObject.activeInHierarchy)
        {
            canvas.gameObject.SetActive(true);
        }

        else if (canvas.gameObject.activeInHierarchy)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void IsToggled()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pause();
        }
    }
}
