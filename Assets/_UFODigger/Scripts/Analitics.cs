using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Analitics : Singleton<Analitics>
{
    public void SendEvent(string eventName)
    {
        AppMetrica.Instance.ReportEvent(eventName);

        AppMetrica.Instance.SendEventsBuffer();
        print($"Analytics Event: {eventName}");
    }

    public void SendAlienEvent(string alienName, int excavations)
    {
        var parameters = new Dictionary<string, object>();
        parameters["alien"] = alienName;
        parameters["excavations"] = excavations;

        AppMetrica.Instance.ReportEvent(
            "alien_complete",
            parameters
        );

        AppMetrica.Instance.SendEventsBuffer();
        print($"Analytics Event: alien_complete - {alienName} - {excavations}");
    }
}
