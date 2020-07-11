using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    [SerializeField] List<TargetController> targetList;
    [SerializeField] int rubiesGainedperLevel;

    [SerializeField] Canvas targetsCanvas;
    [SerializeField] GameObject targetGrid;
    [SerializeField] GameObject targetingImage;
    [SerializeField] GameObject targetCheck;
    [SerializeField] List<Slider> targetChecks;
    [SerializeField] int targetsHitIndex = 0;
    bool targetCanvasObjectsOn = true;
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
                GameObject newTarget = Instantiate(targetCheck, targetGrid.transform.position, Quaternion.identity, targetGrid.transform);
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
        targetCanvasObjectsOn = !targetCanvasObjectsOn;
        targetGrid.SetActive(targetCanvasObjectsOn);
        targetingImage.SetActive(targetCanvasObjectsOn);
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
