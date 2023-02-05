using UnityEngine;

public class TreeGrower : MonoBehaviour
{
    [SerializeField]
    private GameObject littleTree, mediumTree, bigTree;

    private TreeState state;

    void Start()
    {
        state = TreeState.Little;
        littleTree.SetActive(true);
        mediumTree.SetActive(false);
        bigTree.SetActive(false);
    }

    public void PassMedium()
    {
        if (state != TreeState.Little) return;
        state = TreeState.Medium;
        littleTree.SetActive(false);
        mediumTree.SetActive(true);
        bigTree.SetActive(false);
    }

    public void PassBig()
    {
        if (state != TreeState.Medium) return;
        state = TreeState.Big;
        littleTree.SetActive(false);
        mediumTree.SetActive(false);
        bigTree.SetActive(true);
    }
}

enum TreeState
{
    Little,
    Big,
    Medium
}
