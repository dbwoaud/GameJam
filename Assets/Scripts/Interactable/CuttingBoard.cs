using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform socket;
    [SerializeField] List<CookDataSO> slicableIngredients;
    List<int> slicableIngredientIndexes;
    [SerializeField] private ParticleSystem chopVFX;
    private Ingredient onBoard;

    private void Start()
    {
        slicableIngredientIndexes = new();
        foreach (CookDataSO c in slicableIngredients)
        {
            if (c.cookData.itemIndexs.Count > 1) { Debug.LogError("����� �Ǽ��� ���������ϴ�."); continue; }
            slicableIngredientIndexes.Add(c.cookData.itemIndexs[0]);
        }
    }

    public void OnGrab(PlayerInput player)
    {
        //  �� ��� ������ ����
        if (onBoard == null && player.HeldItem is Ingredient ingredient)
        {
            if (!slicableIngredientIndexes.Contains(ingredient.IngredientIndex)) return;

            ingredient.SetCuttingBoard(this);

            onBoard = (Ingredient)player.TakeFromHands();
            onBoard.AttachTo(socket != null ? socket : transform);
            return;
        }

        //  ������ �ִ� ��� ���
        if (onBoard != null && !player.IsHolding)
        {
            onBoard.SetCuttingBoard(null);
            player.Hold(onBoard);
            onBoard = null;
        }
    }

    public void OnInteract(PlayerInput player)
    {
        if (onBoard == null) return;

        //  �̹� �丰 ��ᰡ ���� �� �� �� ���� ���� ���ɼ� ���Ƽ� return
        if (!slicableIngredientIndexes.Contains(onBoard.IngredientIndex)) return;

        SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("Slice"));
        player.TriggerCut();

        // VFX ȿ�� �߰�
        ParticleSystem ps = Instantiate(chopVFX, socket.transform.position, socket.transform.rotation);
        ps.Play();
        Destroy(ps.gameObject, 1.5f);

        onBoard.Chop();
    }

    public void OnChopComplete()
    {
        foreach (CookDataSO c in slicableIngredients)
        {
            if (c.cookData.itemIndexs.Count > 1) { Debug.LogError("����� �Ǽ��� ���������ϴ�."); continue; }
            if (c.cookData.itemIndexs[0] == onBoard.IngredientIndex)
            {
                Destroy(socket.GetChild(0).gameObject);
                GameObject slicedIngredient = Instantiate(c.cookData.result, socket);
                onBoard = slicedIngredient.GetComponent<Ingredient>();
            }
        }
    }

    [Button]
    public void Test()
    {
        Debug.Log($"[CuttingBoard] {onBoard.name}");
    }
}