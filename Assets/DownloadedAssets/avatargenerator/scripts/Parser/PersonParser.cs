﻿﻿﻿﻿﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Casanova.Prelude;

public class PersonParser : MonoBehaviour
{
    private const string PERSONS = "players";

    public static List<Tuple<string, List<Tuple<int, int>>>> ParsedPersons =
        new List<Tuple<string, List<Tuple<int, int>>>>();

    public static int TotalParsedPersons
    {
        get { return ParsedPersons.Count; }
    }

    public static void ParsePersons()
    {
        var personsxml = XmlNodule.Load(PERSONS);
        foreach (var person in personsxml)
        {
            var personalities = person.Get("personalities");
            var personalityList =
                personalities.Select(
                        personality =>
                                new Tuple<int, int>(personality.Get("id").ToInt(), personality.Get("value").ToInt()))
                    .ToList();

            ParsedPersons.Add(new Tuple<string, List<Tuple<int, int>>>(person.Get("prefabname").ToString(),
                personalityList));
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           