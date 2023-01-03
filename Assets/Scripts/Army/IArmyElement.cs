using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmyElement 
{
	ArmyManager ArmyManager { get; set; }
	float Health { get; }
}
