using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelScript : MonoBehaviour
{
    private Animator animator;
    private int state;
    public GameObject InfoPanel;   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClickAnim() {
        animator.SetTrigger("active");
    }
    public void onClickPanel()
    {
        InfoPanel.active = true;
    }
}
