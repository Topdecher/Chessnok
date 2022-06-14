using Assets.Scripts.BoardEnvironment;
using Assets.Scripts.EventSystem;
using Assets.Scripts.States.GameStates;
using Assets.Scripts.PlayerEnvironment;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private GameManager manager;
        private EventControl eventControl;

        private Graveyard graveyard;
        private Deck deck;
        private Hand hand;
        
        private Board board;

        private GameState currentState;


    }
}