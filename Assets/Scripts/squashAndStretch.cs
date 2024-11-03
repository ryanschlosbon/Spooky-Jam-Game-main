using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squashAndStretch : MonoBehaviour
{
    [Header("Squash and Stretch Core")]
    private Transform affectedObject;
    [SerializeField] private SquashStretchAxis axisToAffect = SquashStretchAxis.Y;
    public float animationDuration = 0.25f;
    [SerializeField] private bool canBeOverwritten;
    [SerializeField] private bool playOnStart;

    //[Flags]

    public enum SquashStretchAxis
    {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4
    }

    [Header("Animation Settings")]
    public float initialScale = 1f;
    public float maxScale = 1.3f;
    public bool resetToInitialScale = true; 

    public AnimationCurve squashAndStretchCurve = new AnimationCurve(new Keyframe(0f, 0f),
    new Keyframe(0.25f, 1f),
    new Keyframe(1f, 0f)
    );

    [Header("Looping sheit")]
    private bool looping;
    public float loopingDelay = 0.5f;
    Coroutine squashAndStretchCoroutine;
    WaitForSeconds loopingDelayTimer;
    Vector3 initialScaleVector;

    bool affectX => (axisToAffect & SquashStretchAxis.X) != 0;
    bool affectY => (axisToAffect & SquashStretchAxis.Y) != 0;
    bool affectZ => (axisToAffect & SquashStretchAxis.Z) != 0;

    private void Awake()
    {
        if (affectedObject == null)
        {
            affectedObject = transform;
        }

        initialScaleVector = affectedObject.localScale;
        loopingDelayTimer = new WaitForSeconds(loopingDelay);

    }

    private void Start()
    {
        if(playOnStart)
        {
            CheckForAndStartCoroutine();
        }
    }

    private void CheckForAndStartCoroutine()
    {
        if (axisToAffect == SquashStretchAxis.None)
        {
            Debug.Log("Bruh moment");
            return;
        }

        if (squashAndStretchCoroutine != null)
        {
            StopCoroutine(squashAndStretchCoroutine);
            //if ()
            transform.localScale = initialScaleVector;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
