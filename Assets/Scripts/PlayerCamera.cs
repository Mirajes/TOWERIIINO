using TMPro;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private Material _highlightMaterial;

    private Renderer _currentHighlightedRenderer;
    private Material _originalMaterial;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void CheckForInteract()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _interactableLayer))
        {
            if (_currentHighlightedRenderer != hit.collider.GetComponent<Renderer>())
            {
                RemoveHighlight();
                HighlightObject(hit.collider.gameObject);
            }

        }
        else
        {
            RemoveHighlight();
        }
    }

    private void HighlightObject(GameObject obj)
    {
        _currentHighlightedRenderer = obj.GetComponent<Renderer>();
        if (_currentHighlightedRenderer != null)
        {
            _originalMaterial = _currentHighlightedRenderer.material;
            _currentHighlightedRenderer.material = _highlightMaterial;
        }
    }

    private void RemoveHighlight()
    {
        if (_currentHighlightedRenderer != null)
        {
            _currentHighlightedRenderer.material = _originalMaterial;
            _currentHighlightedRenderer = null;
        }
    }

    private void Update()
    {
        
    }
}
