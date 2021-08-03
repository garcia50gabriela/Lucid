using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shore : MonoBehaviour
{
    public GameObject sandStar;
    private float angle_a;
    private float angle_b;
    // Start is called before the first frame update
    void Start()
    {
        //close angle of water
        angle_a = gameObject.transform.localRotation.eulerAngles.x;
        // deep angle of water
        angle_b = 90f - angle_a;
        StartCoroutine("PopulateSandStars");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PopulateSandStars()
    {
        while (true) 
        {
            var random_z = Random.Range(-9f, -6f);
            // calculate depth based on z
            var y_depth = (random_z / Mathf.Sin(angle_b * Mathf.Deg2Rad)) * Mathf.Sin(angle_a * Mathf.Deg2Rad);
            // account for scale of shore
            y_depth = (sandStar.transform.position.y - y_depth) + gameObject.transform.localScale.y / 2f;
            var pos = new Vector3(Random.Range(-4f, 4f), y_depth, random_z);
            var new_star = Instantiate(sandStar, pos, sandStar.transform.rotation);
            new_star.GetComponent<SandStar>().dontDestroy = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
