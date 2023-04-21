using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

// To replace old BaseProjectile once completed
public class BaseProjectile : MonoBehaviour
{
    // Tags that the bullet will check collision with
    [SerializeField] List<string> targetTags; 

    [SerializeField] public List<BaseStatusEffect> effects;

    public UnityEvent onCollision;

    /* Since projectile systems deal with how projectiles
     * act, with projectiles just being payloads for effects
     * and in charge of collision events, we don't get to
     * modify the properties here
     */
    private ProjectileProperties properties;

    private GameObject parent;
    private Vector2 direction;
    private ProjectileSystem projectileSysComponent;
    private bool hasCollided = false;
    private void Start()
    {
        projectileSysComponent = GetComponent<ProjectileSystem>();
    }

    // Will be added once subparams within arrays can be displayed
    public void Init(GameObject parent, float lifeTime, float damage, float speed, float scaleMod, bool dieOnCollision, GameObject target, Vector2 direction)
    {
        this.parent = parent;
        properties.target = target;

        properties.lifeTime = lifeTime;
        properties.damage = damage;
        properties.speed = speed;
        properties.scaleModifier = scaleMod;
        properties.dieOnCollision = dieOnCollision;

        // Update the scale
        gameObject.transform.localScale *= scaleMod;

        this.direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
        properties.lifeTime -= Time.deltaTime;

        if (properties.lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        Vector2 delta = Time.deltaTime * properties.speed * direction;
        transform.position += new Vector3(delta.x, delta.y, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCollided == true) { return; }
        if (!checkCollisionTags(collision))
            return;

        // pass target information to projectileSystem (if there is one)
        if (projectileSysComponent != null)
        {
            projectileSysComponent.setTarget(properties.target);
        }

        onCollision.Invoke();

        if (properties.dieOnCollision)
        {
            Destroy(gameObject);
            hasCollided = true;
        }

        //collision.GetComponent<HealthComponent>().DamageStat(Stats.HEALTH, damage);
        StatusSystem statSys = collision.GetComponent<StatusSystem>();
        if (!statSys)
            return;

        foreach (BaseStatusEffect effect in effects)
        {
            if (collision.gameObject == parent && !effect.isOwnerInflicting)
                continue;

            statSys.ApplyEffect(effect);
        }
    }

    private bool checkCollisionTags(Collider2D collision)
    {
        // Check to see if collision contains a targeted tag
        return targetTags
                .Select(tag => collision.CompareTag(tag))
                .Aggregate(false, (acc, tag) => acc || tag);
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}