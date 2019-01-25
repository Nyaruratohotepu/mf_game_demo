using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoryTextBox : MonoBehaviour,IPointerClickHandler
{
    public StoryPlayer storyPlayer;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        storyPlayer.GoOn();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
