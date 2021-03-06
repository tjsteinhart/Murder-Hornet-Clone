﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    private void OnEnable()
    {
        StartCoroutine(SubscribeEvents());
    }

    IEnumerator SubscribeEvents()
    {
        yield return new WaitForEndOfFrame();
        TimeManager.Instance.SubscribeToEvents();
    }

    #region Gameplay States
    public delegate void StartGamePlay();
    public event StartGamePlay onStartGameplay;
    public void StartGameplay()
    {
        if (onStartGameplay != null)
        {
            onStartGameplay.Invoke();
        }
    }

    public event Action<Transform> onFinalHit;
    public void FinalHit(Transform finalTarget)
    {
        if(onFinalHit != null)
        {
            onFinalHit(finalTarget);
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

    public event Action onCollectibleHit;
    public void CollectibleHit()
    {
        if(onCollectibleHit != null)
        {
            onCollectibleHit();
        }
    }
}
