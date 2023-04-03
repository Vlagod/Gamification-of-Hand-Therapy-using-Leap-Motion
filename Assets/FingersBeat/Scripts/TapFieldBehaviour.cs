using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;
using Image = UnityEngine.UIElements.Image;

public class TapFieldBehaviour : MonoBehaviour
{

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private UnityEngine.UI.Image image;
    private const int SECTION_SIZE = 100;

    private float speed = 25;

    private Controller leapController;

    private int targetFingerIndex;
    
    public void Init(int size, int fingerIndex)
    {
        var sizeDelta = rectTransform.sizeDelta;
        sizeDelta.y *= size;
        rectTransform.sizeDelta = sizeDelta;
        this.targetFingerIndex = fingerIndex;
    }
    // Start is called before the first frame update
    void Start()
    {
        leapController = FindObjectOfType<LeapServiceProvider>().GetLeapController();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.y -= speed * Time.deltaTime;
        transform.position = position;


        DetectTargetFingerBend();
    }

    private bool fingerLowered = false;
    private float loweredThreshold = 0.05f;
    void DetectTargetFingerBend()
    {
        Frame frame = leapController.Frame();
        if (frame.Hands.Count < 1)
        {
            return;
        }
        Hand hand = frame.Hands[0];
        Finger indexFinger = hand.Fingers[targetFingerIndex];
        if (targetFingerIndex == 1)
        {
            Debug.Log("tip: " + indexFinger.TipPosition.y);
            Debug.Log("hand: " + hand.PalmPosition.y);
        }

        if (indexFinger.TipPosition.y < hand.PalmPosition.y - loweredThreshold)
        {
            if (!fingerLowered)
            {
                fingerLowered = true;
                image.color = Color.red;
                
                // The index finger has been lowered down
                Debug.Log("Index finger lowered down on " + gameObject.name);
            }
        }
        else
        {
            fingerLowered = false;
            image.color = Color.white; 
        }
    }

}
