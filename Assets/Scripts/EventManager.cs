using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class EventManager : MonoBehaviour
{
    public List<string> events;
    private List<string> freeEvents;
    public bool useTwitchVotes;

    [Space]
    public TextMeshProUGUI Status;
    public TextMeshProUGUI Timer;

    [Space]
    public GameObject[] EventsName;
    public GameObject[] sliders;

    [Space]
    public GameObject newEvent;
    public TextMeshProUGUI newEventName;
    public TextMeshProUGUI newEventInfo;

    [Space]
    [Space]
    public GameObject player;
    public Builder builder;
    public Shooter shooter;

    [Space]
    public GameObject wall;
    public GameObject fatWall;

    private bool? eventStarted = false;
    private float nextEvent = 10;

    private const string Oauth = "oauth:zqmxv1c9gm0lqq6kmoz0gjfpbaiewn";

    private void Update()
    {
        nextEvent -= Time.deltaTime;

        if (nextEvent < 0 && eventStarted == false)
        {
            nextEvent = 18;
            eventStarted = null;
            if (useTwitchVotes)
            {
                RefreshEvent();
                Status.text = "Vote End in:";
            }
        }
        else if (nextEvent < 0 && eventStarted == null)
        {
            if(useTwitchVotes)
                Status.text = "Event End in:";

            print("Event");
            eventStarted = true;

            if (useTwitchVotes)
            {
                int selectedEvent = -1;
                float maxValue = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (maxValue < sliders[i].GetComponent<Slider>().value)
                    {
                        maxValue = sliders[i].GetComponent<Slider>().value;
                        selectedEvent = i;
                    }
                }

                ApplyEvent(EventsName[selectedEvent].GetComponent<TextMeshProUGUI>().text);
            }
            else
            {
                print("Apply");
                ApplyEvent(events[Random.Range(0, events.Count - 1)]);
            }
        }
        else if(nextEvent < 0 && eventStarted == true)
        {
            if(useTwitchVotes)
                Status.text = "Next Vote in:";

            nextEvent = Random.Range(10, 20);
            eventStarted = false;
            EndEvents();
        }

        if(useTwitchVotes)
            Timer.text = ((int)nextEvent).ToString() + " seconds";
    }

    void RefreshEvent()
    {
        if (freeEvents == null || freeEvents.Count < 3)
            freeEvents = events;

        foreach(GameObject title in EventsName)
        {
            string eventName = freeEvents[Random.Range(0, freeEvents.Count - 1)];
            freeEvents.Remove(eventName);
            title.GetComponent<TextMeshProUGUI>().text = eventName;
        }

        foreach(GameObject slider in sliders)
        {
            slider.GetComponent<Slider>().value = .33f;
        }
    }

    private async void ApplyEvent(string eventName)
    {
        print("Applying event");
        newEventName.text = eventName;
        newEventInfo.text = "comming in 3";
        newEvent.SetActive(true);
        await Task.Delay(1000);
        newEventInfo.text = "comming in 2";
        await Task.Delay(1000);
        newEventInfo.text = "comming in 1";
        await Task.Delay(1000);
        newEventInfo.text = "";

        switch (eventName)
        {
            case "FatWalls":
                nextEvent = 15;
                foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x + .8f, 4, wall.transform.localScale.z);

                builder.wallObject = fatWall;
                break;
            case "SlowTime":
                Time.timeScale = .6f;
                break;
            case "FastShoot":
                shooter.twitchModifier = 1.2f;
                break;
        }

        await Task.Delay(1000);
        newEvent.SetActive(false);
    }

    void EndEvents()
    {
        builder.wallObject = wall;
        if(Time.timeScale != 0)
            Time.timeScale = 1;
        shooter.twitchModifier = 1;
    }
}
