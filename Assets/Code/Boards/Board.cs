using System;
using System.Collections.Generic;
using Code.Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Boards
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [Header("Prefabs")]
        [SerializeField] private GameObject _circlePrefab;
        [SerializeField] private BetField _greenBetFieldPrefab;
        [SerializeField] private BetField _orangeBetFieldPrefab;
        [SerializeField] private BetField _redBetFieldPrefab;
        [SerializeField] private Circle _greenCirclePrefab;
        [SerializeField] private Circle _orangeCirclePrefab;
        [SerializeField] private Circle _redCirclePrefab;

        private const sbyte YOffset = -4;
        private const float GreenYOffset = -1;
        private const float OrangeYOffset = -1.6f;
        private const float RedYOffset = -2.2f;
        private const byte Row12 = 12;
        private const byte Row14 = 14;
        private const byte Row16 = 16;
        
        private readonly List<GameObject> _cash = new List<GameObject>();
        private readonly List<GameObject> _topCircles = new List<GameObject>();
        private Config _config;
        private IBet _bet;
        private ICurrency _currency;

        [Inject]
        private void Construct(Config config, IBet bet, ICurrency currency)
        {
            _config = config;
            _bet = bet;
            _currency = currency;
        }
        private void Start()
        {
            _dropdown.onValueChanged.AddListener(ChangeBoard);
            _dropdown.value = 1;
        }

        public void SpawnGreenCircle()
        {
            SpawnCircle(_greenCirclePrefab);
        }

        public void SpawnOrangeCircle()
        {
            SpawnCircle(_orangeCirclePrefab);
        }

        public void SpawnRedCircle()
        {
            SpawnCircle(_redCirclePrefab);
        }

        private void SpawnCircle(Circle prefab)
        {
            Circle circle = Instantiate(prefab, transform);
            float x = Random.Range(_topCircles[0].transform.position.x, _topCircles[2].transform.position.x);
            circle.transform.position = new Vector3(x, _topCircles[0].transform.position.y + 1, 0);
            circle.Init(_bet, _currency);
        }

        private void ChangeBoard(int value)
        {
            foreach (GameObject o in _cash)
            {
                Destroy(o);
            }
            _cash.Clear();
            _topCircles.Clear();
            switch (value)
            {
                case 0:
                    SpawnBoard(Row12, 14);
                    break;
                case 1:
                    SpawnBoard(Row14, 16);
                    break;
                case 2:
                    SpawnBoard(Row16, 18);
                    break;
            }
        }

        private void SpawnBoard(byte rowCount, byte columnCount)
        {
            transform.localScale = Vector3.one;
            transform.position = Vector3.zero;
            byte columns = columnCount;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GameObject circle = Instantiate(_circlePrefab, transform);
                    circle.transform.position = new Vector3(-columns * 0.5f + j, i);
                    _cash.Add(circle);
                    if (i + 1 == rowCount)
                        _topCircles.Add(circle);
                }
                columns -= 1;
            }

            SpawnBetFields(rowCount, columnCount);
            
            transform.position = new Vector3(0, YOffset, 0);

            switch (rowCount)
            {
                case Row12:
                    transform.localScale = Vector3.one * 0.9f;
                    break;
                case Row14:
                    transform.localScale = Vector3.one * 0.8f;
                    break;
                case Row16:
                    transform.localScale = Vector3.one * 0.7f;
                    break;
                default:
                    transform.localScale = Vector3.one;
                    return;
            }
        }

        private void SpawnBetFields(byte rowCount, byte columnCount)
        {
            byte index = 0;
            for (int i = 0; i < columnCount - 1; i++)
            {
                BetField greenField = Instantiate(_greenBetFieldPrefab, transform);
                _cash.Add(greenField.gameObject);
                BetField orangeField = Instantiate(_orangeBetFieldPrefab, transform);
                _cash.Add(orangeField.gameObject);
                BetField redField = Instantiate(_redBetFieldPrefab, transform);
                _cash.Add(redField.gameObject);
                if (i % 2 == 0)
                {
                    Vector3 pos = new Vector3(index - 0.5f, GreenYOffset, 0);
                    greenField.transform.position = pos;
                    pos.y = OrangeYOffset;
                    orangeField.transform.position = pos;
                    pos.y = RedYOffset;
                    redField.transform.position = pos;
                }
                else
                {
                    index++;
                    Vector3 pos = new Vector3(-index - 0.5f, GreenYOffset, 0);
                    greenField.transform.position = pos;
                    pos.y = OrangeYOffset;
                    orangeField.transform.position = pos;
                    pos.y = RedYOffset;
                    redField.transform.position = pos;
                }
                switch (rowCount)
                {
                    case Row12:
                        greenField.Init(_config.Green12Bets[index]);
                        orangeField.Init(_config.Orange12Bets[index]);
                        redField.Init(_config.Red12Bets[index]);
                        break;
                    case Row14:
                        greenField.Init(_config.Green14Bets[index]);
                        orangeField.Init(_config.Orange14Bets[index]);
                        redField.Init(_config.Red14Bets[index]);
                        break;
                    case Row16:
                        greenField.Init(_config.Green16Bets[index]);
                        orangeField.Init(_config.Orange16Bets[index]);
                        redField.Init(_config.Red16Bets[index]);
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
