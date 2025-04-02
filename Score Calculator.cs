using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TMP Text component for the score
    private int score = 0;

    void Start()
    {
        // Find the scoreText TMP element in the scene
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has one of the specified tags
        if (collision.gameObject.CompareTag("Ally") || collision.gameObject.CompareTag("Enemy"))
        {
            // Update the score based on the tag of the collided object
            if (collision.gameObject.CompareTag("Ally"))
            {
                score -= 1;
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                score += 1;
            }

            // Update the score text on the canvas
            UpdateScoreText();

            // Expand the radius of the cannon to 3x3 tiles
            ExpandRadius();

            // Destroy the object we collided with
            Destroy(collision.gameObject);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void ExpandRadius()
    {
        // Get the current position of the cannon
        Vector3 position = transform.position;

        // Define the radius expansion (3x3 tiles)
        float radius = 2.5f; // Assuming each tile is 1 unit, 3x3 tiles would be 1.5 units in each direction

        // Find all colliders within the radius
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        // Destroy all objects within the radius that have the "Ally" or "Enemy" tag
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ally"))
            {
                score -= 1;
                Destroy(collider.gameObject);
            }
            else if (collider.CompareTag("Enemy"))
            {
                score += 1;
                Destroy(collider.gameObject);
            }
        }

        // Update the score text on the canvas
        UpdateScoreText();
    }
}
