using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ScriptsUI
{
    public class StartGameButton : MonoBehaviour, IPointerClickHandler
    {
        //Detect if a click occurs
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            //Use this to tell when the user left-clicks on the Button
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
        }
    }
}