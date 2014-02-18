using UnityEngine;
using System.Collections;

public class UpgradeShop : BaseMenuState {
	public Rect startRect = new Rect(.5f,.2f,.45f,.6f);
	public string shopID = "Gold Shop";
	public ShopItem[] items;

	
	public override void onGUI()
	{
		float mx = transform.position.x;
		float my = transform.position.y;
		
			GUI.skin = guiSkin0;
		Rect r = startRect;
		r.x+=mx;
		r.y+=my;
		
		GUI.Box(GUIHelper.screenRect(r),shopID + "\nGold:" + Globals.getIntValue(Globals.State.TOTAL_SCORE,0));
		float x0 = r.x;
		r.y += 40f/960f;
		
		for(int i=0; i<items.Length; i++)
		{
			r.x = x0;
			r = items[i].onGUI(r,this);
			
		}
	}
}
