using UnityEngine;
using System.Collections;

public class BaseIngredient
{

    public string Name { get; set; } 
    // Verweis auf Bild der Pflanze??:
    public string Object { get; set; }
    
    public BaseIngredient(string Name, string Object){
        this.Name = Name;
        this.Object = Object;
    }

    public string ToString()
    {
        return Name;
    }
}
