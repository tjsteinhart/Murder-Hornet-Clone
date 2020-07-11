using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    [SerializeField] List<TargetController> targetList;
    [SerializeField] int rubiesGainedperLevel;
    [SerializeField] GameObject targetGrid;
    [SerializeField] List<Slider> targetChecks;
    [SerializeField] int targetsLeftIndex;

    public int GetRubiesGainedPerLevel() => rubiesGainedperLevel;

    // Start is called before the first frame update
    void Start()
    {
        rubiesGainedperLevel = 0;
        foreach(Transform child in this.transform)
        {
            if (child.GetComponent<TargetController>())
            {
                targetList.Add(child.GetComponent<TargetController>());
            }
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.onTargetStung += TargetDestroyed;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onTargetStung -= TargetDestroyed;
        }
    }

    public void TargetDestroyed(TargetController target)
    {
        targetList.Remove(target);
        rubiesGainedperLevel += 10;
        if(targetList.Count <= 0)
        {
            StartCoroutine(AllTargetsDestroyed());
        }

        
    }

    IEnumerator AllTargetsDestroyed()
    {
        EventManager.Instance.FinalHit();
        yield return new WaitForSeconds(2);
        EventManager.Instance.EndGamePlay();

    }

}
