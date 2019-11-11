using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightEmitter : MonoBehaviour
{
    public GameObject _activeCrystals;
    public GameObject _hittingLightEmitter;

    public int _maxReflectionCount = 5;
    public float _maxStepDistance = 200;
    public bool _drawPrediction;
    public bool _isActive;

    private LineRenderer _lineRenderer;
    private List<Vector3> _lineVertices;
    private Ray _ray;
    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineVertices = new List<Vector3>(_maxReflectionCount + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            DrawLight();
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
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
        _lineVertices.Clear();
        _lineVertices.Add(this.transform.position);
        _ray = new Ray(_lineVertices[0], this.transform.forward);
        if (Physics.Raycast(_ray, out _hit, _maxStepDistance))
        {
            string tag = _hit.collider.gameObject.tag;

            if (tag == "Mirror" || tag == "Player" || tag == "Pedestal")
            {
                ReflectLineRenderer(_lineVertices[0], this.transform.forward, _maxReflectionCount);

            }
            else
            {
                _lineVertices.Add(_hit.point);
                if (tag == "Switch")
                {
                    ActivateCrystal(_hit.collider.gameObject);
                }
                else
                {
                    _activeCrystals = null;
                }
            }
        }
        else
        {
            _activeCrystals = null;
            _lineVertices.Add(this.transform.position + (this.transform.forward * _maxStepDistance));
        }
        _lineRenderer.positionCount = _lineVertices.Count;
        _lineRenderer.SetPositions(_lineVertices.ToArray());
    }

    void ActivateCrystal(GameObject go)
    {
        CrystalSwitch crystal;
        crystal = go.GetComponent<CrystalSwitch>();
        crystal.Activate();
        crystal.SetLight(this.gameObject);
        if (_activeCrystals == null)
        {
            _activeCrystals = go;
        }
    }

    void ActivatePrism(GameObject go)
    {
        LightEmitter prism;
        prism = go.GetComponent<LightEmitter>();
        prism.Activate();
        prism.SetLightEmitter(this.gameObject);
        if (_hittingLightEmitter == null)
        {
            _hittingLightEmitter = go;
        }
    }

    void ReflectLineRenderer(Vector3 position, Vector3 direction, int reflectionsLeft)
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
                    _activeCrystals = null;
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point;
                    _lineVertices.Add(position);
                    ReflectLineRenderer(position, direction, reflectionsLeft - 1); //Reflect line again
                    break;
                case "Switch":
                    ActivateCrystal(hit.collider.gameObject);
                    position = hit.point;
                    _lineVertices.Add(position);
                    return;
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
                    _activeCrystals = null;
                    position = hit.point;
                    _lineVertices.Add(position);
                    return;
            }

        }
        else
        {
            _activeCrystals = null;
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
            if (hit.collider.gameObject.tag == "Mirror")
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
            }
            else
            {
                position = hit.point;
                Gizmos.DrawLine(startPos, position);
                return;
            }
            
        }
        else
        {
            position += direction * _maxStepDistance;
        }

        //Draw Line
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPos, position);

        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }

    public void Activate()
    {
        _isActive = true;
        
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public void SetLightEmitter(GameObject go)
    {
        _hittingLightEmitter = go;
    }
}
