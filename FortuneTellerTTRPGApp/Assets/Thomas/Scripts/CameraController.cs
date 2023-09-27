using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSizeTarget = 3.0f;    
    [SerializeField] private bool drag = false;
    [SerializeField] private bool resettingCameraPos = false;
    
    private Vector3 cameraDefaultPos;
    [SerializeField] private Vector3 cameraOrigin;
    [SerializeField] private Vector3 cameraPosDelta = Vector3.zero;

    private void Awake() {
        cameraDefaultPos = transform.position;
    }

    private void Update() {
        Vector2 scrollDelta = Input.mouseScrollDelta;
        cameraSizeTarget -= scrollDelta.y/3;
        cameraSizeTarget = Mathf.Clamp(cameraSizeTarget, 1, 500);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSizeTarget, 10.0f * Time.deltaTime);

        if (Input.GetKey(KeyCode.R)) {
            resettingCameraPos = true;
        }

        if (Input.GetMouseButton(0)) {
            if (resettingCameraPos) {
                resettingCameraPos = false;
            }
            if (!drag) { 
                drag = true; 
                cameraOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            cameraPosDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cameraTransform.position;
        } else { drag = false; }

        if (drag) {
            cameraTransform.position = cameraOrigin - cameraPosDelta;
        }

        if (resettingCameraPos) {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraDefaultPos, 3.0f * Time.deltaTime);
            if (Vector3.Distance(cameraTransform.position, cameraDefaultPos) < 0.001) { resettingCameraPos = false; }
        }
    }
}
