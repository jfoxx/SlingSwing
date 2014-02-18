using UnityEngine;
using System.Collections;
[System.Serializable]

public class ShopItem : BaseItem {
	public string upgradeName;
	public Rect onGUI(Rect r,BaseMenuState bms)
	{
		Rect outRect = r;
		outRect.y+=30f/640f;
		outRect.x+=20f/960f;
		outRect.width=250f/960f;
		outRect.height = 60f/640f;
		
		GUI.Box(GUIHelper.screenRect(outRect),upgradeName + " " + 
				(getCurrentLevel()+offset).ToString() + " / " + (getMaxLevel()+offset).ToString());

		outRect.x += 270f/960f;
		outRect.width=150f/960f;
		if(getCurrentLevel()<maxLevel)
		{			
			GUI.enabled=true;
			if(bms.addButton(GUIHelper.screenRect(outRect),"Cost:" + getGoldCost().ToString()))
			{
				attemptUpgrade();
			}
			
		}else{
			GUI.enabled=false;
			bms.addButton(GUIHelper.screenRect(outRect),"MAXED OUT");
		}
		outRect.y += outRect.height + 5f/640f;
		GUI.enabled=true;
		return outRect;
	}

}
