using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public GameObject HeartPrefab;
    public GameObject Player;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable()
    {
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        PlayerHealth.OnPlayerDamage += DrawHearts;
    }

    public void Start(){
        DrawHearts();
    }

    public void DrawHearts(){
        ClearHearts();

        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();

        float remainder = playerHealth.MaxHealth % 2;
        int hearts = (int)(playerHealth.MaxHealth / 2 + remainder);

        for(int i = 0; i < hearts; i++){
            int heartHealth = playerHealth.Health - (i * 2);
            if(heartHealth > 1){
                CreateHeart(HeartState.Full);
            }
            else if(heartHealth == 1){
                CreateHeart(HeartState.Half);
            }
            else{
                CreateHeart(HeartState.Empty);
            }
        }
    }

    public void CreateHeart(HeartState state){
        GameObject newHeart = Instantiate(HeartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(state);
        hearts.Add(heartComponent);
    }

    public void ClearHearts(){
        foreach(Transform t in transform){
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();     
    }
}
