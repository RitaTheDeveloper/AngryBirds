using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SlingshotHandler : MonoBehaviour
{
    [Header("Line Renderers:")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transforms:")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;

    [Header("Slingshot stats:")]
    [SerializeField] private float _maxDistanceOfLine = 4f;
    [SerializeField] private float _launchForce = 6f;
    [SerializeField] private float _timeBetweenShellRespawns = 2f;
    [SerializeField] private LayerMask _layerSlingshotArea;

    [Header("Shell:")]
    [SerializeField] private Shell _shellPrefab;

    private Vector2 _currentLinesPosition;
    private Vector3 _mousePosition;
    private bool _clickedWithinArea;
    Vector2 _direction;
    Vector2 _directionNormalized;
    private float _shellPosOffset = 1f;

    private Shell _spawnedShell;
    private bool _shellOnSlingshot = false;

    private GameManager _gameManager;
    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;

        SpawnShell();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);
        if (InputManager.WasLeftMouseButtonPressed && AreaCheck.IsWithinThisArea(_mousePosition, _layerSlingshotArea))
        {
            _clickedWithinArea = true;
        }

        if (InputManager.IsLeftMousePressed && _clickedWithinArea && _shellOnSlingshot)
        {
            DrawSlingshot();
            PositionAndRotationShell();
        }

        if (InputManager.WasLeftMouseButtonReleased && _shellOnSlingshot && _clickedWithinArea)
        {
            if (_gameManager.HasAvailableShots())
            {
                _clickedWithinArea = false;
                _spawnedShell.LaunchShell(_direction, _launchForce);

                _gameManager.UseShot();
                //Time.timeScale = 0.1f;
                _shellOnSlingshot = false;

                SetLines(_centerPosition.position);

                if (_gameManager.HasAvailableShots())
                    StartCoroutine(SpawnShellAfterTime());
            }
        }
    }

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void DrawSlingshot()
    {
        _currentLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(_mousePosition - _centerPosition.position, _maxDistanceOfLine);
        
        SetLines(_currentLinesPosition);

        _direction = (Vector2)_centerPosition.position - _currentLinesPosition;
        _directionNormalized = ((Vector2)_centerPosition.position - _currentLinesPosition).normalized;
    }

    private void SetLines(Vector2 position)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }

        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);

    }

    private void SpawnShell()
    {
        SetLines(_idlePosition.position);

        Vector2 dir = (_centerPosition.position - _idlePosition.position).normalized;
        Vector2 spawnPos = (Vector2)_idlePosition.position + dir * _shellPosOffset;
        _spawnedShell =  Instantiate(_shellPrefab, spawnPos, Quaternion.identity);
        _spawnedShell.gameObject.transform.right = dir;

        _shellOnSlingshot = true;
    }

    private IEnumerator SpawnShellAfterTime()
    {
        yield return new WaitForSeconds(_timeBetweenShellRespawns);
        SpawnShell();
    }

    private void PositionAndRotationShell()
    {
        _spawnedShell.gameObject.transform.position = _currentLinesPosition + _directionNormalized * _shellPosOffset;
        _spawnedShell.gameObject.transform.right = _directionNormalized;
    }
}
