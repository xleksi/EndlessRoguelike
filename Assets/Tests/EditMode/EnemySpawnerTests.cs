using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class EnemySpawnerTests
{
    [Test]
    public void TestEnemySpawnerInitialization()
    {
        GameObject spawnerObject = new GameObject();
        EnemySpawner spawner = spawnerObject.AddComponent<EnemySpawner>();

        GameObject playerObject = new GameObject();
        Transform playerTransform = playerObject.transform;
        spawner.player = playerTransform;

        spawner.InitializeForTesting();

        Assert.NotNull(spawner);
    }
    
    [Test]
    public void TestEnemySpawnerInitialization_PlayerNotNull()
    {
        GameObject spawnerObject = new GameObject();
        EnemySpawner spawner = spawnerObject.AddComponent<EnemySpawner>();

        GameObject playerObject = new GameObject();
        Transform playerTransform = playerObject.transform;

        spawner.player = playerTransform;
        spawner.InitializeForTesting();

        Assert.NotNull(spawner.cachedPlayerTransform);
    }

    [Test]
    public void TestEnemySpawnerInitialization_PlayerNull()
    {
        GameObject spawnerObject = new GameObject();
        EnemySpawner spawner = spawnerObject.AddComponent<EnemySpawner>();

        LogAssert.Expect(LogType.Error, "Player transform not assigned in EnemySpawner."); // Expect error message
        spawner.player = null;
        spawner.InitializeForTesting();

        Assert.IsNull(spawner.cachedPlayerTransform);
        Assert.IsFalse(spawner.enabled);
    }
}