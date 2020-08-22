﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleScroller : MonoBehaviour {
    [SerializeField] private GameObject[] obstacles;

    private GameManager gm;
    private Coroutine coroutine;

    private void Start() {
        gm = GameManager.instance;
    }

    private void Update() {
        if (coroutine == null && gm.startPlaying && !gm.gameFinished) {
            coroutine = StartCoroutine(CreateObstacle());
        }
    }

    private void GenerateObstacle() {
        int index = Random.Range(0, obstacles.Length);
        GameObject random = obstacles[index];
        Vector3 pos = transform.position;
        if (random.CompareTag("Grass")) {
            pos -= new Vector3(0f, 0.12f, 0f);
        }
        Instantiate(random, pos, Quaternion.identity);
    }

    private IEnumerator CreateObstacle() {
        if (gm.playerHit) {
            yield return new WaitForSeconds(gm.spawnRate / gm.currentbeatTempo);
        }
        GenerateObstacle();
        yield return new WaitForSeconds(gm.spawnRate / gm.currentbeatTempo);
        coroutine = null;
    }
}