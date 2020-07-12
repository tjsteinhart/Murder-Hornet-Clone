using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] float timeSlowFinalHit = .1f;
    [SerializeField] float lengthTimeSlow = 2f;
    // Start is called before the first frame update
    [SerializeField] bool gamePaused;


    public void SubscribeToEvents()
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
        if (!gamePaused)
        {
            Time.timeScale += (1 / lengthTimeSlow) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void FinalTargetSlow(Transform transform)
    {
        Time.timeScale = timeSlowFinalHit;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }

}
