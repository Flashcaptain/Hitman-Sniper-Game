﻿﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SettingsParser
{
    private const string SETTINGS = "settings";

    public static List<string> ModelNames = new List<string>();
    public static List<Vector3> Spawnpoints = new List<Vector3>();

    public static void ParseAll()
    {
        var settings = XmlNodule.Load(SETTINGS);
        ParseSpawnPoints(settings);
        ParseModelNames(settings);
    }

    public static void ParsePersonalitiesXml(bool defaultValue, List<AvatarGenerator.Personality> personalities)
    {
        var pathToSettings = Application.dataPath + "\\Resources\\settings.xml";

        if (defaultValue)
            personalities.Clear();

        var personalityDict = personalities.ToDictionary(item => item.Name,
            item => item.ToTuple());

        var settings = XmlNodule.Load(SETTINGS);

        foreach (var personality in settings.Get("personalities"))
        {
            if (defaultValue)
            {
                personalities.Add(ParsePersonality(personality));
            }
            else
            {
                personality.Get("minvalue").Set(personalityDict[personality.Get("name").ToString()].Item1);
                personality.Get("maxvalue").Set(personalityDict[personality.Get("name").ToString()].Item2);
            }
        }
        settings.Save(pathToSettings);
    }

    private static void ParseSpawnPoints(XmlNodule settings)
    {
        var xmlSpawnPoints = settings.Get("spawnpoints");
        foreach (var xmlspawnpoint in xmlSpawnPoints)
        {
            Spawnpoints.Add(xmlspawnpoint.ToVector3());
        }
    }

    private static void ParseModelNames(XmlNodule settings)
    {
        var xmlModelNames = settings.Get("modelnames");
        foreach (var xmlModelName in xmlModelNames)
        {
            ModelNames.Add(xmlModelName.ToString());
        }
    }

    private static AvatarGenerator.Personality ParsePersonality(XmlNodule nodule)
    {
        var personality = new AvatarGenerator.Personality
        {
            Name = nodule.Get("name").ToString(),
            Id = nodule.Get("id").ToInt(),
            MinValue = nodule.Get("minvalue").ToInt(),
            MaxValue = nodule.Get("maxvalue").ToInt()
        };

        return personality;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           