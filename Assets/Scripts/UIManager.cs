using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// The paint manager.
    /// </summary>
    public PaintManager paintManager;
    /// <summary>
    /// The cube paint.
    /// </summary>
    public GameObject cubePaint;
    /// <summary>
    /// The bubble paint.
    /// </summary>
    public GameObject earthPaint;
    /// <summary>
    /// The star paint.
    /// </summary>
    public GameObject starPaint;
    /// <summary>
    /// The dollar paint.
    /// </summary>
    public GameObject dollarPaint;
    /// <summary>
    /// The pepe paint.
    /// </summary>
    public GameObject pepePaint;
    /// <summary>
    /// The index of the paint type.
    /// </summary>
    private int paintIndex;
    /// <summary>
    /// The paint template count.
    /// </summary>
    private const int PAINT_TEMPLATE_COUNT = 5;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (paintManager == null)
        {
            throw new System.Exception("Paint manager not attached!");
        }
        if (cubePaint == null)
        {
            throw new System.Exception("Cube paint not attached!");
        }
        if (earthPaint == null)
        {
            throw new System.Exception("Earth paint not attached!");
        }
        if (starPaint == null)
        {
            throw new System.Exception("Star paint not attached!");
        }
        if (dollarPaint == null)
        {
            throw new System.Exception("Dollar paint not attached!");
        }
        if (pepePaint == null)
        {
            throw new System.Exception("Pepe paint not attached!");
        }
    }
    /// <summary>
    /// Use this for initialization.
    /// </summary>
	private void Start()
    {
        paintIndex = 0;
    }
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        
    }
    /// <summary>
    /// Move next type of the paint.
    /// </summary>
    public void NextPaintType()
    {
        paintIndex = (++paintIndex) % PAINT_TEMPLATE_COUNT;
        UpdatePaintType();
        UpdatePaintItem();
    }
    /// <summary>
    /// Move previous type of the paint.
    /// </summary>
    public void PrevPaintType()
    {
        paintIndex = --paintIndex;
        if (paintIndex < 0) paintIndex = PAINT_TEMPLATE_COUNT - 1;
        UpdatePaintType();
        UpdatePaintItem();
    }
    /// <summary>
    /// Updates the type of the paint.
    /// </summary>
    private void UpdatePaintType()
    {
        paintManager.currentPaintType = (PaintTemplateType)paintIndex;
    }
    /// <summary>
    /// Updates the paint item.
    /// </summary>
    private void UpdatePaintItem()
    {
        cubePaint.SetActive(false);
        earthPaint.SetActive(false);
        starPaint.SetActive(false);
        dollarPaint.SetActive(false);
        pepePaint.SetActive(false);
        switch (paintManager.currentPaintType)
        {
            case PaintTemplateType.CUBE:
                cubePaint.SetActive(true);
                break;
            case PaintTemplateType.EARTH:
                earthPaint.SetActive(true);
                break;
            case PaintTemplateType.STAR:
                starPaint.SetActive(true);
                break;
            case PaintTemplateType.DOLLAR:
                dollarPaint.SetActive(true);
                break;
            case PaintTemplateType.PEPE:
                pepePaint.SetActive(true);
                break;
        }
    }
}
