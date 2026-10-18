using UnityEngine;

public class VeylithFighter : MonoBehaviour, IDamagable {
    [SerializeField] VeylithFighterData _data = null;
    [SerializeField] Animator _shipAnim = null;
    [SerializeField] Animator _vfxAnim = null;

    GameObject _player = null;
    ProjectilePool _projPool = null;
    bool _bounceDir = false;
    bool _exploding = false;
    float _attackTimer = .0f;

    public void Start() {
        if (_data == null
        ||  _shipAnim == null
        ||  _vfxAnim == null
        ) Debug.LogWarning("VeylithFighter has uninitialized 'SerializeField's");

        _player = GameObject.FindWithTag("Player");
        if (_player == null)
            Debug.LogWarning("VeylithFighterBase could not find the GameObject with tag 'Player'");

        _projPool = FindAnyObjectByType<ProjectilePool>();
        if (_projPool == null)
            Debug.LogWarning("VeylithFighterBase could not find object of type 'ProjectilePool'");
    }

    public void Update() {
        if (_exploding
        &&  _shipAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f
        &&  _vfxAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f
        ) Destroy(gameObject);

        Vector2 newPos = transform.position;

        if (newPos.y >=  _player.transform.position.y + _data.verticalBounceLimit && _bounceDir
        ||  newPos.y <= _player.transform.position.y - _data.verticalBounceLimit && !_bounceDir
        ) _bounceDir ^= true;

        if (_isPlayerInFront()) {
            if (newPos.x - _player.transform.position.x > _data.targetHorizontalDistance)
                newPos.x -= _data.velocity.x * Time.deltaTime;
            else if (newPos.x - _player.transform.position.x < _data.targetHorizontalDistance)
                newPos.x += _data.velocity.x * Time.deltaTime;

            newPos.y += (_bounceDir ? 1.0f : -1.0f) * _data.velocity.y * Time.deltaTime;
        }
        else
            newPos.x += _data.velocity.x * _data.horizontalVelocityRepositionMultiplier * Time.deltaTime;

        transform.position = newPos;

        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _data.attackTime) {
            _projPool.spawnLaserLime(transform.position, Vector2.left, "Player");
            _attackTimer = .0f;
        }
    }

    public void damage(int value) {
        _data.health -= value;
        if (_data.health > 0) return;

        _shipAnim.SetBool("explode", true);
        _vfxAnim.SetBool("explode", true);
        _exploding = true;
    }

    public void heal(int value)
        => _data.health += value;

    bool _isPlayerInFront()
        => transform.position.x > _player.transform.position.x;
}