using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySectionTrigger : MonoBehaviour
{
    private StoryManager storyManager;
    private Manager gameManager;
    public string StoryName;
    private StorySection storySection;
    //准备好后，出现操作提示触发剧情
    public bool isReady { get; set; }
    public string LeftName = "Player";
    public string RightName = "Other";
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Manager>();
        storyManager = gameManager.GetStoryManager();

        storySection = storyManager.GetStorySection(StoryName, LeftName, RightName);
        isReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        //触发剧情
        if (isReady && Input.GetButtonDown("Interact"))
        {
            MonoBehaviour.print("对话!" + StoryName + storySection);
            storyManager.StartStory(storySection);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isReady = true;
        MonoBehaviour.print("colli");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isReady = false;
    }

}
