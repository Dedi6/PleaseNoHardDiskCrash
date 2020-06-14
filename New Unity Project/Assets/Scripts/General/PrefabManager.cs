using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    [System.Serializable]
    public struct NamedVFX
    {
        public ListOfVFX vfxName;
        public GameObject vfxGameObject;
    }

    public NamedVFX[] arrayOfVFX;
    public enum ListOfVFX
    {
        BulletDissapear,
        PlayerHit,
        EnemyHit,
        LightningBolt,

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    public void PlayVFX(ListOfVFX name, Vector2 position)
    {
        GameObject vfx = Instantiate(FindVFX(name), position, Quaternion.identity);
    }

    private GameObject FindVFX(ListOfVFX name)
    {
        foreach (NamedVFX currentVFX in arrayOfVFX)
        {
            if (currentVFX.vfxName == name)
                return currentVFX.vfxGameObject;
        }
        return null;
    }
}
