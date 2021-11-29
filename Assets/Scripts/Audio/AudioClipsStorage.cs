using UnityEngine;

[CreateAssetMenu(fileName ="AudioStorage", menuName = "Audio Data", order = 51)]
public class AudioClipsStorage : ScriptableObject
{
    [SerializeField] private AudioClip _swordHit;
    [SerializeField] private AudioClip _run;
    [SerializeField] private AudioClip _punch;

    public AudioClip SwordHit => _swordHit;
    public AudioClip Punch => _punch;
    public AudioClip Run => _run;
}
