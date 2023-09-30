using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    #region Public / SerializeField
    [SerializeField] private LineRenderer lineRenderer;
    public Transform lineRendererPos1;
    public Transform lineRendererPos2;
    #endregion

    #region LineRendering
    private void HandleLineRender() {
        lineRenderer.SetPosition(0, lineRendererPos1.position);
        lineRenderer.SetPosition(1, lineRendererPos2.position);
    }
    #endregion

    private void Awake() {
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        HandleLineRender();
    }
}
