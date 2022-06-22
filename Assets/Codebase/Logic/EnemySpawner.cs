using System.Collections.Generic;
using Codebase.Data;
using Codebase.Infrastructure.Factories;
using Codebase.Services.Progress;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Logic
{
  public class EnemySpawner : MonoBehaviour, ISaveable
  {
    private IGameFactory _gameFactory;
    private Death _death;
    private string _id;
    private bool _slain;

    public void Construct(IGameFactory gameFactory, string id)
    {
      _gameFactory = gameFactory;
      _id = id;
    }

    public void SaveProgress(PlayerProgress progress)
    {
      List<string> slainSpawners = progress.EnemyData.SlainSpawners;

      if (_slain && !slainSpawners.Contains(_id))
        slainSpawners.Add(_id);
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (!progress.EnemyData.SlainSpawners.Contains(_id))
        Spawn();
    }

    private void Spawn()
    {
      GameObject enemy = _gameFactory.CreateEnemy(transform.position);
      _gameFactory.CreateWeapon(WeaponId.Axe, enemy.transform);

      _death = enemy.GetComponent<Death>();
      _death.Happened += Slay;
    }

    private void Slay()
    {
      if (_death != null)
        _death.Happened -= Slay;

      _slain = true;
    }
  }
}