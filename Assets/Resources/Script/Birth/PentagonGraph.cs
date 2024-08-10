using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonGraph : MonoBehaviour
{
    public Transform MiddlePoint;
    public Transform WoodPoint;
    public Transform FirePoint;
    public Transform EarthPoint;
    public Transform MetalPoint;
    public Transform WaterPoint;

    public LineRenderer PentagonLine;
    public BattleCharacter character;

    public void UpdateGraph(BattleCharacter character)
    {
        Debug.Log("UpdateGraph method is called.");

        // Calculate the positions of the element dots
        Vector2 woodDotPosition = Vector2.Lerp(MiddlePoint.position, WoodPoint.position, character.WoodStat / 100f);
        Vector2 fireDotPosition = Vector2.Lerp(MiddlePoint.position, FirePoint.position, character.FireStat / 100f);
        Vector2 metalDotPosition = Vector2.Lerp(MiddlePoint.position, MetalPoint.position, character.MetalStat / 100f);
        Vector2 waterDotPosition = Vector2.Lerp(MiddlePoint.position, WaterPoint.position, character.WaterStat / 100f);
        Vector2 earthDotPosition = Vector2.Lerp(MiddlePoint.position, EarthPoint.position, character.EarthStat / 100f);

        Debug.Log("WoodDotPosition: " + woodDotPosition);
        Debug.Log("FireDotPosition: " + fireDotPosition);
        Debug.Log("MetalDotPosition: " + metalDotPosition);
        Debug.Log("WaterDotPosition: " + waterDotPosition);
        Debug.Log("EarthDotPosition: " + earthDotPosition);

        // Get the PolygonCollider2D component and set its points to the new positions
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }
        polygonCollider.points = new Vector2[] { woodDotPosition, fireDotPosition, metalDotPosition, waterDotPosition, earthDotPosition};

        Debug.Log("PolygonCollider2D points: " + polygonCollider.points.Length);

        // Get the LineRenderer component and set its positions to the new positions
        GameObject lineObject = new GameObject("LineObject");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 6;
        lineRenderer.SetPositions(new Vector3[] { woodDotPosition, fireDotPosition, metalDotPosition, waterDotPosition, earthDotPosition, woodDotPosition });
        
        lineRenderer.startWidth = 0.03f; // Set the start width of the line
        lineRenderer.endWidth = 0.03f; // Set the end width of the line
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Set the material of the line
        lineRenderer.startColor = Color.white; // Set the start color of the line
        lineRenderer.endColor = Color.white; // Set the end color of the line
        
        Debug.Log("WoodStat: " + character.WoodStat);
        Debug.Log("FireStat: " + character.FireStat);
        Debug.Log("MetalStat: " + character.MetalStat);
        Debug.Log("WaterStat: " + character.WaterStat);
        Debug.Log("EarthStat: " + character.EarthStat);
    }
}
