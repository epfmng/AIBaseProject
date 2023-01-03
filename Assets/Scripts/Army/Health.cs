using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField] float m_StartHealth;
	float m_Health;
	public float Value => m_Health;

	[SerializeField] Slider m_HealthBar;

	[SerializeField] UnityEvent m_OnDieEvent;

	private void Start()
	{
		m_Health = m_StartHealth;
		RefreshHealthDisplay();
	}

	void RefreshHealthDisplay()
	{
		m_HealthBar.value = m_Health / m_StartHealth;
	}

	public void InflictDamage(float damage)
	{
		m_Health = Mathf.Max(m_Health - damage, 0);
		RefreshHealthDisplay();

		if (m_Health == 0 && m_OnDieEvent != null) m_OnDieEvent.Invoke();
	}
}
