using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{

    private GameObject _defendersContainer;
    private StarDisplay _starDislay;

    void Start()
    {
        _starDislay = FindObjectOfType<StarDisplay>();
        _defendersContainer = GameObject.FindGameObjectWithTag(Tags.DefendersContainter);
        if (_defendersContainer == null)
        {
            _defendersContainer = new GameObject(Tags.DefendersContainter);
            _defendersContainer.tag = Tags.DefendersContainter;
        }
    }

    void OnMouseDown()
    {
        if (CharacterChooser.SelectedDefender == null) return;

        Vector2 mousePositionInWorldUnit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_starDislay.UseStars(CharacterChooser.SelectedDefender.GetComponent<Defenders>().SpawnCost) == StarTransactionStatus.SUCCESS)
        {
            GameObject spawnedDefender = Instantiate(CharacterChooser.SelectedDefender, _defendersContainer.transform);
            spawnedDefender.transform.position = SnapToGrid(mousePositionInWorldUnit);
        }
        else
        {
            Debug.Log($"Insufficient stars");
        }
    }

    private Vector2 SnapToGrid(Vector2 mousePositionInWorldUnit)
    {
        return new Vector2(Mathf.RoundToInt(mousePositionInWorldUnit.x), Mathf.RoundToInt(mousePositionInWorldUnit.y));
    }
}
