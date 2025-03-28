using Unity.Netcode;
using UnityEngine;

public class FireBreath : Skill, IPrimarySkill  //TODO: canUse ve isCasting kontrol ve kullanýmý
{
    public GameObject fireBreathPrefab;

    public Transform fireSpawnPoint;

    public float fireRange = 10f;

    public float damage = 10f;


    private Mage mage;

    private FireDamage fireDamage;

    private GameObject currentFire;

    private bool isCastingPrimarySkill;


    private void Awake()
    {
        fireDamage = fireBreathPrefab.GetComponent<FireDamage>();
        mage = GetComponent<Mage>();
    }


    private void Update()
    {
        if(isCastingPrimarySkill)
        {
            AimFireBreath();
        }
    }


    public void CastPrimarySkill()
    {
        if (isCastingPrimarySkill)
        {
            StopFire();
        }
        else
        {
            StartFire();
        }
        

        CharacterAnimation.Instance.SetBool(AnimationKey.PRIMARY_SKILL,isCastingPrimarySkill);
    }


    void AimFireBreath()
    {
        currentFire.transform.SetPositionAndRotation(fireSpawnPoint.position, transform.rotation);
    }


    void StartFire()
    {
        if (currentFire == null)
        {
            isCastingPrimarySkill = true;
            currentFire = Instantiate(fireBreathPrefab, fireSpawnPoint.position, fireSpawnPoint.rotation);
        }
    }

    void StopFire()
    {
        isCastingPrimarySkill = false;
        Destroy(currentFire);
        StartCoroutine(Cooldown());
    }

    protected override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }
}
