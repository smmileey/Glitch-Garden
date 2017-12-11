using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shooter : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject GunPlaceHolder;

    private GameObject _parentObject;
    private Animator _animator;
    private GameObject _laneEnemySpawner;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _parentObject = GameObject.FindGameObjectWithTag(Tags.ProjectileContainer);
        if (_parentObject == null)
        {
            _parentObject = new GameObject(Tags.ProjectileContainer);
            _parentObject.tag = Tags.ProjectileContainer;
        }
        if (GunPlaceHolder == null) throw new UnassignedReferenceException($"Missing the gun placeholder for the {name}");
        SetupLaneEnemySpawner();
    }

    void Update()
    {
        _animator.SetBool(AnimatorControllerFlags.IsAttacking, IsEnemyDetected());
    }

    private void SetupLaneEnemySpawner()
    {
        GameObject[] laneEnemySpawners = GameObject.FindGameObjectsWithTag(Tags.Spawner);
        if (laneEnemySpawners == null || laneEnemySpawners.Length == 0) return;
        _laneEnemySpawner = laneEnemySpawners.First(spawner => spawner.transform.position.y == transform.position.y);
    }

    private bool IsEnemyDetected()
    {
        GameObject coreGameSpace = GameObject.FindGameObjectWithTag(Tags.CoreGameSpace);
        if (coreGameSpace == null) Debug.LogError("Max play space is not defined!");

        foreach (Transform child in _laneEnemySpawner.transform)
        {
            float enemyXPosition = child.transform.position.x;
            bool enemyWithinBounds = enemyXPosition <= coreGameSpace.GetComponent<Collider2D>().bounds.max.x;
            bool enemyAheadOfShooter = child.transform.position.x > transform.position.x;
            if (enemyWithinBounds && enemyAheadOfShooter) return true; ;
        }
        return false;
    }

    private void FireGun()
    {
        GameObject projectile = Instantiate(Projectile, _parentObject.transform);
        projectile.transform.position = GunPlaceHolder.transform.position;
    }
}
