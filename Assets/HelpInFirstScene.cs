using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelpInFirstScene : MonoBehaviour
{
    


    [SerializeField] GameObject Panel;
    [SerializeField] GameObject text1;
    [SerializeField] GameObject text2;
    [SerializeField] GameObject text3;
    [SerializeField] GameObject text4;
    [SerializeField] GameObject text5;


    private void Awake()
    {
        RPG.Core.SceneLoader.AddEventListenerLevelChange((e) =>
        {
            Study1Show();
        });

        RPG.Core.FollowCamera.OnCameraRotation += FollowCamera_OnCameraRotation;

        RPG.Core.FollowCamera.OnCameraScale += FollowCamera_OnCameraScale; 
    }

    private void FollowCamera_OnCameraRotation() => EndStudy1();
    private void FollowCamera_OnCameraScale() => EndStudy2();

    private void OnDestroy()
    {
        RPG.Core.FollowCamera.OnCameraRotation -= FollowCamera_OnCameraRotation;
        RPG.Core.FollowCamera.OnCameraScale -= FollowCamera_OnCameraScale;
    }

    private void restTexts()
    {
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
    }

    public void Study1Show()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex !=0) return;
        
        Panel.SetActive(true);
        restTexts();
        text1.SetActive(true);

    }

    private void EndStudy1()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 0) return;
        Panel.SetActive(false);
        IGame.Instance.dataPLayer.playerData.helpIndex = 1;
        Study2();
    }


    public void Study2()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 1) return;
        Panel.SetActive(true);
        restTexts();
        text2.SetActive(true);
    }
    private void EndStudy2()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 1) return;
        Panel.SetActive(false);
        IGame.Instance.dataPLayer.playerData.helpIndex = 2;
    }
    public void Study3()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 2) return;
        Panel.SetActive(true);
        restTexts();
        text3.SetActive(true);
    }

    public void EndStudy3()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 2) return;
        Panel.SetActive(false);
        IGame.Instance.dataPLayer.playerData.helpIndex = 3;
    }
    public void Study4()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 3) return;
        Panel.SetActive(true);
        restTexts();
        text4.SetActive(true);
    }
    public void EndStudy4()
    {
        if (IGame.Instance.dataPLayer.playerData.helpIndex != 3) return;
        Panel.SetActive(false);
        IGame.Instance.dataPLayer.playerData.helpIndex = 4;
    }
    public void Study5()
    {
        //��� ��� �������. ���� ����� �� ����� ����� � ������
        Panel.SetActive(true);
        restTexts();
        text5.SetActive(true);
            IGame.Instance.dataPLayer.playerData.helpIndex = 5;
    }


}