using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class NotificationInfo : Singleton<NotificationInfo>
{
    [SerializeField] private TMP_Text info;

    private RectTransform rectTransform;

    protected override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;
    }

    public IEnumerator ShowInfo(Vector3 _position, float _data, Color color = default, string _text = "" )
    {
        transform.position = _position;
        
        bool isDone = false;

        info.color = color;
        info.text = $"{Mathf.FloorToInt(_data)} {_text}";
        
        LeanTween.scale(gameObject, Vector3.one, .5f).setEaseOutElastic().setOnComplete((() =>
        {
            LeanTween.delayedCall(0.5f, () =>
            {
                LeanTween.scale(gameObject, Vector3.zero, .5f).setEaseOutElastic().setOnComplete(() => isDone = true);
            });
        }));

        yield return new WaitUntil(() => isDone);
    }
}
