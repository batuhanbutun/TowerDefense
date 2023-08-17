using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedEnemyCard : MonoBehaviour
{
   [SerializeField] private GameObject rawImage;
   [SerializeField] private GameObject rawImageCamera;
  //[SerializeField] private GameObject lockedSpiderCard,lockedGolemCard,lockedBeeCard,lockedMagmaCard,lockedWolfCard;
  //[SerializeField] private GameObject lockedSpider,lockedGolem,lockedBee,lockedMagma,lockedWolf;
   
   [System.Serializable]
   public class LockedEnemy
   {
      public string tag;
      public GameObject lockedEnemyObject;
      public GameObject lockedEnemyCard;
   }
   public List<LockedEnemy> LockedEnemies;
   
   public static LockedEnemyCard Instance;
   
   private void Awake()
   {
       Instance = this;
   }

   public void OpenCard(string tag)
   {
       foreach (var lockedEnemies in LockedEnemies)
       {
           if (lockedEnemies.tag.Equals(tag))
           {
               lockedEnemies.lockedEnemyObject.SetActive(true);
               lockedEnemies.lockedEnemyCard.SetActive(true);
               rawImage.SetActive(true);
               rawImageCamera.SetActive(true);
           }
       }
   }
}
