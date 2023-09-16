using System;

[Serializable]
public struct PlayerData
{
    public PlayerMovementData MovementData;
    public PlayerMeshData MeshData;
}

[Serializable]
public struct PlayerMovementData
{
    public float ForwardSpeed;
    public float SidewaySpeed;
}

[Serializable]
public struct PlayerMeshData
{
    public float ScaleCounter;
}
