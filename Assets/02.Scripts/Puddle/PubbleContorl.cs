using System.Collections;
using UnityEngine;

public enum PubbleState
{
    gatling,
    bomb,
    water,
}

public class PubbleContorl : MonoBehaviour
{
    public PubbleState state;
    public int maxBubble = 100 ;
    public int curBubble;
    public int addBubble = 1;

    private void Start()
    {
        curBubble = maxBubble;
        StartCoroutine(bubbleAdd());
    }
    private void Update()
    {
        float newSize = curBubble / (float)maxBubble;
        transform.localScale = new Vector3(newSize, newSize, 1);
    }

    IEnumerator bubbleAdd()
    {
        if(curBubble< maxBubble)
           curBubble += addBubble;
        yield return new WaitForSeconds(1);
        StartCoroutine(bubbleAdd());
    }
}
