using UnityEngine;
using System.Collections;

public class Ingredient
{

    private string Name { get; set; } 
    // Verweis auf Bild der Pflanze??:
    private string Object { get; set; }
    
    public Ingredient(string Name, string Object){
        this.Name = Name;
        this.Object = Object;
    }
}
