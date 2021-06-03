﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Arachnid
{
	[RequireComponent(typeof(Collider))]
	public class FilteredTrigger : MonoBehaviour
	{

		[ToggleLeft]
		public bool debug;
		
		[ToggleLeft, Tooltip("Will only trigger once.")]
		public bool oneOff;

		[ToggleLeft, Tooltip("Only allow triggers from objects of a particular collection")]
		public bool filterTriggers = true;
		
		[ToggleLeft]
		public bool useLayerMask;
		
		[Tooltip("Layers which will trigger this."), ShowIf("useLayerMask")]
		public LayerMask layerMask;

		[ShowIf("filterTriggers"), Tooltip("Any object in one of these collections can trigger this."), AssetsOnly]
		public List<Collection> triggerers = new List<Collection>();

		bool _triggered;


		void OnTriggerEnter(Collider other)
		{
			if (!filterTriggers)
			{
				Trigger(other);
				return;
			}

			foreach (var c in triggerers)
			{
				if (c.ContainsGameObject(other.gameObject))
				{
					Trigger(other);
					return;
				}
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (!filterTriggers)
			{
				TriggerExit(other);
				return;
			}

			foreach (var c in triggerers)
			{
				if (c.ContainsGameObject(other.gameObject))
				{
					TriggerExit(other);
					return;
				}
			}
		}

		void Trigger(Collider other)
		{
			if (debug) Debug.Log(other.name + " has triggered " + name + ". Running further checks...");
			if (!PassedOneOffCheck()) return;
			if (!PassedLayerMaskCheck(other)) return;
			_triggered = true;
			OnTriggered(other);
		}

		void TriggerExit(Collider other)
		{
			if (debug) Debug.Log(other.name + " has exited " + name + ". Running further checks...");
			if (!PassedOneOffCheck()) return;
			if (!PassedLayerMaskCheck(other))return;
			_triggered = true;
			OnTriggerExited(other);
		}

		bool PassedLayerMaskCheck(Collider other)
		{
			if (useLayerMask && !Math.LayerMaskContainsLayer(layerMask, other.gameObject.layer))
			{
				Debug.Log(other.name + " is in a layer that doesn't trigger " + name, gameObject);
				return false;
			}

			return true;
		}
		
		bool PassedOneOffCheck()
		{
			if (_triggered && oneOff)
			{
				if (debug) 
					Debug.Log(name + " is a one-off trigger, and has already been activated.", gameObject);
				return false;
			}

			return true;
		}

		protected virtual void OnTriggerExited(Collider triggerer)
		{
			
		}

		protected virtual void OnTriggered(Collider triggerer)
		{
		}
	}
}