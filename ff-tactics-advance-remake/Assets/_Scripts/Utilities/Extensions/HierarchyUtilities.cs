using UnityEngine;

public static class HierarchyUtilities
{
    /// <summary>
    /// Create a texture with a given color
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width*height];
 
        for(int i = 0; i < pix.Length; i++)
            pix[i] = col;
 
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
 
        return result;
    }
    
    /// <summary>
    /// Set the parameter color to a brighter or darker one
    /// </summary>
    /// <param name="color">the color to modify</param>
    /// <param name="correctionFactor">|1 = default | <1 = darker | >1 = Lighter |</param>
    /// <returns></returns>
    public static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        return new Color(color.r * correctionFactor, color.g * correctionFactor, color.b * correctionFactor, 1f);
    }
}
