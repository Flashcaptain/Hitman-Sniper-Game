using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDescription : MonoBehaviour
{
    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private Transform noteBookText;

    void Update()
    {
        IsToggled();
    }

    public void OpenNotebook()
    {
        if (!canvas.gameObject.activeInHierarchy)
        {
            canvas.gameObject.SetActive(true);
            noteBookText.gameObject.SetActive(false);
        }

        else if (canvas.gameObject.activeInHierarchy)
        {
            canvas.gameObject.SetActive(false);
            noteBookText.gameObject.SetActive(true);
        }
    }

    public void IsToggled()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenNotebook();
        }
    }
}
