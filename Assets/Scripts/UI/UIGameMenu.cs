using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameMenu : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _lifeIconPrefab;

    private List<GameObject> _lives;
    public void DisplayLives(int numberOfLives)
    {
        DestroyAllIcons();
        _lives = new List<GameObject>();

        for (int i = 0; i < numberOfLives; i++)
        {
            var icon = Instantiate(_lifeIconPrefab, _container);
            _lives.Add(icon);
        }
    }

    private void DestroyAllIcons()
    {
        foreach (Transform icon in _container)
        {
            Destroy(icon.gameObject);
        }
    }


}
