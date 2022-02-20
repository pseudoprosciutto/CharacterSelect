using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FoodType
{
    Great, Good, Poor
}

public enum EffortType
{
    None, Jump, Walk, Run
}

public class EnergyDrain : MonoBehaviour
{
    public Slider energyMeter;
    public GameObject fillArea;
    public GameObject empty;
    public float regDrain;
    public float foodDrain;
    public float effortDrain;
    public float jumpDrain;
    public float food;
    public bool eat;
    public bool jump;
    public FoodType currentFood;
    public EffortType currentEffort;


    // Start is called before the first frame update
    void Awake()
    {
        eat = false;
        jump = false;
        empty.SetActive(false);
    }

    public IEnumerator Drain()
    {
        yield return new WaitForSeconds(0.5f);
        energyMeter.value -= (regDrain + foodDrain + effortDrain) * Time.deltaTime;
        if (eat)
        {
            energyMeter.value += food;
            eat = false;
        }
        if (jump)
        {
            energyMeter.value -= jumpDrain;
            jump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFood == FoodType.Great)
        {
            foodDrain = 0.3f;
            food = 15f;
        }
        else if (currentFood == FoodType.Good)
        {
            foodDrain = 0.5f;
            food = 6f;
        }
        else if (currentFood == FoodType.Poor)
        {
            foodDrain = 1f;
            food = 4f;
        }

        if (currentEffort == EffortType.Jump)
        {
            jumpDrain = 5f;
            effortDrain = 0f;
            jump = true;
        }
        else if(currentEffort == EffortType.Walk)
        {
            effortDrain = 0.5f;
        }
        else if (currentEffort == EffortType.Run)
        {
            effortDrain = 1.3f;
        }
        if (energyMeter.value > 0)
        {
            StartCoroutine(Drain());
        }
        else if(energyMeter.value == 0)
        {
            energyMeter.enabled = false;
            fillArea.SetActive(false);
            empty.SetActive(true);
        }
    }
}
