using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyGameApplication.Item;
using System;

namespace MyGameApplication {
	public class ItemInfo : ScriptableObject {
		public List<Sheet> sheets = new List<Sheet>();

		[System.SerializableAttribute]
		public class Sheet {
			public string name = string.Empty;
			public List<Param> list = new List<Param>();
		}

		[System.SerializableAttribute]
		public class Param : BaseItem {
			public bool consumable;
			public bool isCarItem;

			[NonSerialized] public PropEffect effect;

			public Param() {
				type = ItemType.Prop;
			}
		}
	}
}
