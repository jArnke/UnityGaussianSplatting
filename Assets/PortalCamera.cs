using UnityEngine;
using UnityEngine.XR;

public class PortalCamera : MonoBehaviour {

	public Transform player;
	public Transform outPortal;
	public Transform inPortal;
    public MeshRenderer portalRenderer;
	
    public bool isLeft;
    Camera cam;


    bool isSetup = false;

    public void Setup(MeshRenderer portalRender, Transform player, Transform outPortal, Transform inPortal, bool isLeft)
    {

        this.player = player;
        this.portalRenderer = portalRender;
        this.inPortal = inPortal;
        this.outPortal = outPortal;
        this.isLeft = isLeft;


        cam = gameObject.AddComponent<Camera>();
        cam.CopyFrom(Camera.main);
        cam.fieldOfView = Camera.main.fieldOfView;
        cam.aspect = Camera.main.aspect;
        cam.targetTexture = new RenderTexture(XRSettings.eyeTextureDesc);
        if(isLeft){
            cam.projectionMatrix = Camera.main.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
            portalRender.material.SetTexture("_LeftEyeTexture", cam.targetTexture);
        }
        else{
            cam.projectionMatrix = Camera.main.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
            portalRender.material.SetTexture("_RightEyeTexture", cam.targetTexture);
        }
        cam.cullingMask -= PortalSystem.portalLayerMask;
        isSetup = true;
    }

	// Update is called once per frame
	void LateUpdate () {
        if(!isSetup)
            return;

		Vector3 playerOffsetFromPortal = player.position - inPortal.position;
		transform.position = outPortal.position + playerOffsetFromPortal;

		float angularDifferenceBetweenPortalRotations = Quaternion.Angle(inPortal.rotation, outPortal.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * player.forward;
		transform.rotation = Quaternion.LookRotation(newCameraDirection, player.up);
	}
}
