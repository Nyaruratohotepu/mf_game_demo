using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class Bag :BagWindow {

	public Bag()
    {

    }
    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("GamingMain", "bagmain").asCom;
    }
	
}
