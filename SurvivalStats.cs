using UnityEngine;

public class SurvivalStats : MonoBehaviour
{

    private float maxHunger;
    private float maxThirst;
    private float maxEnergy;
    private float currentHunger;
    private float currentThirst;
    private float currentEnergy;


    /****** constructor class to initialize maxValues on other script. Good for encapsulation, allows the class to control how it is initialized
     * 
     * private SurvivalStats survivalStats;
     * 
     * 
     * private void Start()
     * {
     * 
     * survivalStats = new SurvivalStats( 100f, 100f, 100f);
     * 
     * }
     * 
     * 
     * 
     ******* or just initialize on this script...
     * 
     * private float maxHunger = 100f;
     * 
     * 
     * 
     * 
     */


    public SurvivalStats(float maxHunger, float maxThirst, float maxEnergy)
    {
        this.maxHunger = maxHunger;
        this.maxThirst = maxThirst;
        this.maxEnergy = maxEnergy;
        this.currentHunger = maxHunger;
        this.currentThirst = maxThirst;
        this.currentEnergy = maxEnergy;
    }


    /* Remember to give negative values as amount in order to reduce hunger,thirst,energy.
     * 
     * Values clamped between, 0 and maxValues.
     * 
     */
    public void ModifyHunger(float amount)
    {
        currentHunger = Mathf.Clamp(currentHunger + amount, 0, maxHunger);
    }

    public void ModifyThirst(float amount)
    {
        currentThirst = Mathf.Clamp(currentThirst + amount, 0, maxThirst);
    }

    public void ModifyEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
    }





    // GetMethods gives hunger/thirst/energy levels as percentage. These methods can be used to show those values on UI.
    public float GetHungerLevel()
    {
        return (currentHunger / maxHunger) * 100f;
    }

    public float GetThirstLevel()
    {
        return (currentThirst / maxThirst) * 100;
    }

    public float GetEnergyLevel()
    {
        return (currentEnergy / maxEnergy) * 100f;
    }
}

