using UnityEngine;

namespace Backend
{
    [CreateAssetMenu(fileName = "New Progression", menuName = "Backend/Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        public ProgressionType Type;
    }
}