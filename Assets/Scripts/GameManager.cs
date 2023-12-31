using UnityEngine;
using Unity.Mathematics;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager manager;
    public static GameManager instance { get { return manager; } }
    public enum Actions { Blocksun, Attack, Water, None };
    [NonSerialized] public bool actioning = false;

    [Header("Health")]
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float statDmgRate = 0.8f;

    [Header("Sunlight")]
    [SerializeField] private int sunLevel = 50;
    [SerializeField] private int maxSunLevel = 100;
    [SerializeField] private float sunLevelDmgMax = 90f;
    [SerializeField] private float sunLevelDmgMin = 10f;
    [SerializeField] private float sunChangeRate = 0.5f;

    [Header("Water")]
    [SerializeField] private int waterLevel = 50;
    [SerializeField] private int maxWaterLevel = 100;
    [SerializeField] private float waterLevelDmgMax = 90f;
    [SerializeField] private float waterLevelDmgMin = 10f;
    [SerializeField] private float waterChangeRate = 0.5f;

    [Header("Actions")]
    public Actions action = Actions.None;

    [SerializeField] private RectTransform healthBar;
    [SerializeField] private RectTransform sunBar;
    [SerializeField] private RectTransform waterBar;
    [SerializeField] private Image swat;
    [SerializeField] private Sprite swatImg1;
    [SerializeField] private Sprite swatImg2;

    [SerializeField] private Transform attackZone;

    private void Start()
    {
        manager = this;
        setSunLevelRate(sunChangeRate);
        setWaterLevelRate(waterChangeRate);
        setStatDmgRate(statDmgRate);
        Invoke(nameof(leveler), UnityEngine.Random.Range(5, 7));
    }

    private void Update()
    {
        if (health <= 0) gameOver();

        minMaxChecks();
        changeBar(healthBar, health, maxHealth);
        changeBar(sunBar, sunLevel, maxSunLevel);
        changeBar(waterBar, waterLevel, maxWaterLevel);
    }

    private void gameOver()
    {
        // TODO: Implement Game Over
    }

    private void sunlightChange()
    {
        int change = 0;
        float difference = Mathf.Abs((float)waterLevel / maxWaterLevel) - ((float)sunLevel / maxSunLevel);
        if (difference <= 0.2) { change = 1; }
        else if (difference <= 0.4) { change = 3; }
        else if (difference <= 0.6) { change = 5; }
        else if (difference <= 0.8) { change = 8; }

        if (action == Actions.Blocksun && actioning) { change = -3; }
        sunLevel += change;
    }

    private void waterLevelChange()
    {
        int change = 0;
        float difference = ((float)waterLevel / maxWaterLevel) - ((float)sunLevel / maxSunLevel);
        if (difference <= 0.2) { change = -1; }
        else if (difference <= 0.4) { change = -3; }
        else if (difference <= 0.6) { change = -5; }
        else if (difference <= 0.8) { change = -8; }

        if (action == Actions.Water && actioning) { change = 3; }
        waterLevel += change;
    }

    private void statDmgChange()
    {
        if (sunLevel > sunLevelDmgMax || sunLevel < sunLevelDmgMin || waterLevel > waterLevelDmgMax || waterLevel < waterLevelDmgMin)
        {
            AddHealth(-3);
        }
    }

    private void changeBar(RectTransform bar, int value, int max)
    {
        bar.offsetMax = new Vector2(-math.remap(0, max, 460, 10, value), bar.offsetMax.y);
    }

    private void minMaxChecks()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        sunLevel = Mathf.Clamp(sunLevel, 0, maxSunLevel);
        waterLevel = Mathf.Clamp(waterLevel, 0, maxWaterLevel);
    }

    private void leveler()
    {
        float sunPercentage = (float)sunLevel / maxSunLevel * 100;
        float waterPercentage = (float)waterLevel / maxWaterLevel * 100;
        if (sunPercentage >= 40 && sunPercentage <= 60 && waterPercentage >= 40 && waterPercentage <= 60)
        {
            PlantController.instance.growPlant();
        }
        Invoke(nameof(leveler), UnityEngine.Random.Range(5,7));
    }

    /// <summary>
    /// Get the player health
    /// </summary>
    /// <returns>Players health</returns>
    public int GetHealth() { return health; }

    /// <summary>
    /// Set the players health
    /// </summary>
    /// <param name="health">Value to set players health to</param>
    public void SetHealth(int health) {
        this.health = health;
        changeBar(healthBar, health, maxHealth);
    }

    /// <summary>
    /// Add/subtract from players health
    /// </summary>
    /// <param name="deltaHealth">The change to the players health</param>
    public void AddHealth(int deltaHealth) {
        health += deltaHealth;
        changeBar(healthBar, health, maxHealth);
    }

    /// <summary>
    /// Change the rate that the sun change occurs
    /// </summary>
    /// <param name="changeRate">The time between each decay or increase</param>
    public void setSunLevelRate(float changeRate)
    {
        sunChangeRate = changeRate;
        CancelInvoke(nameof(sunlightChange));
        InvokeRepeating(nameof(sunlightChange), 0f, sunChangeRate);
    }

    /// <summary>
    /// Change the rate that the water level change occurs
    /// </summary>
    /// <param name="changeRate">The time between each decay or increase</param>
    public void setWaterLevelRate(float changeRate)
    {
        waterChangeRate = changeRate;
        CancelInvoke(nameof(waterLevelChange));
        InvokeRepeating(nameof(waterLevelChange), 0f, waterChangeRate);
        
    }

    /// <summary>
    /// Change the rate that that damage occurs for having low stats
    /// </summary>
    /// <param name="changeRate">The time between each damage tick</param>
    public void setStatDmgRate(float changeRate)
    {
        statDmgRate = changeRate;
        CancelInvoke(nameof(statDmgChange));
        InvokeRepeating(nameof(statDmgChange), 0f, waterChangeRate);

    }

    public void attack()
    {
        if (actioning) { return; }
        actioning = true;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;
        attackZone.position = worldPosition;
        swat.sprite = swatImg2;
        Invoke(nameof(moveAttack), 0.4f);
        Invoke(nameof(swatAnim), 0.2f);
    }

    private void moveAttack()
    {
        attackZone.position = new Vector3(-20, 0);
        actioning = false;
    }

    private void swatAnim() {
        swat.sprite = swatImg1;
    }
}
