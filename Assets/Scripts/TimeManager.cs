using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float timeSlowFinalHit = .1f;
    [SerializeField] float lengthTimeSlow = 2f;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.Instance.onFinalHit += FinalTargetSlow;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onFinalHit -= FinalTargetSlow;
        }
    }

    private void Update()
    {
        Time.timeScale += (1 / lengthTimeSlow) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void FinalTargetSlow(Transform transform)
    {
        Time.timeScale = timeSlowFinalHit;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

}
