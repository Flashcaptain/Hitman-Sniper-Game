﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
using Casanova.Prelude;
using UnityEngine;

public static class HelperFunctions
{
    public static List<Tuple<T, K>> ToTupleList<T, K>(this Dictionary<T, K> dictionary)
    {
        return dictionary.Select(keyValue => new Tuple<T, K>(keyValue.Key, keyValue.Value)).ToList();
    }

    public static GenericVector ToGenericVector(this Dictionary<int, int> dictionary)
    {
        var newGenericVector = new GenericVector();
        foreach (var keyValue in dictionary)
        {
            newGenericVector.Add(keyValue.Value);
        }
        return newGenericVector;
    }

    public static Vector3 GenerateRandomVector()
    {
        return new Vector3(
            Screen.width / 2.0f + Random.Range(-20.0f, 20.0f),
            0.0f,
            Screen.height / 2.0f + Random.Range(-20.0f, 20.0f)
        );
    }

    //casanova functions
    public static int indexOf(this List<float> list, float value)
    {
        return list.Contains(value) ? list.IndexOf(value) : -1;
    }

    public static void Log(object message)
    {
        //Debug.Log(message);
    }

    public static void LogGUI(object message)
    {
        DebugConsole.Log(message.ToString());
    }

    public static List<int> removeInt(int value, List<int> list)
    {
        list.Remove(value);
        return list;
    }

    //end casanova functions

    public static GenericVector SetUpGenericVector(this List<Tuple<int, int>> personalityvalues)
    {
        return personalityvalues
            .AddCounterFactors()
            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2)
            .ToGenericVector();
    }

    public static void SetPositionTo(this GameAction action, GameObject gameObject)
    {
        action.Position = gameObject.transform.position;
    }

    private static List<Tuple<int, int>> AddCounterFactors(this List<Tuple<int, int>> singlePersonalities)
    {
        //this to prevent infinite loop. since we adding to singlepersonalities
        var sizeSinglePersonalities = singlePersonalities.Count;
        for (var i = 0; i < sizeSinglePersonalities; i++)
        {
            var inversePersonality =
                new Tuple<int, int>(singlePersonalities[i].Item1 + sizeSinglePersonalities,
                    100 - singlePersonalities[i].Item2);
            singlePersonalities.Add(inversePersonality);
        }
        return singlePersonalities;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   