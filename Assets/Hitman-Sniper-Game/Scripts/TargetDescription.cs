using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDescription : MonoBehaviour
{
    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private Transform noteBookText;

    [SerializeField]
    private List<Text> descriptionText;

    [SerializeField]
    private List<NPCBehaviour> Targets;

    void Update()
    {
        IsToggled();
    }

    public void Start()
    {
        PickTarget();
    }

    public void PickTarget()
    {
        if (Targets.Count == 0)
        {
            return;
        }

        NPCBehaviour target = Targets[Random.Range(0, Targets.Count)];
        for (int i = 0; i < target.targetDescriptions.Count; i++)
        {
            descriptionText[i].text = target.targetDescriptions[i];
        }

        target.isTarget = true;
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
