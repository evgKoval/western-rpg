using System.Collections.Generic;
using Codebase.Data;
using Codebase.Enemy;
using Codebase.Infrastructure.Factories;
using Codebase.Services.Progress;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Logic
{
  public class EnemySpawner : MonoBehaviour, ISaveable
  {
    private IGameFactory _gameFactory;
    private IStaticDataService _staticDataService;
    private GameObject _enemy;
    private Death _death;
    private string _id;

    public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService, string id)
    {
      _gameFactory = gameFactory;
      _staticDataService = staticDataService;
      _id = id;
    }

    public void SaveProgress(PlayerProgress progress)
    {
      Dictionary<string, EnemyStateData> enemyData = progress.EnemyData.Dictionary;
      EnemyHealth health = _enemy.GetComponent<EnemyHealth>();

      if (!enemyData.ContainsKey(_id))
        SaveNewEnemyData(enemyData, health);
      else
        UpdateEnemyData(enemyData, health);
    }

    public void LoadProgress(PlayerProgress progress)
    {
      Dictionary<string, EnemyStateData> enemyData = progress.EnemyData.Dictionary;

      if (!enemyData.ContainsKey(_id))
        Spawn(at: transform.position, _staticDataService.Enemy.CurrentHealth, _staticDataService.Enemy.MaxHealth);
      else
        SpawnWithStoredData(enemyData);
    }

    private void SaveNewEnemyData(Dictionary<string, EnemyStateData> enemyData, EnemyHealth health) =>
      enemyData.Add(_id, new EnemyStateData(_enemy.transform.position.AsVectorData(), health.Current, health.Max));

    private void UpdateEnemyData(Dictionary<string, EnemyStateData> enemyData, EnemyHealth health)
    {
      EnemyStateData enemyState = enemyData[_id];

      enemyState.Position = _enemy.transform.position.AsVectorData();
      enemyState.CurrentHealth = health.Current;
      enemyState.MaxHealth = health.Max;
    }

    private void SpawnWithStoredData(Dictionary<string, EnemyStateData> enemyData)
    {
      Vector3 enemyPosition = enemyData[_id].Position.AsUnityVector();
      int currentHealth = enemyData[_id].CurrentHealth;
      int maxHealth = enemyData[_id].MaxHealth;

      Spawn(enemyPosition, currentHealth, maxHealth);
    }

    private void Spawn(Vector3 at, int currentHealth, int maxHealth)
    {
      _enemy = _gameFactory.CreateEnemy(at);
      _gameFactory.CreateWeapon(WeaponId.Axe, _enemy.transform);

      _death = _enemy.GetComponent<Death>();
      _death.Happened += Slay;

      if (currentHealth <= 0)
        _death.Die();

      EnemyHealth health = _enemy.GetComponent<EnemyHealth>();
      health.Construct(currentHealth, maxHealth);
    }

    private void Slay()
    {
      if (_death != null)
        _death.Happened -= Slay;
    }
  }
}