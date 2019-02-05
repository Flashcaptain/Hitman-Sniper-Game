﻿﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnityEventController
{
    public static bool IsEventReady(int eventId)
    {
        return EventController.IsEventReady(eventId);
    }

    public static bool IsEventAvailable()
    {
        return EventController.IsEventAvailable();
    }

    public static void SpawnAllPlayerEvents()
    {
        EventController.SpawnAllPlayerEvents();
    }

    public List<EventObject> PlayerEventsList
    {
        get { return EventController.PlayerEvents.Values.ToList(); }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           