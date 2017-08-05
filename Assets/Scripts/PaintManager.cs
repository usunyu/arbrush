using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public enum PaintTemplateType
{
    CUBE,   // Object
    BUBBLE, // Object
    STAR,   // Object
    DOLLAR, // Particle
}

public class PaintManager : MonoBehaviour
{
    /// <summary>
    /// The star template.
    /// </summary>
    public GameObject starTemplate;
    /// <summary>
    /// The cube template.
    /// </summary>
    public GameObject cubeTemplate;
    /// <summary>
    /// The sphere template.
    /// </summary>
    public GameObject sphereTemplate;
    /// <summary>
    /// The dollor paint template.
    /// </summary>
    public ParticleSystem dollorTemplate;
    /// <summary>
    /// If we have new vertices need paint.
    /// </summary>
    private bool newPaintVertices;
    /// <summary>
    /// The painting toggle is on.
    /// </summary>
    private bool paintingOn;
    /// <summary>
    /// The previous paint object position.
    /// </summary>
    private Vector3 previousPosition;
    /// <summary>
    /// The paint game object list.
    /// </summary>
    private List<GameObject> gameObjectList;
    /// <summary>
    /// Stores all particle systems.
    /// </summary>
    private List<ParticleSystem> particleSystemList;
    /// <summary>
    /// Stores current camera positions to paint.
    /// </summary>
    private List<Vector3> currVertices;
    /// <summary>
    /// Stores current particle system.
    /// </summary>
    private ParticleSystem currentParticle;
    /// <summary>
    /// The type of the current paint.
    /// </summary>
    public PaintTemplateType currentPaintType;
    /// <summary>
    /// The camera item distance.
    /// </summary>
    private const float CAMERA_ITEM_DISTANCE = 0.5f;
    /// <summary>
    /// The paint item distance.
    /// </summary>
    private const float PAINT_ITEM_DISTANCE = 0.1f;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (starTemplate == null)
        {
            throw new System.Exception("Star template not attached!");
        }
        if (cubeTemplate == null)
        {
            throw new System.Exception("Cube template not attached!");
        }
        if (sphereTemplate == null)
        {
            throw new System.Exception("Sphere template not attached!");
        }
        if (dollorTemplate == null)
        {
            throw new System.Exception("Dollar template not attached!");
        }
    }
    /// <summary>
    /// Ons the enable.
    /// </summary>
    private void OnEnable()
    {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
    }
    /// <summary>
    /// Ons the destroy.
    /// </summary>
    private void OnDestroy()
    {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent -= ARFrameUpdated;
    }
    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start()
    {
        currentPaintType = PaintTemplateType.CUBE;
        paintingOn = false;
        newPaintVertices = false;
        gameObjectList = new List<GameObject>();
        particleSystemList = new List<ParticleSystem>();
        currentParticle = Instantiate(dollorTemplate);
        currVertices = new List<Vector3>();
        //paintColor = Color.green;
    }
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (currentPaintType != PaintTemplateType.DOLLAR ||
            !paintingOn ||
            !newPaintVertices ||
            currVertices.Count == 0)
        {
            return;
        }
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[currVertices.Count];
        int index = 0;
        foreach (Vector3 vtx in currVertices)
        {
            particles[index].position = vtx;
            //particles[index].color = paintColor;
            particles[index].startSize = 0.1f;
            index++;
        }
        currentParticle.SetParticles(particles, currVertices.Count);
        newPaintVertices = false;
    }
    /// <summary>
    /// Toggles the paint.
    /// </summary>
    public void TogglePaint()
    {
        paintingOn = !paintingOn;
    }
    /// <summary>
    /// Randomizes the color.
    /// </summary>
    public void RandomizeColor()
    {
        if (currentParticle.particleCount > 0)
        {
            SaveParticleSystem();
        }
        //paintColor = Random.ColorHSV();
    }
    /// <summary>
    /// Reset the paint.
    /// </summary>
    public void Reset()
    {
        foreach (GameObject g in gameObjectList)
        {
            Destroy(g);
        }
        gameObjectList = new List<GameObject>();

        foreach (ParticleSystem p in particleSystemList)
        {
            Destroy(p);
        }
        particleSystemList = new List<ParticleSystem>();

        Destroy(currentParticle);
        currentParticle = Instantiate(dollorTemplate);
        currVertices = new List<Vector3>();
    }
    /// <summary>
    /// Saves the particle system.
    /// </summary>
    private void SaveParticleSystem()
    {
        particleSystemList.Add(currentParticle);
        currentParticle = Instantiate(dollorTemplate);
        currVertices = new List<Vector3>();
    }
    /// <summary>
    /// Called when ARFrame updated.
    /// </summary>
    /// <param name="cam">Cam.</param>
    private void ARFrameUpdated(UnityARCamera cam)
    {
        Vector3 paintPosition = GetCameraPosition(cam) + (Camera.main.transform.forward * CAMERA_ITEM_DISTANCE);
        if (Vector3.Distance(paintPosition, previousPosition) > PAINT_ITEM_DISTANCE)
        {
            if (paintingOn)
            {
                if (currentPaintType == PaintTemplateType.DOLLAR)
                {
                    currVertices.Add(paintPosition);
                    newPaintVertices = true;
                }
                else if (currentPaintType == PaintTemplateType.STAR)
                {
                    GameObject go = Instantiate(starTemplate, paintPosition, transform.rotation);
                    gameObjectList.Add(go);
                }
                else if (currentPaintType == PaintTemplateType.BUBBLE)
                {
                    GameObject go = Instantiate(sphereTemplate, paintPosition, transform.rotation);
                    gameObjectList.Add(go);
                }
                else
                {
                    GameObject go = Instantiate(cubeTemplate, paintPosition, transform.rotation);
                    gameObjectList.Add(go);
                }
            }
            previousPosition = paintPosition;
        }
    }
    /// <summary>
    /// Gets the camera position.
    /// </summary>
    /// <returns>The camera position.</returns>
    /// <param name="cam">Cam.</param>
    private Vector3 GetCameraPosition(UnityARCamera cam)
    {
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(3, cam.worldTransform.column3);
        return UnityARMatrixOps.GetPosition(matrix);
    }
}
