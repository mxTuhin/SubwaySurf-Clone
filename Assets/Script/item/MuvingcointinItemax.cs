using UnityEngine;
using System.Collections;

public class MuvingcointinItemax : MonoBehaviour {
    public Transform dic;
    bool delay;
	// Use this for initialization
 
	public void GetDich(Transform tranformm)
    {
        dic = tranformm;
    }
	// Update is called once per frame
	void Update () {
        if (dic!= null)
        {
            transform.position = Vector3.Lerp(transform.position, dic.position, 15 * Time.deltaTime);
            if (Mathf.Round(transform.position.x) ==Mathf.Round(dic.position.x)&&
                Mathf.Round(transform.position.y) == Mathf.Round(dic.position.y))
            {
             //   Debug.Log("raaaaaa");
                Slideraxspeed.instance.Muvingojecttotranform(this.gameObject);
             
            }
        }
    }
}
