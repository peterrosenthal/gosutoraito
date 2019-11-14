using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightEmitter : MonoBehaviour
{
    public bool debug;

    public int _maxReflectionCount = 5;
    public float _maxStepDistance = 200;
    public bool _drawPrediction;
    public bool _isActive = false;

    public List<GameObject> _activeCrystals;
    public GameObject _activePrism;
    public GameObject _parentLightEmitter;

    private LineRenderer _lineRenderer;
    private List<Vector3> _lineVertices;
    private Ray _ray;
    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineVertices = new List<Vector3>(_maxReflectionCount + 1);
        _activeCrystals = new List<GameObject>
        {
            this.gameObject
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive || _parentLightEmitter)
        {
            DrawLight();
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        }
        if (_parentLightEmitter != null && _parentLightEmitter.GetComponent<LightEmitter>()._activePrism != this.gameObject) 
        {
            DeactivatePrism();
        }
    }

    private void OnDrawGizmos()
    {

        if (_drawPrediction)
        {
            DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, _maxReflectionCount);

        }
    }

    void DrawLight()
    {
        _activeCrystals.Clear();
        _lineVertices.Clear();
        _lineVertices.Add(this.transform.position);
        _ray = new Ray(_lineVertices[0], this.transform.forward);
        if (Physics.Raycast(_ray, out _hit, _maxStepDistance))
        {
            GameObject go = _hit.collider.gameObject;
            string tag = go.tag;

            if (tag == "Mirror" || tag == "Player" || tag == "Pedestal")
            {
                ReflectLineRenderer(_lineVertices[0], this.transform.forward, _maxReflectionCount);

            }
            else
            {
                _lineVertices.Add(_hit.point);
                switch (tag)
                {
                    case "Switch":
                        ActivateCrystal(go);
                        break;
                    case "LightRay":
                        ActivatePrism(go);
                        break;
                }
            }
        }
        else
        {
            _activePrism = null;
            _activeCrystals.Clear();
            _lineVertices.Add(this.transform.position + (this.transform.forward * _maxStepDistance));
        }
        _lineRenderer.positionCount = _lineVertices.Count;
        _lineRenderer.SetPositions(_lineVertices.ToArray());
    }

    void ReflectLineRenderer(Vector3 position, Vector3 direction, int reflectionsLeft) //LineRenderer
    {
        if (reflectionsLeft == 0) return;

       
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxStepDistance))
        {
            GameObject go = hit.collider.gameObject;
            switch (go.tag)
            {
                case "Mirror":
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point;
                    _lineVertices.Add(position);
                    ReflectLineRenderer(position, direction, reflectionsLeft - 1); //Reflect line again
                    break;
                case "Switch":
                    ActivateCrystal(hit.collider.gameObject);
                    position = hit.point;
                    _lineVertices.Add(position);
                    ReflectLineRenderer(hit.point + direction, direction, reflectionsLeft - 1);
                    break;
                case "Player":
                    ReflectLineRenderer(hit.point + direction, direction, reflectionsLeft - 1);
                    break;
                case "Pedestal":
                    ReflectLineRenderer(hit.point + direction, direction, reflectionsLeft - 1);
                    break;
                case "LightRay":
                    position = hit.point;
                    _lineVertices.Add(position);
                    if (!go.GetComponent<LightEmitter>()._isActive)
                    {
                        ActivatePrism(go);
                    }
                    
                    return;
                default:
                    _activePrism = null;
                    position = hit.point;
                    _lineVertices.Add(position);
                    return;
            }

        }
        else
        {
            _activePrism = null;
            position += direction * _maxStepDistance;
            _lineVertices.Add(position);
            return;
        }
        
        
    }

    void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        //Return if no more reflections
        if (reflectionsRemaining == 0) return;

        Vector3 startPos = position;

        //Raycast to detect collider, reflect if mirror
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _maxStepDistance))
        {
            string tag = hit.collider.gameObject.tag;
            if (tag == "Mirror")
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startPos, position);
                DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
            }
            else if (tag=="Pedestal" || tag == "Player")
            {
                position = hit.point;
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startPos, position);
                DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
            }
            else
            {
                position = hit.point;
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startPos, position);
                return;
            }
        }
        else
        {
            position += direction * _maxStepDistance;
        }
    } //Gizmos prediction

    void ActivateCrystal(GameObject go)
    {
        CrystalSwitch crystal;
        crystal = go.GetComponent<CrystalSwitch>();
        crystal.Activate();
        crystal.SetLight(this.gameObject);
        if (!_activeCrystals.Contains(go))
        {
            _activeCrystals.Add(go);
        }

    }

    void ActivatePrism(GameObject go)
    {
        LightEmitter prism;
        prism = go.GetComponent<LightEmitter>();
        prism._isActive = true;
        _activePrism = go;
        prism._parentLightEmitter = this.gameObject;
        if (!_activeCrystals.Contains(go))
        {
            _activeCrystals.Add(go);
        }
    }

    public void DeactivatePrism()
    {
        _isActive = false;
        _parentLightEmitter = null;
        _lineVertices.Clear();
        _lineRenderer.positionCount = _lineVertices.Count;
        _lineRenderer.SetPositions(_lineVertices.ToArray());
    }
}
