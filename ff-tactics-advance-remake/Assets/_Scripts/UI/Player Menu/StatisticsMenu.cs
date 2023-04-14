using System;
using UnityEngine;

public class StatisticsMenu : PlayerMenu
{
    private void OnEnable()
    {
        EventManager.OnCharacterHovered += OnCharacterHovered;
        EventManager.OnCharacterUnhovered += OnCharacterUnhovered;
    }

    private void OnCharacterUnhovered(Character _item)
    {
        Toggle(false, -1);
    }

    private void OnCharacterHovered(Character _item)
    {
        Toggle(true);
    }
}
