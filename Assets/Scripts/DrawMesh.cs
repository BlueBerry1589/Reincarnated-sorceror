using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawMesh : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private Texture2D symbolTemplate;
    [SerializeField] private XRGrabInteractable wand;
    [SerializeField] private LayerMask drawingLayerMask;

    private const int k_TextureSide = (int)(1.5f * 500);

    private Texture2D _drawTexture;
    private Material _material;
    private MeshCollider _meshCollider;
    private GameObject _cursor;
    private MeshRenderer _cursorRenderer;

    private bool _isDrawing;
    private Vector2 _lastDrawPos;

    private void Start()
    {
        CreateMesh();
        InitializeDrawingTexture();
        SetupCollider();
        CreateCursor();
    }

    private void CreateMesh()
    {
        const float drawingSide = 1.5f;
        const float offset = drawingSide / 2;
        Vector3[] vertices =
        {
            new Vector3(-1, +1) * offset,
            new Vector3(-1, -1) * offset,
            new Vector3(+1, -1) * offset,
            new Vector3(+1, +1) * offset,
        };
        var uv = new Vector2[4];
        // Par défaut les éléments du tableau sont null, c'est pourquoi on fait appelle à cette méthode.
        Array.Fill(uv, Vector2.zero);

        int[] triangles = { 0, 3, 1, 1, 3, 2 };

        var mesh = new Mesh
        {
            vertices = vertices,
            uv = uv,
            triangles = triangles
        };

        // Permet d'optimiser le mesh pour des mises à jour fréquentes
        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void InitializeDrawingTexture()
    {
        _drawTexture = new Texture2D(k_TextureSide, 750);

        var pixels = new Color[k_TextureSide * k_TextureSide];
        Array.Fill(pixels, new Color(1, 1, 1, 0.5f));
        _drawTexture.SetPixels(pixels);
        _drawTexture.Apply();

        _material = new Material(Shader.Find("Unlit/Transparent"));
        _material.mainTexture = _drawTexture;
        GetComponent<MeshRenderer>().material = _material;
    }

    private void SetupCollider()
    {
        _meshCollider = gameObject.AddComponent<MeshCollider>();
        _meshCollider.sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    private void CreateCursor()
    {
        const float cursorSize = 0.1f;
        _cursor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _cursor.transform.localScale = Vector3.one * cursorSize;
        _cursorRenderer = _cursor.GetComponent<MeshRenderer>();
        _cursorRenderer.enabled = false;
        
        var cursorCollider = _cursor.GetComponent<Collider>();
        if (cursorCollider != null)
        {
            // Important sinon cela entraîne des clignotements indésirables.
            Destroy(cursorCollider);
        }
    }

    private void Update()
    {
        // Empêche le curseur de s'afficher une fois la baguette lâchée
        if (!wand.isSelected)
        {
            _cursorRenderer.enabled = false;
            _isDrawing = false;
            return;
        }
        
        var ray = new Ray(rayInteractor.transform.position, rayInteractor.transform.forward);
        if (!Physics.Raycast(ray, out var hit) || hit.collider.gameObject != gameObject)
        {
            _cursorRenderer.enabled = false;
            _isDrawing = false;
            return;
        }
        
        _cursorRenderer.enabled = true;

        _cursor.transform.position = hit.point + hit.normal * 0.02f;
        _cursor.transform.rotation = Quaternion.LookRotation(hit.normal);

        var pixelUV = hit.textureCoord * k_TextureSide;
        if (IsDrawingInput())
        {
            if (!_isDrawing)
            {
                _isDrawing = true;
                _lastDrawPos = pixelUV;
            }

            DrawLine(_lastDrawPos, pixelUV);
            _lastDrawPos = pixelUV;
            _cursorRenderer.material.color = Color.yellow;
        }
        else
        {
            _cursorRenderer.material.color = Color.white;
            _isDrawing = false;
        }
    }

    private bool IsDrawingInput()
    {
        var actionController = rayInteractor.GetComponent<ActionBasedController>();
        return actionController.activateAction.action.ReadValue<float>() > 0.5f;
    }

    private void DrawLine(Vector2 from, Vector2 to)
    {
        var steps = (int)Vector2.Distance(from, to);
        for (var i = 0; i < steps; ++i)
        {
            var t = i / (float)steps;
            var point = Vector2.Lerp(from, to, t);
            DrawBrush((int)point.x, (int)point.y);
            _drawTexture.Apply();
        }
    }

    private void DrawBrush(int centerX, int centerY)
    {
        const int brushSize = 5;
        for (var x = -brushSize; x <= brushSize; x++)
        {
            for (var y = -brushSize; y <= brushSize; y++)
            {
                if (x * x + y * y > brushSize * brushSize) continue; // Brosse circulaire
                var pixelX = centerX + x;
                var pixelY = centerY + y;

                if (0 <= pixelX && pixelX < k_TextureSide && 0 <= pixelY && pixelY < k_TextureSide)
                {
                    _drawTexture.SetPixel(pixelX, pixelY, Color.yellow);
                }
            }
        }
    }

    public float CheckAccuracy()
    {
        if (symbolTemplate is null) return 0f;

        var matchingPixels = 0;
        var totalRelevantPixels = 0;

        for (var x = 0; x < k_TextureSide; x++)
        {
            for (var y = 0; y < k_TextureSide; y++)
            {
                var templatePixel = symbolTemplate.GetPixel(x, y);
                var drawnPixel = _drawTexture.GetPixel(x, y);

                // Si le pixel du template est noir (fait partie du symbole)
                if (!(templatePixel.grayscale < 0.5f)) continue;
                totalRelevantPixels++;

                // Si le joueur a aussi dessiné là
                if (drawnPixel.grayscale < 0.5f)
                {
                    matchingPixels++;
                }
            }
        }

        return totalRelevantPixels > 0 ? (float)matchingPixels / totalRelevantPixels : 0f;
    }

    public bool ValidateDrawing()
    {
        const float matchThreshold = 0.7f;
        var accuracy = CheckAccuracy();
        Debug.Log($"Précision du dessin : {accuracy * 100}%");
        return accuracy >= matchThreshold;
    }

    public void ClearDrawing()
    {
        InitializeDrawingTexture();
    }
}