using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private PlayableDirector _currentDirector;
    private bool _sceneSkipped = true;
    private float _timeToSkipTo; 
    // Start is called before the first frame update
    public void GetDirector(PlayableDirector director)
    {
        _sceneSkipped = false;
        _currentDirector = director;
    }
    public void GetSkipTime(float skiptime)
    {
        _timeToSkipTo = skiptime;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !_sceneSkipped)
        {
            _currentDirector.time = _timeToSkipTo;
            _sceneSkipped = true;
        }
    }
}
