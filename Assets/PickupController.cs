using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Camera _camera;
    public float suckSpeed = 20;
    public float maxHitdistance = 8;

    public AudioSource vacuumSound;
    public GameObject vacTarget;
    
    private bool soundPlaying = false;

    private float radius = 0.3f;
    private float height = 2.0f;
    
    private InventoryController _inventoryController;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _inventoryController = GetComponent<InventoryController>();
        vacuumSound.volume = 0.3f;
        if(PlayerPrefs.HasKey("suckSpeed")){
             suckSpeed = PlayerPrefs.GetFloat("suckSpeed");
        }
        if(PlayerPrefs.HasKey("distance")){
            maxHitdistance = PlayerPrefs.GetFloat("distance");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButton("Fire1"))
        {
            vacuumSound.Stop();
            soundPlaying = false;
            return;
        }
        else{
            if(!soundPlaying && !GameManager.GetIsFrozen()){
                vacuumSound.Play();
                soundPlaying = true;
            }
        }

        var vacTargetPos = vacTarget.transform.position + (transform.right * 0.25f); // offset the position a bit to the right

        // Set the coneOrigin to the camera's position
        var coneOrigin = _camera.transform;
        var origin = coneOrigin.position;
        var direction = coneOrigin.forward;

        var hits = Physics.CapsuleCastAll(origin + direction * radius, origin + direction * (height - radius), radius,
            direction, Mathf.Infinity);

        if (hits.Length < 1) return;

        if (Input.GetButtonDown("Fire1"))
        {
            foreach (var _hit in hits)
            {
                // ensure not too far away
                var hitDistance = Vector3.Distance(vacTargetPos, _hit.transform.position);
                if (hitDistance > 4f) continue; // too far away!

                if (_hit.transform.CompareTag("RecyclingBin"))
                {
                    _inventoryController.EmptyRecyclables();
                    return;
                }

                if (_hit.transform.CompareTag("TrashBin"))
                {
                    _inventoryController.EmptyTrash();
                    return;
                }
            }
        }

        // we want to only let the player collect the closest piece of trash
        RaycastHit? nHit = null;
        foreach (var _hit in hits)
        {
            if (!_hit.transform.CompareTag("Trash")) continue;
            var hitDistance = Vector3.Distance(vacTargetPos, _hit.transform.position);
            if (hitDistance > maxHitdistance) continue; // too far away!
            nHit = _hit;
            break;
        }

        if (nHit == null)
            return;

        var hit = nHit.Value; // we got a hit!

        // Calculate the distance between the player and the hit object
        var distance = Vector3.Distance(vacTargetPos, hit.transform.position);

        // if super close, destroy object
        if (distance < 1.3f)
        {
            var cleanName = InventoryController.CleanObjectName(hit.transform.name);
           // var pointVal = hit.transform.PointValue;
           // Debug.Log(pointVal);
            _inventoryController.AddTrash(cleanName);
            Destroy(hit.transform.gameObject);
            return;
        }

        // Calculate the pull speed based on the distance
        var pullSpeed = suckSpeed / Mathf.Sqrt(distance);

        // Normalize the direction vector from the hit object to the player
        var position = hit.transform.position;
        Vector3 pullDirection = (vacTargetPos - position).normalized;

        // Apply the pull force to the hit object
        position += pullDirection * (pullSpeed * Time.deltaTime);
        hit.transform.position = position;
    }
}
