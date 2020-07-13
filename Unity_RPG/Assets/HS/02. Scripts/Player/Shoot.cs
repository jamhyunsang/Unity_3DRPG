using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject firePos;
    public GameObject arrowFactory;
    public GameObject skillArrow;
    public GameObject skillEffect;

    public void Shooting()
    {
        var Arrow = ArrowObjectPool.GetObject(firePos);
    }

    public void Skill1()
    {
        var ArrowSkill = ArrowObjectPoolSkill.GetObject(firePos);
    }

    public void SkillEffectOn()
    {
        skillEffect.SetActive(true);
    }
    public void SkillEffectOff()
    {
        skillEffect.SetActive(false);
    }
}
