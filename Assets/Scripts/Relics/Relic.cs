using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : MonoBehaviour
{
    [SerializeField] RelicObject relicObject;
    public RelicObject RelicObject => relicObject;
    [SerializeField] Sprite noRelicSprite;
    [SerializeField] Image relicImage;

    public void DoAwake()
    {
        if(relicObject != null)
        {
            Player.Instance.SetHasRelic(true);
            relicImage.sprite = relicObject.RelicTexture;

            relicObject.Behavior.OnBattleBegin(relicObject);
            relicObject.Behavior.OnRelicEquip(relicObject);
        }
        else
        {
            Player.Instance.SetHasRelic(false);
            relicImage.sprite = noRelicSprite;
        }
    }

    private void InitRelic(RelicObject relic)
    {
        if(relicObject == null)
        {
            relicObject = relic;

            Player.Instance.SetHasRelic(true);

            relicImage.sprite = relicObject.RelicTexture;

            relicObject.Behavior.OnRelicEquip(relicObject);
        }
    }

    private void RemoveRelic()
    {
        relicObject.Behavior.OnRelicUnEquip(relicObject);

        relicObject = null;

        Player.Instance.SetHasRelic(false);

        relicImage.sprite = noRelicSprite;
    }

    public void CreateRelic(RelicObject relic)
    {
        if(relicObject != null)
        {
            RemoveRelic();
        }
        
        InitRelic(relic);
    }

    public void SetRelicObject(RelicObject _relicObject)
    {
        relicObject = _relicObject;
    }
}
