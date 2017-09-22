using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Assertions;
using Common.PropertyDrawers;

public class HandMenu : MonoBehaviour, ICarouselReceiver
{
    [Serializable]
    private class MenuObject
    {
        public string name = string.Empty;
        public int amount = 0;
        [HideInInspector] public int remaining = 0;
        public GameObject visualRepresentation = null;
        public GameObject prefab = null;
    }

    [SerializeField] private List<MenuObject> objects = new List<MenuObject>();
    [SerializeField] private int currentObjectIndex = 0;

    private MenuObject CurrentObject { get { return objects[currentObjectIndex]; } }
    private string CurrentObjectLabel { get { return string.Format( "{0} {1}/{2}", CurrentObject.name, CurrentObject.remaining, CurrentObject.amount ); } }

    [SerializeField] private TextMeshProUGUI objectLabel = null;

#region [Collectible placement in VR helper] // Remove for the final build
#if PLACING_STARS
    [SerializeField] [TagSelector] private string collectibleTag = string.Empty;
    [SerializeField] private List<GameObject> collectibles = new List<GameObject>();

    private int currentCollectible = 0;
#endif
#endregion

    public OnCarouselSetActive OnCarouselSetActive { get { return SetMenuActive; } }
    public OnCarouselSelection OnCarouselSelection { get { return Select; } }
    public OnCarouselMoveLeft OnCarouselMoveLeft { get { return MoveLeft; } }
    public OnCarouselMoveRight OnCarouselMoveRight { get { return MoveRight; } }

    private void Awake()
    {
        Assert.IsTrue( objects.Count > 0, "Menu list is empty!" );

        foreach ( MenuObject menuObject in objects )
        {
            Assert.IsTrue( menuObject.name != string.Empty, "There is an object without name in the menu!" );
            Assert.IsNotNull( menuObject.visualRepresentation, string.Format( "{0} in menu doesn't have a visual representation assigned!", menuObject.name ) );
            Assert.IsNotNull( menuObject.prefab, string.Format( "{0} in menu doesn't have a prefab assigned!", menuObject.name ) );

            menuObject.visualRepresentation.SetActive( false );
            menuObject.remaining = menuObject.amount;
        }

        Assert.IsNotNull( objectLabel, "The menu's label container is not assigned!" );
        objectLabel.text = string.Empty;
    }

#region [ICarouselReceiver]
    private void SetMenuActive( bool active )
    {
        CurrentObject.visualRepresentation.SetActive( active );

        objectLabel.text = ( active ? CurrentObjectLabel : string.Empty );
    }

    private void Select()
    {
#region [Collectible placement in VR helper] // Remove for the final build
#if PLACING_STARS
        if ( CurrentObject.prefab.CompareTag(collectibleTag) )
        {
            GameObject collectible = collectibles[currentCollectible];
            collectible.transform.position = CurrentObject.visualRepresentation.transform.position;
            currentCollectible = ( currentCollectible + 1 ) % collectibles.Count;
            return;
        }
#endif
#endregion

        if ( CurrentObject.remaining > 0 )
        {
            Instantiate( CurrentObject.prefab,
                CurrentObject.visualRepresentation.transform.position + transform.forward * 0.5f,
                CurrentObject.visualRepresentation.transform.rotation );

            CurrentObject.remaining--;
            objectLabel.text = CurrentObjectLabel;
        }
    }

    private void MoveLeft()
    {
        CurrentObject.visualRepresentation.SetActive( false );
        currentObjectIndex--;

        if ( currentObjectIndex < 0 )
        {
            currentObjectIndex = objects.Count - 1;
        }
        CurrentObject.visualRepresentation.SetActive( true );
        objectLabel.text = CurrentObjectLabel;
    }

    private void MoveRight()
    {
        CurrentObject.visualRepresentation.SetActive( false );
        currentObjectIndex++;

        if ( currentObjectIndex == objects.Count )
        {
            currentObjectIndex = 0;
        }
        CurrentObject.visualRepresentation.SetActive( true );
        objectLabel.text = CurrentObjectLabel;
    }
#endregion
}
