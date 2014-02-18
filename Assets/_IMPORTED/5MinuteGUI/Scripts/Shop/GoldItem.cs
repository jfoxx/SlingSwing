//#define GOT_PRIME31_STORE_KIT_PLUGIN

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class GoldItem {
	public string upgradeName;
	public string costInUSD = "1";
	public int goldBonus = 1000;
	
	public string productID = "";

	
	public void purchaseItem( string pid)
	{
		if(pid.Equals(productID))
		{
			int curGold = Globals.getTotalScore(0);
			Globals.setTotalScore(curGold + goldBonus);			
		}
	}
	
	protected virtual void attemptUpgrade()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
#if UNITY_IPHONE
#if GOT_PRIME31_STORE_KIT_PLUGIN
			StoreKitBinding.purchaseProduct( productID,1);
#endif				
#endif			
		}else{
			int curGold = Globals.getTotalScore(0);
			Globals.setTotalScore(curGold + goldBonus);
		}
	}
	public Rect onGUI(Rect r,BaseMenuState bms)
	{
		Rect outRect = r;
		outRect.y+=30f/640f;
		outRect.x+=20f/960f;
		outRect.width=250f/960f;
		outRect.height = 60f/640f;
		
		GUI.Box(GUIHelper.screenRect(outRect),upgradeName);

		outRect.x += 270f/960f;
		outRect.width=150f/960f;
		
		GUI.enabled=true;
		if(bms.addButton(GUIHelper.screenRect(outRect),"Cost $" + costInUSD))
		{
			attemptUpgrade();
		}
		outRect.y += outRect.height + 5f/640f;
		GUI.enabled=true;
		return outRect;
	}

}
