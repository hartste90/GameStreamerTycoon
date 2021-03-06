﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameHUD : MonoBehaviour
{
    public GameObject LiveWindow;
    public Text AmountOfCash, NumOfFollowers, NumOfViewers, NumOfLikes, PlayerName;
    public Button LiveButton, MarketButton;

    public RectTransform LiveProgressBar;
    Game CurrentGame;

    void Start( )
    {
        LiveWindow.SetActive( false );
    }

    void FixedUpdate( )
    {
        PlayerName.text = GameManager.Instance.StreamerName;
        AmountOfCash.text = "$" + ( Mathf.RoundToInt( GameManager.Instance.Money ) ).ToString( );
        NumOfFollowers.text = GameManager.Instance.Followers.ToString( );

        LiveButton.interactable = !GameManager.Instance.IsLive;
        MarketButton.interactable = !GameManager.Instance.IsLive;
        
        if ( GameManager.Instance.IsLive )
        {
            NumOfViewers.text = GameManager.Instance.Viewers.ToString( );
            NumOfLikes.text = GameManager.Instance.Likes.ToString( );

            if ( CurrentGame.Equals( default( Game ) ) ) 
                CurrentGame = GameManager.Instance.CurrentGame;
            else if ( LiveWindow.transform.GetChild( 0 ).GetComponent<Text>( ).text != CurrentGame.Title )
            {
                LiveWindow.transform.GetChild( 0 ).GetComponent<Text>( ).text = CurrentGame.Title; //Game title
                LiveWindow.transform.GetChild( 1 ).GetComponent<Text>( ).text = CurrentGame.Developer; //Developer
                LiveWindow.transform.GetChild( 2 ).GetChild( 1 ).GetComponent<RectTransform>( ).SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, GameManager.RatingToWidth( CurrentGame.CommunityRating, 300 ) ); //Community rating bar
                LiveWindow.transform.GetChild( 3 ).GetChild( 1 ).GetComponent<RectTransform>( ).SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, GameManager.RatingToWidth( CurrentGame.CriticRating, 300 ) ); //Critics rating bar
                LiveWindow.transform.GetChild( 4 ).GetChild( 1 ).GetComponent<RectTransform>( ).SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, GameManager.RatingToWidth( CurrentGame.StreamerRating, 300 ) ); //Streamer rating bar
            }

            //Update progress bar
            float progressPerc = GameManager.Instance.CurrentStreamTime / GameManager.Instance.TotalStreamTime;
            float barWidth = 1305f * progressPerc;
            LiveProgressBar.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, barWidth );
        }
        else
        {
            if ( LiveWindow.activeInHierarchy )
                LiveWindow.SetActive( false );
        }
    }

    public void GoLive( )
    {
        GameManager.Instance.ToggleLive( );

        LiveWindow.SetActive( GameManager.Instance.IsLive );

        if ( GameManager.Instance.IsLive )
        {
}
    }
}