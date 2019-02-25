using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using Assets.Scripts.Inventory;
using System;
using Assets.Scripts.Inventory;
public class Bag :BagWindow {
    public GList ItemList;
    public Dictionary<GButton, IInventoryItem> dic;
    public bool isshowinginformation = false;
    public GComponent DesCription;
    public Camera UICamera;
	public Bag()
    {

    }
    protected override void OnInit()
    {
         
    }
    public void AddItem(int id,string picturename,int number,int maxnumber,string kind)
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
                    AddItem( id, picturename, number-maxnumber, maxnumber,kind);
                }
                if (kind == "weapon")
                {
                    list[i].asButton.onRollOver.Add( ShowWeaponDescription);
                    list[i].asButton.onRollOut.Add(CloseDescription);                    
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
                    AddItem(id, picturename, number-rest, maxnumber,kind);
                }
                break;
            }
        }
        //到这里没跑出去就说明满了
    }
    public void ShowWeaponDescription()
    {
        if (isshowinginformation == false)
        {
            DesCription=UIPackage.CreateObject("GamingMain", "bagweaponitemdescribe").asCom;
            this.contentPane.AddChild(DesCription);
            isshowinginformation = true;
        }
        //DesCription.position = new Vector3(b.position.x +860, b.position.y+160, b.position.z);
        DesCription.position = this.contentPane.GlobalToLocal(new Vector2( Input.mousePosition.x+10,Screen.height-Input.mousePosition.y+10));
    }
    public void CloseDescription()
    {
        this.contentPane.RemoveChild(DesCription);
        DesCription.Dispose();
        isshowinginformation = false;        
    }
    
}
