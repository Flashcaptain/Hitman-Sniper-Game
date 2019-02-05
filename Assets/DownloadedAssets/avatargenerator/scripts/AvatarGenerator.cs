﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
using Casanova.Prelude;
using UnityEngine;

public class AvatarGenerator : MonoBehaviour
{
    public static int TotalAmountOfEvents = ActionsParser.Events.Count;

    //              Inspector values                //
    [Header("Automatically generate chacarcters")] public bool AutoGenerateCharacters = true;

    [Header("Amount of characters")] [Range(1, 30)] public int Amount;

    [Header("Use default (stored) values")] public bool DefaultValue = true;

    [Header("Personalities")] public List<Personality> Personalities;

    //              Properties & behaviour          //
    public List<Tuple<int, Tuple<int, int>>> SettingsList
    {
        get
        {
            return (from personality in Personalities
                let minMaxValues = new Tuple<int, int>(personality.MinValue, personality.MaxValue)
                select new Tuple<int, Tuple<int, int>>(personality.Id, minMaxValues)).ToList();
        }
    }

    public static AvatarGenerator Find()
    {
        var avatarGenerator = GameObject.Find("AvatarGenerator").GetComponent<AvatarGenerator>();
        if (!avatarGenerator.AutoGenerateCharacters)
        {
            SettingsParser.ParsePersonalitiesXml(avatarGenerator.DefaultValue, avatarGenerator.Personalities);
        }
        return avatarGenerator;
    }

    [System.Serializable]
    public class Personality
    {
        public string Name;
        public int Id;
        public int Value;
        [Range(1, 100)] public int MinValue;
        [Range(1, 100)] public int MaxValue;

        public Tuple<int, int> ToTuple()
        {
            return new Tuple<int, int>(MinValue, MaxValue);
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   