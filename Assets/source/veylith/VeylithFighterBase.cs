using UnityEngine;

public class VeylithFighterBase : MonoBehaviour, IDamagable {
    [Header("General")]
    [SerializeField] int _health;
    [SerializeField] float _attackTime;

    [Header("Movement")]
    [SerializeField] float _maxHorizontalVelocity;
    [SerializeField] float _maxHorizontalRepositionVelocityMultiplier;
    [SerializeField] float _targetHorizontalDistance;
    [SerializeField] float _verticalBounceVelocity;
    [SerializeField] float _verticalBounceLimit;

    [Header("Animations")]
    [SerializeField] Animator _shipAnim;
    [SerializeField] Animator _vfxAnim;

    GameObject _player = null;
    ProjectilePool _projPool = null;
    bool _bounceDir = false;
    bool _exploding = false;
    float _attackTimer = .0f;

    public void Start() {
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

        if (newPos.y >=  _player.transform.position.y + _verticalBounceLimit && _bounceDir
        ||  newPos.y <= _player.transform.position.y - _verticalBounceLimit && !_bounceDir
        ) _bounceDir ^= true;

        if (_isPlayerInFront()) {
            if (newPos.x - _player.transform.position.x > _targetHorizontalDistance)
                newPos.x -= _maxHorizontalVelocity * Time.deltaTime;
            else if (newPos.x - _player.transform.position.x < _targetHorizontalDistance)
                newPos.x += _maxHorizontalVelocity * Time.deltaTime;

            newPos.y += (_bounceDir ? 1.0f : -1.0f) * _verticalBounceVelocity * Time.deltaTime;
        }
        else
            newPos.x += _maxHorizontalVelocity * _maxHorizontalRepositionVelocityMultiplier * Time.deltaTime;

        transform.position = newPos;

        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackTime) {
            _projPool.spawnLaserLime(transform.position, Vector2.left, "Player");
            _attackTimer = .0f;
        }
    }

    public void damage(int value) {
        _health -= value;
        if (_health > 0) return;

        _shipAnim.SetBool("explode", true);
        _vfxAnim.SetBool("explode", true);
        _exploding = true;
    }

    public void heal(int value)
        => _health += value;

    bool _isPlayerInFront()
        => transform.position.x > _player.transform.position.x;
}