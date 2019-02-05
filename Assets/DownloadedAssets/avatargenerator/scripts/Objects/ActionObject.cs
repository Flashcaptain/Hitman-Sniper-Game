﻿﻿﻿﻿﻿﻿﻿using UnityEngine;

public class ActionObject : MonoBehaviour
{
    public static ActionObject Instantiate(GameAction action)
    {
        var actionObject = (Instantiate(
                Resources.Load("Person0"),
                action.Position,
                Quaternion.identity)
            as GameObject).GetComponent<ActionObject>();
        actionObject.gameObject.layer = 8;
        return actionObject;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 