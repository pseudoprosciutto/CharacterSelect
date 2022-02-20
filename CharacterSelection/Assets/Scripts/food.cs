using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public FoodType foodEat;
    public int cost;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(foodEat == FoodType.Great)
        {
            cost = 25;
        }
        else if (foodEat == FoodType.Good)
        {
            cost = 15;
        }
        else if (foodEat == FoodType.Poor)
        {
            cost = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
