using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    static Timer m_Instance;
    public static Timer Instance=>m_Instance;
    public static float Value => Instance.m_Timer;

    [SerializeField] TMP_Text m_TimerText;
    float m_Timer=0;

    void SetTimerText(float timer)
	{
        m_TimerText.text = ((int)timer).ToString("N00")+" s";
	}

	private void Awake()
	{
        if (!m_Instance) m_Instance = this;
        else Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
        m_Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        SetTimerText(m_Timer);
    }

    public void DeactivateTimer()
	{
        this.enabled = false;
	}
}
