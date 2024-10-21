using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarsManager : MonoBehaviour
{
    public static EnemyHealthBarsManager Instance;

    [SerializeField]
    private EnemyHealthBar _healthBarPrefab;

    private List<EnemyHealthBar> _healthBarList = new List<EnemyHealthBar>();

    private Camera _cam;

    private void Awake()
    {
        Instance = this;
        _cam = Camera.main;
    }

    
    public void AddHealthBar(Health health)
    {
        EnemyHealthBar current = Instantiate(_healthBarPrefab, transform);
        current.SetUp(health);
        current.OnDie += RemoveHealthBar;
        _healthBarList.Add(current);
    }


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _healthBarList.Count; i++)
        {
            EnemyHealthBar current = _healthBarList[i];
            current.transform.position = _cam.WorldToScreenPoint((Vector2)current.targetTransform.position + current._offset);
        }
    }

    private void RemoveHealthBar(EnemyHealthBar bar)
    {
        bar.OnDie -= RemoveHealthBar;
        _healthBarList.Remove(bar);
        Destroy(bar.gameObject);
    }
}
