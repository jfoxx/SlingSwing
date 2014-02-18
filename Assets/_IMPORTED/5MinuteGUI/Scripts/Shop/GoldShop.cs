//#define GOT_PRIME31_STORE_KIT_PLUGIN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoldShop : BaseMenuState {
	public Rect startRect = new Rect(.5f,.2f,.45f,.5f);
	public string shopID = "Gold Shop";
	public GoldItem[] items;
	
	//do we want to save hte gold after resetting the data.
	public bool saveGold = true;
#if UNITY_IPHONE
#if GOT_PRIME31_STORE_KIT_PLUGIN
	public string[] productList;
	private List<StoreKitProduct> _products;
	void Start () {
		StoreKitBinding.requestProductData( productList );
		StoreKitManager.productListReceivedEvent += allProducts =>
		{
			Debug.Log( "received total products: " + allProducts.Count );
			_products = allProducts;
		};
	}
	void OnEnable()
	{
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelled;
		StoreKitManager.purchaseFailedEvent += purchaseFailed;			
	}
	void OnDisable()
	{
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent-= purchaseCancelled;
		StoreKitManager.purchaseFailedEvent -= purchaseFailed;			
	}
	void purchaseFailed( string error )
	{
		Debug.Log( "purchase failed with error: " + error );
	}
	

	void purchaseCancelled( string error )
	{
		Debug.Log( "purchase cancelled with error: " + error );
	}
	
	
	void purchaseSuccessful( StoreKitTransaction transaction )
	{
		for(int i=0; i<items.Length; i++)
		{
			items[i].purchaseItem( transaction.productIdentifier );
		}
	}	
#endif
#endif	
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
		r.y+=r.height-40/640f;
		r.height = 60f/640f;
		if(addButton(GUIHelper.screenRect(r),"Reset") )
		{
			int gold = Globals.getIntValue(Globals.State.TOTAL_SCORE,0);
			PlayerPrefs.DeleteAll();
			if(saveGold)
			{
				Globals.setIntValue(Globals.State.TOTAL_SCORE,gold);
			}
		}
	}
}
