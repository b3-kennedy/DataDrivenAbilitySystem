using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Abilities/Ability")]
public class Ability: ScriptableObject
{

    protected float cooldown;
    protected float cooldownEndTime;
    GameObject caster;

    float damage;
    GameObject icon;

    float castRange;

    LayerMask mask;

    protected LineRenderer lineRendererPrefab;

    LineRenderer lineRenderer;

    int index;

    bool isTargeting;

    List<SpecialValue> specialValues = new List<SpecialValue>();

    bool isActive = true;

    public virtual void Start()
    {
        StopCooldown();
    }

    public void SetSpecialValues(List<SpecialValue> values)
    {
        specialValues = values;
    }

    public string GetSpecialValue(string name)
    {
        foreach (var value in specialValues)
        {
            if(value.name == name)
            {
                return value.value;
            }
        }
        return null;
    }

    public float GetSpecialFloatValue(string name)
    {
        foreach (var value in specialValues)
        {
            if(value.name == name)
            {
                return float.Parse(value.value);
            }
        }
        return -1;
    }

    public int GetSpecialIntValue(string name)
    {
        foreach (var value in specialValues)
        {
            if(value.name == name)
            {
                return int.Parse(value.value);
            }
        }
        return -1;
    }

    public void SetActive(bool value)
    {
        isActive = value;
        icon.SetActive(value);
    }

    public bool GetActive()
    {
        return isActive;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetLineRenderer(LineRenderer lr)
    {
        lineRendererPrefab = lr;
    }
    public void SetCastRange(float range)
    {
        castRange = range;
    }

    public void SetLayerMask(LayerMask lm)
    {
        mask = lm;
    }

    public float GetCastRange()
    {
        return castRange;
    }

    public virtual Vector3 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, mask))
        {
            return Action.GetCorrectVector(hit.point);
        }
        return Vector3.zero;
        
    }

    public virtual Vector3 GetRawCursorPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetIcon(GameObject img)
    {
        icon = img;
    }

    public GameObject GetIcon()
    {
        return icon;
    }

    public void SetIndex(int value)
    {
        index = value;
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetCooldown(float cd)
    {
        cooldown = cd;
    }

    public bool IsOnCooldown()
    {
        return Time.time < cooldownEndTime;
    }

    public void StartCooldown()
    {
        cooldownEndTime = Time.time + cooldown;
    }

    public void StopMove()
    {
        Move m = caster.GetComponent<Move>();
        m.StopMove();
    }

    public void StopCooldown()
    {
        cooldownEndTime = Time.time;
    }

    public void SetCaster(GameObject hero)
    {
        caster = hero;
    }

    public virtual GameObject GetCaster()
    {
        return caster;
    }

    public float GetCooldownRemaining()
    {
        return Mathf.Max(0f, cooldownEndTime - Time.time);
    }

    public float GetCooldownProgress()
    {
        if (cooldown <= 0f) return 0f;
        return Mathf.Clamp01(1f - GetCooldownRemaining() / cooldown);
    }

    public bool IsTargeting()
    {
        return isTargeting;
    }

    public void SetTargeting(bool value)
    {
        isTargeting = value;
    }

    public virtual void OnStartCast()
    {
        if(this is not ImmediateAbility)
        {
            isTargeting = true;

            lineRenderer = Instantiate(lineRendererPrefab);
            
            
        }
        else
        {
            Cast();
        }
    }

    public virtual void Update()
    {
        if(isTargeting && !IsOnCooldown())
        {
            DrawCircle(lineRenderer,caster.transform.position,castRange);
        }
    }

    public virtual void DrawCircle(LineRenderer lr,Vector3 center,float radius, int points = 100) 
    {
            lr.enabled = true;
            lr.positionCount = points + 1;
            for (int i = 0; i <= points; i++) {
                float angle = i * 2 * Mathf.PI / points;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                // Add center position offset here
                lr.SetPosition(i, center + new Vector3(x, 0f, z));
        }
    }

    public bool IsWithinRange()
    {

        if(castRange == 0) //cast range 0 for global abilities/ abilities that dont rely on cast range
        {
            return true;
        }

        Vector3 targetPos = GetCursorPosition();
        Vector3 casterPos = Action.GetCorrectVector(caster.transform.position);
        float localCastRange = castRange;

        if(targetPos.z > casterPos.z)
        {
            localCastRange += 2;
        }



        float distance = Vector3.Distance(casterPos, targetPos);

        if (distance > localCastRange)
        {
            Debug.Log("Target out of range!");
            return false;
        }
        return true;
    }

    public Abilities GetAbilityHolder()
    {
        return caster.GetComponent<Abilities>();
    }


    public void Cast()
    {

        if(IsOnCooldown()) return;
        if(!IsWithinRange()) return;




        PerformCast();
        DisableLineRenderer();
        StartCooldown();

    }

    public virtual void DisableLineRenderer()
    {
        if(lineRenderer)
        {
            Destroy(lineRenderer.gameObject);
        }
        
    }

    public virtual void PerformCast()
    {

    }
}
