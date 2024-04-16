using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class DestroyVideoWhenEnds : MonoBehaviour
{

    [SerializeField] private GameObject video;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<GameObject> toDestroy = new List<GameObject>();

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnds;
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnds;
    }

    private void OnVideoEnds(VideoPlayer vp)
    {
        foreach (GameObject go in toDestroy) 
        {
            Destroy(go);
        }
        Destroy(video);
    }

}
