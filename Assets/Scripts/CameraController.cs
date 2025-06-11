using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private Transform targetsParent; // Empty GameObject that contains all targets as children
    private List<Transform> targets = new List<Transform>();

    [SerializeField] private float CamBuffer = 5f;
    [SerializeField] private float MinSize = 6f;
    [SerializeField] private float MaxSize = 20f;
    [SerializeField] private float SmoothTime = 0.2f;

    private Vector3 _velocity;
    private float _zoomSpeed;
    private bool hasMultiplePlayers;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        PopulateTargets();
        hasMultiplePlayers = targets.Count > 1;

        if (targets.Count > 0)
        {
            transform.position = GetAveragePosition();
            _mainCamera.orthographicSize = GetDesiredSize();
        }
    }

    private void LateUpdate()
    {
        // Repopulate the list in case children have changed (optional)
        PopulateTargets();
        hasMultiplePlayers = targets.Count > 1;

        if (targets.Count > 0)
        {
            SetPosition();
            SetSize();
        }
    }

    private void PopulateTargets()
    {
        targets.Clear();

        if (targetsParent == null)
            return;

        foreach (Transform child in targetsParent)
        {
            targets.Add(child);
        }
    }

    private void SetPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetAveragePosition(), ref _velocity, SmoothTime);
    }

    private void SetSize()
    {
        _mainCamera.orthographicSize = Mathf.SmoothDamp(
            _mainCamera.orthographicSize,
            GetDesiredSize(),
            ref _zoomSpeed,
            SmoothTime);
    }

    private Vector3 GetAveragePosition()
    {
        if (targets.Count == 0)
            return transform.position;

        Vector3 averagePosition = Vector3.zero;

        foreach (var target in targets)
        {
            averagePosition += target.position;
        }

        averagePosition /= targets.Count;
        return averagePosition;
    }

    private float GetDesiredSize()
    {
        if (targets.Count <= 1)
            return MinSize;

        float size = 0f;
        Vector3 desiredLocalPos = transform.InverseTransformPoint(GetAveragePosition());

        foreach (var target in targets)
        {
            Vector3 targetLocalPos = transform.InverseTransformPoint(target.position);
            Vector3 delta = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size,
                Mathf.Abs(delta.y),
                Mathf.Abs(delta.x) / _mainCamera.aspect);
        }

        return Mathf.Clamp(size + CamBuffer, MinSize, MaxSize);
    }
}
