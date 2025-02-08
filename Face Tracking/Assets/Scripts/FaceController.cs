using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARFaceManager))]   
public class FaceController : MonoBehaviour
{

    [Header("Face Tracking Button")] [SerializeField]
    private Button faceTrackingToggle;
    [Header("Swap Filter Button")] [SerializeField]
    private Button swapFaceToggle;

    [Header("References, bools and variables")]
    private ARFaceManager faceManager;
    private bool faceTrackingOn = true;
    private int swapCounter = 0;

    [SerializeField]
    public FaceMaterial[] materials;

    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();

        faceTrackingToggle.onClick.AddListener(ToggleTrackingFaces);
        swapFaceToggle.onClick.AddListener(SwapFaces);

        faceManager.facePrefab.GetComponent<MeshRenderer>().material = materials[0].Material;
    }

    void SwapFaces()
    {
        //functionality to swap the materials
        swapCounter = swapCounter == materials.Length - 1 ? 0 : swapCounter + 1;

        foreach (ARFace face in faceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = materials[swapCounter].Material;
        }

        swapFaceToggle.GetComponentInChildren<Text>().text = $"Face Material ({materials[swapCounter].Name})";
    }

    void ToggleTrackingFaces()
    {
        faceTrackingOn = !faceTrackingOn;

        foreach (ARFace face in faceManager.trackables)
        {
            face.enabled = faceTrackingOn;
        }

        faceTrackingToggle.GetComponentInChildren<Text>().text = $"Face Tracking {(faceManager.enabled ? "Off" : "On")}";
    }

}

[System.Serializable]
public class FaceMaterial
{
    [Header("Filter")]
    public Material Material;

    [Header("Name of Filter")]
    public string Name;
}