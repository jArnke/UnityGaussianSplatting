using System.Collections.Generic;
using UnityEngine;

public class PortalSystemManager : MonoBehaviour
{
    [SerializeField] Shader PortalShader;
    [SerializeField] Transform leftEye;
    [SerializeField] Transform rightEye;
    [SerializeField] GameObject XROrigin;
    [SerializeField] LayerMask portalLayerMask;

    void Awake(){
        Debug.Assert(PortalShader != null);
        PortalSystem.portalShader = PortalShader;
        Debug.Assert(leftEye != null);
        PortalSystem.leftEye = leftEye;
        Debug.Assert(rightEye != null);
        PortalSystem.rightEye = rightEye;
        Debug.Assert(XROrigin != null);
        PortalSystem.player = XROrigin;
        PortalSystem.XROrigin = XROrigin;
        PortalSystem.portalLayerMask = portalLayerMask;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class PortalSystem{
    public static Shader portalShader;
    public static Transform leftEye;
    public static Transform rightEye;
    public static GameObject player;
    public static GameObject XROrigin;

    public static LayerMask portalLayerMask;
    public static List<Portal> portals;
}
