using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserReflect : MonoBehaviour
{
    public int _maxReflectionCount = 5;
    public float _maxStepDistance = 200;
    public bool _drawPrediction;

    private LineRenderer _lineRenderer;
    public List<Vector3> _lineVertices;
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
        _lineVertices.Clear();
        _lineVertices.Add(this.transform.position);
        _ray = new Ray(_lineVertices[0], this.transform.forward);
        if (Physics.Raycast(_ray, out _hit, _maxStepDistance))
        {
            if (_hit.collider.gameObject.tag == "Mirror")
            {
                ReflectLineRenderer(_lineVertices[0], this.transform.forward, _maxReflectionCount);
                
            }
            else
            {
                _lineVertices.Add(_hit.point);
                if (_hit.collider.gameObject.tag == "Switch")
                {

                }
            }
        }
        else
        {
            _lineVertices.Add(this.transform.position + (this.transform.forward * _maxStepDistance));
        }
        _lineRenderer.positionCount = _lineVertices.Count;
        _lineRenderer.SetPositions(_lineVertices.ToArray());
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        if (_drawPrediction)
        {
            DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, _maxReflectionCount);

        }
    }

    void ReflectLineRenderer(Vector3 position, Vector3 direction, int reflectionsLeft)
    {
        if (reflectionsLeft == 0) return;

       
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxStepDistance))
        {
            if (hit.collider.gameObject.tag == "Mirror")
            {
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else if (hit.collider.gameObject.tag == "Switch")
            {
                GameObject switchGO;
                CrystalSwitch crystal;
                switchGO = hit.collider.gameObject;
                crystal = switchGO.GetComponent<CrystalSwitch>();
                crystal.Activate();
            }
            else
            {
                //crystal.Deactivate();
            }
            position = hit.point;
            _lineVertices.Add(position);
        }
        else
        {
            position += direction * _maxStepDistance;
            _lineVertices.Add(position);
            return;
        }
        
        ReflectLineRenderer(position, direction, reflectionsLeft - 1);
    }

    void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        //Return if no more reflections
        if (reflectionsRemaining == 0) return;

        Vector3 startPos = position;

        //Raycast to detect collider, reflect if mirror
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _maxStepDistance) && hit.collider.gameObject.tag == "Mirror")
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
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
}
