using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class Bag :BagWindow {
    GList ItemList;
	public Bag()
    {

    }
    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("GamingMain", "bagmain").asCom;
        ItemList = this.contentPane.GetChild("baglist").asList;
        

    }
    
	
}
