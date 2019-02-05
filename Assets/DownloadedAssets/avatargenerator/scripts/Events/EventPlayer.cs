using System.Linq;
using UnityEngine;

public class EventPlayer
{
    public static void PlayEventAmbience(EventObject eventObject)
    {
        PlayAudioSource(eventObject.gameObject, "Play");

        switch (eventObject.AnimationName)
        {
            case "light_switch":
                SwitchLights();
                break;
            case "Celebration":
                SetLightColor(Color.green);
                break;
            case "AlarmBell":
                SetLightColor(Color.red);
                break;
            case "Faint":
                var randomNpc = NpcObject.AllPersons[Random.Range(0, NpcObject.AllPersons.Count)];
                randomNpc.IsEventActor = true;
                break;
        }
    }

    public static void RemoveAmbience(EventObject eventObject)
    {
        PlayAudioSource(eventObject.gameObject, "Stop");

        switch (eventObject.AnimationName)
        {
            //no case for lightswitching since those can be, well, switched
            case "AlarmBell":
                SetLightColor(Color.white);
                break;
            case "Celebration":
                SetLightColor(Color.white);
                break;
        }
    }

    private static void PlayAudioSource(GameObject gameObject, string desiredState)
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            if (desiredState.Equals("Play"))
                gameObject.GetComponent<AudioSource>().Play();
            else
                gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    private static void SwitchLights()
    {
        foreach (var light in GetAllLights().Select(x => x.GetComponent<Light>()))
        {
            light.enabled = !light.enabled;
        }
    }

    private static void SetLightColor(Color color)
    {
        foreach (var light in GetAllLights().Select(x => x.GetComponent<Light>()))
        {
            if (!light.color.Equals(color))
                light.color = color;
        }
    }

    private static GameObject[] GetAllLights()
    {
        return GameObject.FindGameObjectsWithTag("ceiling_light");
    }
}