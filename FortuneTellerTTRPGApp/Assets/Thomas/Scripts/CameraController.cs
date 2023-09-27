using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSizeTarget = 3.0f;    
    [SerializeField] private bool drag = false;
    [SerializeField] private bool resettingCameraPos = false;
    [SerializeField] private float scrollSpeed = 10.0f;
    [SerializeField] private float dragSpeed = 15.0f;
    [SerializeField] private float cameraResetSpeed = 3.0f;

    private float actualCameraMovingSpeed = 0;
    
    private Vector3 cameraDefaultPos;
    private Vector3 cameraOrigin;
    private Vector3 cameraPosDelta = Vector3.zero;
    private Vector3 cameraPosTarget;

    private void Awake() {
        cameraDefaultPos = transform.position;
        cameraPosTarget = cameraDefaultPos;
    }

    private void Update() {
        cameraSizeTarget -= Input.mouseScrollDelta.y/3;
        cameraSizeTarget = Mathf.Clamp(cameraSizeTarget, 1, 500);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSizeTarget, scrollSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space)) {
            resettingCameraPos = true;
            cameraPosTarget = cameraDefaultPos;
            actualCameraMovingSpeed = cameraResetSpeed;
        }

        if (Input.GetMouseButton(0)) {
            if (resettingCameraPos) {
                resettingCameraPos = false;
            }
            if (!drag) { 
                drag = true; 
                actualCameraMovingSpeed = dragSpeed;
                cameraOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            cameraPosDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cameraTransform.position;
        } else { drag = false; }

        if (drag) {
            cameraPosTarget = cameraOrigin - cameraPosDelta;
        }
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPosTarget, actualCameraMovingSpeed * Time.deltaTime);

        if (resettingCameraPos) {
            if (Vector3.Distance(cameraTransform.position, cameraDefaultPos) < 0.001) { resettingCameraPos = false; }
        }
    }
}
