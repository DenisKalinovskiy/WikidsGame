using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowForPlayerManager
{
    

    private SortedDictionary<int, ArrowForPlayer> allArrowForPlayers;

    public SortedDictionary<int, ArrowForPlayer> AllArrowForPlayers { get => allArrowForPlayers; set => allArrowForPlayers = value; }

    public void Init()
    {
        allArrowForPlayers = new SortedDictionary<int, ArrowForPlayer>();
        RPG.Core.SceneLoader.LevelChanged += SceneLoader_LevelChanged;
    }

    private void SceneLoader_LevelChanged(LevelChangeObserver.allScenes obj)
    {
        allArrowForPlayers = new SortedDictionary<int, ArrowForPlayer>();

    }

    public void StartArrow()
    {
        List<ArrowForPlayer> sorted = AllArrowForPlayers.Values.ToList();
        if (sorted.Count > 0)
        {
            sorted[0].gameObject.SetActive(true);
        }
    }

}


public class ArrowForPlayer : MonoBehaviour
{
    [SerializeField]
    public GameObject ArrowSprite;
    public GameObject ArrowImage;

    public int Index;

    private bool trigered = false;

    public void Init()
    {

    }

    private void Awake()
    {
        IGame.Instance.ArrowForPlayerManager.AllArrowForPlayers[Index] = this;
        if (Index != 0) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        trigered = true;
        IGame.Instance.ArrowForPlayerManager.AllArrowForPlayers[Index].gameObject.SetActive(false);
        IGame.Instance.ArrowForPlayerManager.AllArrowForPlayers.Remove(Index);
        IGame.Instance.ArrowForPlayerManager.StartArrow();
    }

    private void Update()
    {
        if (!trigered)
        {
            Vector3 rotate = transform.eulerAngles;
            Vector3 normPos = (transform.position - IGame.Instance.playerController.transform.position).normalized;
            float yAngle = Mathf.Acos(normPos.z) * Mathf.Rad2Deg;
            if (normPos.x < 0)
            {
                yAngle = -yAngle;
            }
            rotate.x = 90;
            rotate.y = yAngle;
            ArrowSprite.transform.rotation = Quaternion.Euler(rotate);

            ArrowSprite.transform.position = IGame.Instance.playerController.transform.position + new Vector3(0, 1, 0) + normPos * 3;
        }

    }

    private void OnDestroy()
    {
        Destroy(ArrowSprite);
        Destroy(ArrowImage);
    }
}
