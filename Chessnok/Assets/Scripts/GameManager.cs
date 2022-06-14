using Assets.Scripts.EventSystem;
using Assets.Scripts.PlayerEnvironment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Deck deck;
    public Hand hand;
    public EventControl eventControl;
    void Awake()
    {
        eventControl = Instantiate(new GameObject()).AddComponent<EventControl>();
        deck.SetEventControl(eventControl);
        hand.SetEventControl(eventControl);
    }

    void Update()
    {
        
    }
}
