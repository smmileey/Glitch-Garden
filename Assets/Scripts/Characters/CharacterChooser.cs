using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer), typeof(Transform))]
public class CharacterChooser : MonoBehaviour
{
    private static CharacterChooser[] _characterChoosers;

    public GameObject DefenderPrefab;
    public static GameObject SelectedDefender;

    private static GameObject SelectedDefenderShadow;
    private Text _starCostDisplay;

    void Start()
    {
        _characterChoosers = FindObjectsOfType<CharacterChooser>();
        _starCostDisplay = GetComponentInChildren<Text>();
        if (_starCostDisplay == null) Debug.LogError($"StarCostDisplay text missing in object: {name}.");

        _starCostDisplay.text = DefenderPrefab.GetComponent<Defenders>().SpawnCost.ToString();
        DestroyObject(SelectedDefenderShadow);
    }

    void Update()
    {
        if (SelectedDefender == null) return;
        UpdateSelectedDefenderShadowPosition();
    }

    void OnMouseDown()
    {
        MarkSelectedCharacterAsCurrentSelectedDefender();
        SelectedDefender = DefenderPrefab;
        ReloadSelectedDefenderShadow();
    }

    private void UpdateSelectedDefenderShadowPosition()
    {
        if (SelectedDefenderShadow == null) return;
        var currentMousePositionInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SelectedDefenderShadow.transform.position = new Vector3(currentMousePositionInWorldPoint.x, currentMousePositionInWorldPoint.y, 1.5f);
    }

    private void ReloadSelectedDefenderShadow()
    {
        DestroyObject(SelectedDefenderShadow);
        SelectedDefenderShadow = new GameObject("shadow", new[] { typeof(SpriteRenderer) });
        SetupSpriteRendererAndScaling();
    }

    private void MarkSelectedCharacterAsCurrentSelectedDefender()
    {
        SetCharactersToChooseColorToBlack();
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void SetCharactersToChooseColorToBlack()
    {
        foreach (var characterChooser in _characterChoosers)
        {
            var spriteRenderer = characterChooser.GetComponent<SpriteRenderer>();
            characterChooser.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    private void SetupSpriteRendererAndScaling()
    {
        var shadowSpriteRenderer = SelectedDefenderShadow.GetComponent<SpriteRenderer>();
        shadowSpriteRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        shadowSpriteRenderer.color = new Color(shadowSpriteRenderer.color.r, shadowSpriteRenderer.color.g, shadowSpriteRenderer.color.b, 0.5f);
        SelectedDefenderShadow.GetComponent<Transform>().localScale = GetComponent<Transform>().localScale;
    }
}
