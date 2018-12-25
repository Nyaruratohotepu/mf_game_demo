using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySectionTrigger : MonoBehaviour
{
    private StoryManager storyManager;
    private Manager gameManager;
    public string StoryName { get; set; }
    private StorySection storySection;
    //准备好后，出现操作提示触发剧情
    public bool isReady { get; set; }
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Manager>();
        storyManager = gameManager.GetStoryManager();
        storySection = storyManager.GetStorySection(StoryName);
        isReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        //触发剧情
        if (isReady && Input.GetButtonDown("Interact"))
            storyManager.StartStory(storySection);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            isReady = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            isReady = false;
    }
}
