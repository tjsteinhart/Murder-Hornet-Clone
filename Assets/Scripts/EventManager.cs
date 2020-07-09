using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    #region Gameplay States
    public event Action onStartGameplay;
    public void StartGameplay()
    {
        if (onStartGameplay != null)
        {
            onStartGameplay();
        }
    }

    public event Action onFinalHit;
    public void FinalHit()
    {
        if(onFinalHit != null)
        {
            onFinalHit();
        }
    }

    public event Action onEndGamePlay;
    public void EndGamePlay()
    {
        if(onEndGamePlay != null)
        {
            onEndGamePlay();
        }
    }
    #endregion

    public event Action<TargetController> onTargetStung;
    public void TargetStung(TargetController target)
    {
        if(onTargetStung != null)
        {
            onTargetStung(target);
        }
    }
}
