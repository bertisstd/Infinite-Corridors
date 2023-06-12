
namespace Bertis.Game
{
	using UnityEngine;

	[RequireComponent(typeof(SatelliteInfo))]
	public class TurretBehavior : MonoBehaviour
	{
		[SerializeField]
		private float m_RotateSpeed;
		[SerializeField]
		private Gun m_Gun;
		[SerializeField]
		private Sensor m_DetectSensor;

		private SatelliteInfo m_SatelliteInfo;

		private void Awake()
		{
			m_SatelliteInfo = GetComponent<SatelliteInfo>();
			m_Gun.Source = m_SatelliteInfo;
			m_DetectSensor.OnDetect += OnSensorDetect;
			enabled = false;
		}

		private void FixedUpdate()
		{
			var deltaAngle = ext.Math.ToDegree(PlayerInfo.Reference.transform.position - transform.position);

			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				Quaternion.Euler(0f, 0f, deltaAngle),
				m_RotateSpeed * Time.deltaTime);

		}

		private void OnSensorDetect(UnitInfo unit, bool forward)
		{
			if (unit is PlayerInfo)
			{
				m_Gun.PullTrigger(forward);
				enabled = forward;
			}
		}

	}
}