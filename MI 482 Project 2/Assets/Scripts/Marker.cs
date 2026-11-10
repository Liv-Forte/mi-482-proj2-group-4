using UnityEngine;
using UnityEngine.InputSystem;

public class Marker : MonoBehaviour
{
    public Transform rayOrigin;                
    public InputActionProperty drawAction;     
    public Color color = Color.black;
    public int brushSize = 8;
    public float maxDistance = 3f;
    public LayerMask boardMask = ~0;
    public LineRenderer laser;               

    Whiteboard board;
    Vector2 lastUV;
    bool wasDrawing;

    void OnEnable()  
    { 
        drawAction.action?.Enable(); 
    }
    void OnDisable() 
    { 
        drawAction.action?.Disable(); 
    }

    void Update()
    {
        bool hitBoard = Physics.Raycast(rayOrigin.position, rayOrigin.forward,out RaycastHit hit, maxDistance, boardMask) && hit.collider.TryGetComponent(out board);

        if (laser)
        {
            laser.positionCount = 2;
            laser.SetPosition(0, rayOrigin.position);
            laser.SetPosition(1, hitBoard ? hit.point : rayOrigin.position + rayOrigin.forward * maxDistance);
        }

        bool pressed = drawAction.action != null && drawAction.action.IsPressed();

        if (hitBoard && pressed)
        {
            Vector2 uv = hit.textureCoord;
            int size = board.textureSize;

            if (wasDrawing)
                for (float t = 0; t <= 1f; t += 0.1f)
                {
                    Vector2 p = Vector2.Lerp(lastUV, uv, t);
                    Draw((int)(p.x * size), (int)(p.y * size));
                }

            else Draw((int)(uv.x * size), (int)(uv.y * size));

            board.texture.Apply();
            lastUV = uv;
            wasDrawing = true;
        }

        else wasDrawing = false;
    }

    void Draw(int cx, int cy)
    {
        int size = board.textureSize;

        for (int x = -brushSize; x <= brushSize; x++)

            for (int y = -brushSize; y <= brushSize; y++)
            {

                if (x * x + y * y > brushSize * brushSize) continue;
                
                int px = cx + x, py = cy + y;
                if (px >= 0 && px < size && py >= 0 && py < size)
                    board.texture.SetPixel(px, py, color);

            }
    }
}