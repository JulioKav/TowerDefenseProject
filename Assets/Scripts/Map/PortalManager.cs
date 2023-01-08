using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    // Inspired by: https://answers.unity.com/questions/429697/how-will-i-get-animated-gif-images-in-scene.html

    public bool portalActive = false;

    public GameObject front;
    public GameObject back;

    public Light pointLight;

    public Texture2D inactivePortal;
    public Texture2D[] activePortalFrames;
    int framesPerSecond = 10;

    Transform portal;

    Vector3 lookAt;

    // Start is called before the first frame update
    void Start()
    {
        pointLight.enabled = false;
        portal = transform.GetChild(0);
        FaceCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (!portalActive) return;

        int index = (int)(Time.time * framesPerSecond) % activePortalFrames.Length;
        front.GetComponent<MeshRenderer>().material.mainTexture = activePortalFrames[index];
        back.GetComponent<MeshRenderer>().material.mainTexture = activePortalFrames[index];
    }

    public void ActivatePortal()
    {
        portalActive = true;
        pointLight.enabled = true;
    }

    void FaceCamera()
    {
        var go = Instantiate(front, transform.position, Quaternion.identity, transform);
        go.transform.position += new Vector3(1, 0, 1);
        portal.LookAt(go.transform, Vector3.up);
        Destroy(go);
    }

    IEnumerator TurnToPath()
    {
        var go = Instantiate(new GameObject(), lookAt, Quaternion.identity);
        go.transform.LookAt(portal);
        while (true)
        {
            Vector3 rot = Vector3.RotateTowards(portal.rotation.eulerAngles, go.transform.rotation.eulerAngles, Mathf.PI / 15f, 10);
            if (portal.rotation.eulerAngles == rot) break;
            portal.rotation = Quaternion.Euler(rot);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(go);
    }

    public void SetPathDirection(Vector3 pos)
    {
        lookAt = pos;
        StartCoroutine(TurnToPath());
    }
}
