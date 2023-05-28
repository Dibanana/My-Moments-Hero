using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private CamSwitch Switch;
    [SerializeField] protected FullSceneManager FadeManage;
    [SerializeField]protected bool IsNextArea = false; //Automatically goes to the next area
    [SerializeField] protected bool IsCutscene = false;
    [SerializeField] protected int ChooseNextArea; //Manually decide which area to go to next
    private void Awake()
    {
        Switch = (CamSwitch)FindObjectOfType(typeof(CamSwitch));
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(IsCutscene == true)
            {
                FadeManage.FadeBack(false);
                Debug.Log("Calling for Fade");
                StartCoroutine(FadeAway());
                return;
            }
            
            if (IsNextArea == true)
                    {
                        Debug.Log("Switching Camera");
                        Switch.NextCam();
                        Destroy(gameObject);
                    }
                    else
                    {
                        Switch.SetCam(ChooseNextArea);
                        
                    }
        }
    }
    private IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("GameManager").GetComponent<InteractionManager>().Interact();
        
    }
}
