using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    [SerializeField] List<TargetController> targetList;
    [SerializeField] int rubiesGainedperLevel;

    [SerializeField] Canvas targetsCanvas;
    [SerializeField] Transform targetGrid;
    [SerializeField] GameObject targetCheck;
    [SerializeField] List<Slider> targetChecks;
    [SerializeField] int targetsHitIndex = 0;
    bool targetGridOn = true;
    bool allTargetsHit = false;

    public int GetRubiesGainedPerLevel() => rubiesGainedperLevel;
    public List<TargetController> GetTargetList() => targetList;
    public bool GetAllTargetsHit() => allTargetsHit;

    // Start is called before the first frame update
    void Start()
    {
        rubiesGainedperLevel = 0;
        foreach(Transform child in this.transform)
        {
            if (child.GetComponent<TargetController>())
            {
                targetList.Add(child.GetComponent<TargetController>());
                GameObject newTarget = Instantiate(targetCheck, targetGrid.position, Quaternion.identity, targetGrid);
                targetChecks.Add(newTarget.GetComponent<Slider>());
            }
        }
        ToggleTargetCanvas();
    }

    private void OnEnable()
    {
        EventManager.Instance.onTargetStung += TargetDestroyed;
        EventManager.Instance.onStartGameplay += ToggleTargetCanvas;
        EventManager.Instance.onEndGamePlay += ToggleTargetCanvas;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onTargetStung -= TargetDestroyed;
            EventManager.Instance.onStartGameplay -= ToggleTargetCanvas;
            EventManager.Instance.onEndGamePlay -= ToggleTargetCanvas;

        }
    }

    void ToggleTargetCanvas()
    {
        targetGridOn = !targetGridOn;
        targetGrid.gameObject.SetActive(targetGridOn);
    }

    public void TargetDestroyed(TargetController target)
    {
        targetList.Remove(target);
        targetChecks[targetsHitIndex].fillRect.GetComponent<Image>().fillAmount = 1;
        targetsHitIndex += 1;
        rubiesGainedperLevel += 10;
        if(targetList.Count <= 0)
        {
            allTargetsHit = true;
            EventManager.Instance.FinalHit(target.transform);
        }
    }


}
