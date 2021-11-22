using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    public bool male;
    public bool isSword;
    [SerializeField] GameObject male_root;
    [SerializeField] GameObject female_root;
    public int head;
    public int hair;
    public int torse;
    public int leg;
    public int sword;
    [SerializeField] GameObject[] male_heads;
    [SerializeField] GameObject[] male_hairs;
    [SerializeField] GameObject[] male_torses;
    [SerializeField] GameObject[] male_legs;
    [SerializeField] GameObject[] male_swords;
    [SerializeField] GameObject[] female_heads;
    [SerializeField] GameObject[] female_hairs;
    [SerializeField] GameObject[] female_torses;
    [SerializeField] GameObject[] female_legs;
    [SerializeField] GameObject[] female_swords;

    private void Start()
    {
        Custom_Changed();
    }

    public void Custom_Changed()
    {

        male_root.SetActive(false);
        female_root.SetActive(false);

        for (int i = 0; i < male_heads.Length; i++) male_heads[i].SetActive(false);
        for (int i = 0; i < male_hairs.Length; i++) male_hairs[i].SetActive(false);
        for (int i = 0; i < male_torses.Length; i++) male_torses[i].SetActive(false);
        for (int i = 0; i < male_legs.Length; i++) male_legs[i].SetActive(false);
        for (int i = 0; i < male_swords.Length; i++) male_swords[i].SetActive(false);

        for (int i = 0; i < female_heads.Length; i++) female_heads[i].SetActive(false);
        for (int i = 0; i < female_hairs.Length; i++) female_hairs[i].SetActive(false);
        for (int i = 0; i < female_torses.Length; i++) female_torses[i].SetActive(false);
        for (int i = 0; i < female_legs.Length; i++) female_legs[i].SetActive(false);
        for (int i = 0; i < female_swords.Length; i++) female_swords[i].SetActive(false);

        if (male)
        {
            //male
            male_root.SetActive(true);
            male_heads[head].SetActive(true);
            male_hairs[hair].SetActive(true);
            male_torses[torse].SetActive(true);
            male_legs[leg].SetActive(true);
            if (isSword) male_swords[sword].SetActive(true);
        }
        else
        {
            //female
            female_root.SetActive(true);
            female_heads[head].SetActive(true);
            female_hairs[hair].SetActive(true);
            female_torses[torse].SetActive(true);
            female_legs[leg].SetActive(true);
            if (isSword) female_swords[sword].SetActive(true);
        }
    }
}
