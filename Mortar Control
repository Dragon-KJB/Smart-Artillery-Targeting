using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour
{
    [Header("Mortar Components")]
    [SerializeField] private Transform mortarBase;    // Added base component
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform missileSpawnPoint;
    [SerializeField] private GameObject missilePrefab;

    [Header("Angle Settings")]
    [SerializeField] private float minElevationAngle = -30f;
    [SerializeField] private float maxElevationAngle = 50f;
    [SerializeField] private float currentElevationAngle = 0f;
    [SerializeField] private float elevationChangeSpeed = 15f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float currentRotationAngle = 0f;

    [Header("Firepower Settings")]
    [SerializeField] private float minFirePower = 10f;
    [SerializeField] private float maxFirePower = 50f;
    [SerializeField] private float currentFirePower = 30f;
    [SerializeField] private float powerChangeSpeed = 10f;

    [Header("Firing Settings")]
    [SerializeField] private float cooldownTime = 1.5f;

    private bool canFire = true;
    private float lastFireTime = 0f;

    void Start()
    {
        // Ensure the initial elevation angle and firepower are within allowed ranges
        currentElevationAngle = Mathf.Clamp(currentElevationAngle, minElevationAngle, maxElevationAngle);
        currentFirePower = Mathf.Clamp(currentFirePower, minFirePower, maxFirePower);

        // Set initial position
        UpdateMortarPosition();
    }

    void Update()
    {
        HandleInput();
        UpdateMortarPosition();

        // Update cooldown status
        if (!canFire && Time.time - lastFireTime >= cooldownTime)
        {
            canFire = true;
        }
    }

    void HandleInput()
    {
        // Adjust elevation angle (up/down)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentElevationAngle += elevationChangeSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentElevationAngle -= elevationChangeSpeed * Time.deltaTime;
        }

        // Ensure elevation angle stays within limits after any changes
        currentElevationAngle = Mathf.Clamp(currentElevationAngle, minElevationAngle, maxElevationAngle);

        // Rotate the base (left/right)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotationAngle += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotationAngle -= rotationSpeed * Time.deltaTime;
        }

        // Adjust fire power (which affects distance)
        if (Input.GetKey(KeyCode.W))
        {
            currentFirePower += powerChangeSpeed * Time.deltaTime;
            currentFirePower = Mathf.Clamp(currentFirePower, minFirePower, maxFirePower);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentFirePower -= powerChangeSpeed * Time.deltaTime;
            currentFirePower = Mathf.Clamp(currentFirePower, minFirePower, maxFirePower);
        }

        // Fire missile
        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            FireMissile();
        }
    }

    void UpdateMortarPosition()
    {
        // Update the base rotation (horizontal aiming)
        if (mortarBase != null)
        {
            mortarBase.localRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }

        // Update barrel elevation (vertical aiming)
        if (barrel != null)
        {
            Quaternion targetRotation = Quaternion.Euler(-currentElevationAngle, 0, 0);
            barrel.localRotation = targetRotation;
        }
    }

    void FireMissile()
    {
        if (!canFire) return;

        // Create missile
        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);

        // Get the missile's rigidbody
        Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();

        if (missileRigidbody != null)
        {
            // Use the missile spawn point's forward direction, which is now determined by both
            // the base rotation and barrel elevation
            Vector3 fireDirection = missileSpawnPoint.forward;

            // Apply force in the calculated direction using the current fire power
            missileRigidbody.AddForce(fireDirection * currentFirePower, ForceMode.Impulse);
        }

        // Start cooldown
        canFire = false;
        lastFireTime = Time.time;

        // Destroy missile after some time if it doesn't hit anything
        Destroy(missile, 10f);
    }

    // Helper method to set elevation angle directly (can be called from UI)
    public void SetElevationAngle(float angle)
    {
        currentElevationAngle = Mathf.Clamp(angle, minElevationAngle, maxElevationAngle);
        UpdateMortarPosition();
    }

    // Helper method to set rotation angle directly (can be called from UI)
    public void SetRotationAngle(float angle)
    {
        currentRotationAngle = angle;
        UpdateMortarPosition();
    }

    // Helper method to set fire power directly (can be called from UI)
    public void SetFirePower(float power)
    {
        currentFirePower = Mathf.Clamp(power, minFirePower, maxFirePower);
    }
}
