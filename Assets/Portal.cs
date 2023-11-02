using UnityEngine;

public class Portal : MonoBehaviour
{
    

    [SerializeField] Portal outPortal;
    
    MeshRenderer _portalRenderer;

    //Portal holds reference to cameras outside the other portal
    PortalCamera leftCamera;
    PortalCamera rightCamera;
    
    //Delay Setting up cameras until VR Cam is initialized
    bool haveSetupCameras = false;
    float timer = 0;

    //Portal Will only render when inLineOfSight;
    public bool activatePortalCollider;

    void Awake(){

        activatePortalCollider = true;
        
    }

    void Start(){
        //Safety Checks
        if(outPortal == null){
            Debug.LogError("Please select an output portal");
        }

        this._portalRenderer = gameObject.GetComponent<MeshRenderer>();
        if(_portalRenderer == null){
            Debug.LogError("Please attach a plane and mesh renderer to portal script");
        }
        _portalRenderer.material = new Material(PortalSystem.portalShader);

        //Setup Portal Enabled Collider:
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        GameObject go = new GameObject();
        go.name = "PortalOtherSideRender";
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.Euler(0,0,180);
        go.AddComponent<MeshFilter>().mesh = this.GetComponent<MeshFilter>().mesh;
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.material = _portalRenderer.material;
        go.layer = gameObject.layer;


    }


    // Update is called once per frame
    void Update()
    {
        if(!haveSetupCameras)
            if(timer > 1){
                SetupPortalCameras();
            }
            timer += Time.deltaTime;
            return;

    }

    void SetupPortalCameras(){
        leftCamera = (new GameObject()).AddComponent<PortalCamera>();
        leftCamera.gameObject.name = "Left Portal Cam";
        leftCamera.transform.parent = outPortal.transform;
        rightCamera = (new GameObject()).AddComponent<PortalCamera>();
        rightCamera.gameObject.name = "Right Portal Cam";
        rightCamera.transform.parent = outPortal.transform;

        leftCamera.Setup(_portalRenderer, PortalSystem.leftEye, outPortal.transform, this.transform,  true);
        
        rightCamera.Setup(_portalRenderer, PortalSystem.rightEye, outPortal.transform, this.transform, false);

        haveSetupCameras = true;
    }

    private void OnTriggerEnter(Collider other){
        if(leftCamera == null)
            return;

        if(!activatePortalCollider)
            return;
        
        outPortal.activatePortalCollider = false;

        Debug.Log("Portal Entered");

        PortalSystem.XROrigin.transform.position = (PortalSystem.XROrigin.transform.position - other.transform.position) + (leftCamera.transform.position + rightCamera.transform.position)/2f;
        PortalSystem.XROrigin.transform.position += outPortal.transform.position - other.transform.position;

    }

    private void OnTriggerExit(Collider _){
        activatePortalCollider = true;
    }
}
