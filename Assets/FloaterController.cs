using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterController : MonoBehaviour
{
    [SerializeField] float timeToTarget;
    [SerializeField] Vector3 myTargetPos;
    [SerializeField] Vector2 timeToSeekTargetMinMax;

    public void InitializeFloater(Vector3 targetPos)
    {
        myTargetPos = targetPos;
        StartCoroutine(StartFloaterMovement());
    }

    IEnumerator StartFloaterMovement()
    {
        var currentPos = transform.position;
        yield return new WaitForSeconds(Random.Range(timeToSeekTargetMinMax.x, timeToSeekTargetMinMax.y));

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToTarget;
            transform.position = Vector3.Lerp(currentPos, myTargetPos, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.SqrMagnitude(myTargetPos - transform.position) < 1f)
        {
            DestroyFloater();
        }

    }

    protected void DestroyFloater()
    {
        Destroy(this.gameObject);
    }



}
