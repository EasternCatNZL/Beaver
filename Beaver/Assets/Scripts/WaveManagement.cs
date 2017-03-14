using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagement : MonoBehaviour {

    public Object VinePrefab;
    public bool TimerBased;
    public bool ElminBased;

    private int WaveNumber = 0;
    private int[] Fib = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
    private float m_fTimer = 0.0f;

    void Start()
    {
        if (!TimerBased && !ElminBased)
        {
            TimerBased = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(ElminBased)
        {
            if(GameObject.FindGameObjectsWithTag("VineRoot").Length <= 0)
            {
                NextWave();
            }
        }
        else
        {

        }
	}

    void NextWave()
    {
        Vine.SetMaxSplits(10);
        if (WaveNumber < Fib.Length)
        {
            SpawnVineRoots(Fib[WaveNumber]);
            WaveNumber++;
        }
    }

    void SpawnVineRoots(int _iAmount)
    {
        float _fWidthIntervals = 1.0f / (_iAmount + 1);
        float _fXPos = _fWidthIntervals;
        float _fYPos = 1.0f;
        Vector3 SpawnPos;
        
        for(int i = 0; i < _iAmount; ++i)
        {
            SpawnPos = new Vector3(_fXPos, _fYPos, 0.0f);
            print(SpawnPos.ToString());
            SpawnPos = Camera.main.ViewportToWorldPoint(SpawnPos);
            print(SpawnPos.ToString());
            SpawnPos = Vector3.Scale(SpawnPos, new Vector3(1.0f, 1.0f, 0.0f));
            print(SpawnPos.ToString());
            Instantiate(VinePrefab, SpawnPos, Quaternion.identity);
            _fXPos += _fWidthIntervals;
        }
    }
}
