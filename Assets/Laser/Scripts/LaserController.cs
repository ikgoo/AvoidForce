//======================================
/*
@autor ktk.kumamoto
@date 2017.8.11 create
@note LaserController
*/
//======================================

using System;
using System.Collections;
using System.Collections.Generic;
using TWOPRO.Scripts.TWOPRO;
using TWOPROLIB.ScriptableObjects.Entitys;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

public class LaserController : MonoBehaviour
{
	public Entity entity;

	public float length = 500f;         //laser_length
	public float width = 0.1f;          //laser_width
	public float OvarAll_Size = 1.0f;   //eff_scale

	public GameObject hit_effect;       //hitEffect:GameObject

	[SerializeField]
	private GameObject laser_add;       //main_laser_add:GameObject

	[SerializeField]
	private GameObject laser_alpha;         //main_laser_alpha:GameObject

	[SerializeField]
	private GameObject trf_scaleController;         //eff_scale:GameObject


	void Start()
	{

		// Effect Scale
		if (trf_scaleController)
		{
			trf_scaleController.transform.localScale = new Vector3(OvarAll_Size, OvarAll_Size, OvarAll_Size);
		}

		// laser width
		if (laser_add)
		{
			var pa1_width = laser_add.GetComponent<ParticleSystem>().main;
			pa1_width.startSize = width;
		}
		if (laser_alpha)
		{
			var pa2_width = laser_alpha.GetComponent<ParticleSystem>().main;
			pa2_width.startSize = width;
		}

		// laser length
		if (laser_add)
		{
			var pa1_length = laser_add.GetComponent<ParticleSystemRenderer>();
			pa1_length.lengthScale = length / width / 10;
		}
		if (laser_alpha)
		{
			var pa2_length = laser_alpha.GetComponent<ParticleSystemRenderer>();
			pa2_length.lengthScale = length / width / 10;
		}

	}


	void Update()
	{
		// Effect Scale
		if (trf_scaleController)
		{
			trf_scaleController.transform.localScale = new Vector3(OvarAll_Size, OvarAll_Size, OvarAll_Size);
		}


		// laser length
		if (laser_add)
		{
			var pa1_length = laser_add.GetComponent<ParticleSystemRenderer>();
			pa1_length.lengthScale = length / width / 10;
		}
		if (laser_alpha)
		{
			var pa2_length = laser_alpha.GetComponent<ParticleSystemRenderer>();
			pa2_length.lengthScale = length / width / 10;
		}


		// Hit Controller:
		RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);

		bool b = false;
		for(int i = 0; i < hits.Length; i++)
        {
			RaycastHit hit = hits[i];
			b = true;

			//Debug.Log (hit.distance);
			if (hit.collider && hit.distance <= length / 10 * OvarAll_Size && hit.transform.gameObject.tag.Equals("Player"))
			{

				if (laser_add)
				{
					var pa1_length = laser_add.GetComponent<ParticleSystemRenderer>();
					pa1_length.lengthScale = hit.distance * 10 / width / 10 / OvarAll_Size;
				}
				if (laser_alpha)
				{
					var pa2_length = laser_alpha.GetComponent<ParticleSystemRenderer>();
					pa2_length.lengthScale = hit.distance * 10 / width / 10 / OvarAll_Size;
				}

				//Hit Effect Instance
				try
				{
					GameObject interactableGameObject = GamePrefabPoolManager.Instance.GetObjectForTypeWithPoolDestroy(hit_effect.name, false, true, 0.2f, false, null, 0, false, false);
					interactableGameObject.transform.position = hit.point;
					interactableGameObject.transform.localScale = new Vector3(OvarAll_Size, OvarAll_Size, OvarAll_Size);
					interactableGameObject.SetActive(true);
				}
				catch (Exception ex)
				{
					DebugX.Log("DestroyAction", ex);
				}

				//GameObject ins_hiteff = (GameObject)Instantiate(hit_effect, hit.point, Quaternion.identity);
				//ins_hiteff.transform.localScale = new Vector3(OvarAll_Size, OvarAll_Size, OvarAll_Size);

				hit.transform.gameObject.GetComponent<StateController3D_Player>().Interactable(hit.transform.gameObject.tag, entity, 0, gameObject);


			}
		}


		if(b == false)
        {
			// laser length
			if (laser_add)
			{
				var pa1_length = laser_add.GetComponent<ParticleSystemRenderer>();
				pa1_length.lengthScale = length / width / 10;
			}
			if (laser_alpha)
			{
				var pa2_length = laser_alpha.GetComponent<ParticleSystemRenderer>();
				pa2_length.lengthScale = length / width / 10;
			}
		}

		// laser width
		if (laser_add)
		{
			var pa1_width = laser_add.GetComponent<ParticleSystem>().main;
			pa1_width.startSize = width;
		}
		if (laser_alpha)
		{
			var pa2_width = laser_alpha.GetComponent<ParticleSystem>().main;
			pa2_width.startSize = width;
		}

	}
}
