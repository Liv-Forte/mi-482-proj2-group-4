using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public int textureSize = 1024;
    public Texture2D texture 
    { 
        get; private set; 
    }

    void Start()
    {
        texture = new Texture2D(textureSize, textureSize);
        Clear();
        GetComponent<Renderer>().material.mainTexture = texture;

    }

    public void Clear()
    {
        var pixels = new Color[textureSize * textureSize];

        for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.white;

        texture.SetPixels(pixels);
        texture.Apply();

    }
}