using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MosNavManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mapPrefab;

    private Vector3 generatePos = new Vector3(50, 0, 50);

    NavMeshSurface[] surfaces;
    // Start is called before the first frame update
    void Start()
    {
        surfaces = gameObject.GetComponentsInChildren<NavMeshSurface>();
        StartCoroutine(UpdateNav());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMavmesh()
    {
        foreach (var s in surfaces)
        {
            s.RemoveData();
            s.BuildNavMesh();
        }
    } 

    IEnumerator UpdateNav()
    {
        while (true)
        {
            Debug.Log("½ÇÇà");
            GenerateMavmesh();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
