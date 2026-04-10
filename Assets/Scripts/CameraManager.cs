using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target, cam, objPlayer, objGlobal;

    private Camera camGlobal, camMy;
    private Camera camComponent;
    private float targetSize;
    private float playerCamSize = 3f;
    private float globalCamSize = 10f;

    void Start()
    {
        camComponent = cam.GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OnClickMyCam();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            OnClickGlobalCam();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);
            cam.position = Vector3.Lerp(cam.position, targetPosition, 5 * Time.deltaTime);

            camComponent.orthographicSize = Mathf.Lerp(camComponent.orthographicSize, targetSize, 3 * Time.deltaTime);
        }
    }
    public void OnClickMyCam()
    {
        if (objPlayer != null)
        {
            target = objPlayer;
            targetSize = playerCamSize;
        }
    }

    public void OnClickGlobalCam()
    {
        target = objGlobal;
        targetSize = globalCamSize;
    }
}
