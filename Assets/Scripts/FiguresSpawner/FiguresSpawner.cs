using System.Linq;
using Assets.Scripts.Figures;
using Assets.Scripts.Static;
using UnityEngine;

namespace Assets.Scripts.FiguresSpawner
{
    public class FiguresSpawner : MonoBehaviour
    {
        [SerializeField] private HexFigure[] _hexFigurePrefabs;
        [SerializeField] private FigureSpawnPoint[] _figuresSpawnPoints;
        [SerializeField] private Color[] _figureColors;


        public void Init()
        {
            foreach (FigureSpawnPoint spawnPoint in _figuresSpawnPoints)
                SpawnFigure(spawnPoint, _hexFigurePrefabs[Random.Range(0, _hexFigurePrefabs.Length)]);
        }
        
        private void SpawnFigure(FigureSpawnPoint spawnPoint, HexFigure figurePrefab)
        {
            spawnPoint.IsEmpty = false;
            
            HexFigure newFigure = Instantiate(figurePrefab.gameObject, spawnPoint.transform.position, Quaternion.identity).GetComponent<HexFigure>();
            newFigure.FigureSpawnPoint = spawnPoint;
            newFigure.SetColor(_figureColors[Random.Range(0, _figureColors.Length)]);
            newFigure.Placed += SpawnRandomFigure;
            newFigure.transform.localScale = Vector3.one * Constants.InactiveScaleFactor;
        }

        private void SpawnRandomFigure()
        {
            FigureSpawnPoint emptySpawnPoint = _figuresSpawnPoints.FirstOrDefault(data => data.IsEmpty);
            SpawnFigure(emptySpawnPoint, _hexFigurePrefabs[Random.Range(0, _hexFigurePrefabs.Length)]);
        }
    }
}