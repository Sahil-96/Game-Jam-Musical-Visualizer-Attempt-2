using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

//this script will have all hand placed bars bounce up and down (grow) to beat of the music 
//these bars create the effect of a musical visualizer as they move up and down according to the pitch of the notes being played

public class GrowOnBeat : MonoBehaviour
{
    public Transform target;

    private float currentSize;

    public float growSize, shrinkSize;
    
    [Range(0.8f,0.99f)]
    public float shrinkFactor;

    [Header("Beat Setting")] [Range(0, 3)] public int onFullBeat;

    [Range(0,7)]
    public int[] onBeatD8;

    private int beatCountFull;

    public bool stop;

    public float localcale;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = transform;
        }
        shrinkSize = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if (currentSize > shrinkSize)
            {
                currentSize *= shrinkFactor;
            }
            else
            {
                currentSize = shrinkSize;
            }
        
            CheckBeat();
            target.localScale = new Vector3(target.localScale.x,currentSize,target.localScale.z);
//            target.localScale = new Vector3(target.localScale.x,shrinkSize,target.localScale.z);
//            return;
            
        }
    }

    void Grow()
    {
        currentSize = growSize;
        
    }
    
    //this will input move the bars up and down when told to by the BPeerM script
    void CheckBeat()
    {
        beatCountFull = BPeerM.beatCountFull % 4;
        for (int i = 0; i < onBeatD8.Length; i++)
        {
            if (BPeerM.beatD8 && beatCountFull == onFullBeat && BPeerM.beatCountD8 % 8 == onBeatD8[i])
            {
                Grow();
            }
        }
    }
}
