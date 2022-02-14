using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Enemy enemy;
    public Image[] StatusBars;
    private PlayerController pc;

    private void Start()
    {
        pc = Object.FindObjectOfType<PlayerController>();
        foreach (Image image in StatusBars)
        {
            image.fillAmount = 0;
            image.gameObject.transform.parent.gameObject.GetComponent<Image>().fillAmount = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pc.gameObject.transform.position);
        for (int i = 0; i < 4; i++)
        {
            Status temp = enemy.statuses[i];
            StatusBars[i].fillAmount = temp.statusAmount / temp.statusMax;
            if (temp.statusAmount / temp.statusMax > .1)
            {
                StatusBars[i].gameObject.transform.parent.gameObject.GetComponent<Image>().fillAmount = 1;
            }
        }
    }
}
