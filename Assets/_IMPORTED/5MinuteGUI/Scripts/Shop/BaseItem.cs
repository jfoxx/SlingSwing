using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseItem  {
	public int initalLevel = 0;
	public int initalCost = 0;
	
	public int baseCost 	= 	100;
	
	public Globals.Upgrade shopID;
	public int maxLevel	=	5;	
	public int minVal=1;
	public int offset = 3;



	void onPressCBF(string id)
	{
		if(id.Equals("buy"))
		{
			attemptUpgrade();
		}
	}
	protected virtual void attemptUpgrade()
	{
		
		int curGold = Globals.getTotalScore(0);
		if(upgradeSpell(ref curGold))
		{
			Globals.setTotalScore(curGold);
		}		
	}

	
	public bool upgradeSpell(ref int gold)
	{
		int currentLevel = getCurrentLevel();
		bool canUpgrade = false;
		int goldCost = getGoldCost();
		if(currentLevel < maxLevel && gold>=goldCost)
		{
			currentLevel++;
			
			setLevel(  currentLevel);
			
			gold -= goldCost;
			canUpgrade = true;
		}
		return canUpgrade;
	}
	
	public virtual int getCurrentLevel()
	{
		int val = Mathf.Clamp(Globals.getIntValue(shopID,0),minVal,maxLevel);
		return val;
	}
	
	public virtual void setLevel(int levelID)
	{

		Globals.setIntValue(shopID,levelID);
	}
	
	public int getGoldCost()
	{
		int m = (getCurrentLevel()-1) + initalLevel;
		int n = (int)Mathf.Pow(2,m);
		n--;
//		Debug.Log ("m + " + m + "n " + n);
		int cost = initalCost + (n * baseCost);

		return cost;
	}
	
	public int getMaxLevel()
	{
		return maxLevel;
	}

}
