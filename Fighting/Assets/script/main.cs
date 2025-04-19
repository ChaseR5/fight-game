using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public GameObject dummy;
	public int targetFrameRate = 30;
	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = targetFrameRate;
	}
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnDummy(){
        Instantiate(dummy, new Vector2(0,0), gameObject.transform.rotation);
    }
}
