﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DamageInfo : Singleton<DamageInfo>
{
    [SerializeField] private TMP_Text info;

    private RectTransform rectTransform;

    protected override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;
    }

    public IEnumerator ShowInfo(Vector3 _position, float _data)
    {
        transform.position = _position;
        
        bool isDone = false;
        
        info.text = $"{Mathf.FloorToInt(_data)} HP";
        
        LeanTween.scale(gameObject, Vector3.one, .5f).setEaseSpring().setOnComplete((() =>
        {
            LeanTween.delayedCall(1f, () =>
            {
                LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseSpring().setOnComplete(() => isDone = true);
            });
        }));

        yield return new WaitUntil(() => isDone);
    }
}