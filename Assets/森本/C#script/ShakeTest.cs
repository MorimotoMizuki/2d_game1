using UnityEngine;

/// <summary>
/// �h��e�X�g�p
/// </summary>
public class ShakeTest : MonoBehaviour
{
    [SerializeField] private ShakeByRandom shakeByRandom;

    /// <summary>
    /// �Z���h��e�X�g
    /// </summary>
    public void PushButton1()
    {
        var duration = 1.0f;
        var strength = 30.0f;
        var vibrato = 30.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }

    /// <summary>
    /// �����h��e�X�g
    /// </summary>
    public void PushButton2()
    {
        var duration = 3.0f;
        var strength = 30.0f;
        var vibrato = 30.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }

    /// <summary>
    /// �����U���e�X�g
    /// </summary>
    public void PushButton3()
    {
        var duration = 3.0f;
        var strength = 30.0f;
        var vibrato = 100.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }

    /// <summary>
    /// �����h��e�X�g
    /// </summary>
    public void PushButton4()
    {
        var duration = 3.0f;
        var strength = 100.0f;
        var vibrato = 30.0f;
        shakeByRandom.StartShake(duration, strength, vibrato);
    }
}
