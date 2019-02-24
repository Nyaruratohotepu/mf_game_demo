using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using Assets.Scripts.Inventory;
using System;

public class Bag :BagWindow {
    public GList ItemList;
	public Bag()
    {

    }
    protected override void OnInit()
    {
         
    }
    public void AddItem(int id,string picturename,int number,int maxnumber)
    {
        int i ;
        List<GObject> list=ItemList._children;
        for (i = 0; i < list.Count; i++)
        {
            if (list[i].asButton.icon == null)
            {
                list[i].asButton.icon = UIPackage.GetItemURL("GamingMain", "1");
                list[i].asButton.GetChild("num").asTextField.text = Mathf.Min(number,maxnumber).ToString();
                if (number > maxnumber)
                {
                    AddItem( id, picturename, number-maxnumber, maxnumber);
                }
                break;                                
            }
            else if (list[i].asButton.icon == UIPackage.GetItemURL("GamingMain", "1") && Convert.ToInt32(list[i].asButton.GetChild("num").asTextField.text)<maxnumber)
            {
                int rest = maxnumber - Convert.ToInt32(list[i].asButton.GetChild("num").asTextField.text);
                if (number <= rest)
                {
                    list[i].asButton.GetChild("num").asTextField.text = (Convert.ToInt32(list[i].asButton.GetChild("num").asTextField.text) + number).ToString();
                }
                else
                {
                    list[i].asButton.GetChild("num").asTextField.text = maxnumber.ToString();
                    AddItem(id, picturename, number-rest, maxnumber);
                }
                break;
            }
        }
        //到这里没跑出去就说明满了
    }
	
}
