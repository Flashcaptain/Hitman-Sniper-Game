﻿﻿﻿﻿﻿using System.Collections.Generic;
using UnityEngine;

public class UnityAction : MonoBehaviour
{
    public static Dictionary<int, ActionObject> Actions = new Dictionary<int, ActionObject>();

    public static void SpawnActions()
    {
        var actionsDict = ActionsParser.NormalActions;

        foreach (var action in actionsDict)
        {
            Actions.Add(action.Key, ActionObject.Instantiate(action.Value));
        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           