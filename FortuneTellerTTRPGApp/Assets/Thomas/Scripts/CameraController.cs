using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Public / SerializeField
    [Header("Inputs:")]
    [SerializeField] private bool drag = false;
    [SerializeField] private bool resettingCameraPos = false;
    [SerializeField] private float deltaScroll;
    [Space(10)]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float scrollSpeed = 10.0f;
    [SerializeField] private float dragSpeed = 15.0f;
    [SerializeField] private float cameraResetSpeed = 3.0f;
    [Space(10)]
    #endregion
    #region Private
    private float cameraSizeTarget = 3.0f;
    private float actualCameraMovingSpeed = 0;
    private Vector3 cameraDefaultPos;
    private Vector3 cameraOrigin;
    private Vector3 cameraPosDelta = Vector3.zero;
    private Vector3 cameraPosTarget;
    #endregion

    #region Inputs
    private void HandleInputs() {
        if (Input.GetKey(KeyCode.Space)) {
            resettingCameraPos = true;
        }
        if (Input.GetMouseButton(0)) {
            if (resettingCameraPos) { resettingCameraPos = false; }

            if (!drag) { drag = true; JustStartedToDrag(); }
        }   else       { drag = false; }

        deltaScroll = Input.mouseScrollDelta.y;
    }
    private void JustStartedToDrag() {
        UpdateCameraOrigin();
    }
    #endregion
    #region Movement
    private void HandleMovement() {
        if (resettingCameraPos) {
            cameraPosTarget = cameraDefaultPos;
            actualCameraMovingSpeed = cameraResetSpeed;
            if (Vector3.Distance(cameraTransform.position, cameraDefaultPos) < 0.001) { resettingCameraPos = false; }
        }
        else if (drag) {
            actualCameraMovingSpeed = dragSpeed;
            cameraPosDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cameraTransform.position;
            cameraPosTarget = cameraOrigin - cameraPosDelta;
        }

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPosTarget, actualCameraMovingSpeed * Time.deltaTime);
    }
    private void UpdateCameraOrigin() {
        cameraOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
    #region ScrollZooming
    private void HandleScrollZooming() {
        cameraSizeTarget = Mathf.Clamp(cameraSizeTarget-deltaScroll/3.0f, 1, 500);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSizeTarget, scrollSpeed * Time.deltaTime);
    }
    #endregion

    private void Awake() {
        cameraPosTarget = cameraDefaultPos = transform.position;
    }

    private void Update() {
        HandleInputs();
        HandleMovement();
        HandleScrollZooming();
    }
}
