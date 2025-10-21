using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SO_Enemy _data;
    private Transform _player;

    public void Init(Transform player, SO_Enemy data)
    {
        _player = player;
        _data = data;
    }

    private void Update()
    {
        
    }

}
