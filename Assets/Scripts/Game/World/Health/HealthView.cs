namespace PlatformerPrototype.Game.World.Health
{
    internal sealed class HealthView : UnityEngine.MonoBehaviour, IHealthView
    {
        [UnityEngine.SerializeField] private TMPro.TMP_Text _view;

        private IHealth _health;

        public void Init(IHealth health)
        {
            _health = health;
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
                Subscribe();
            else
                Unsubscribe();
        }

        private void Subscribe()
        {
            _health.Changed += OnChanged;
        }

        private void Unsubscribe()
        {
            _health.Changed -= OnChanged;
        }

        private void OnChanged(IHealth health)
        {
            _view.text = $"{health.CurrentHp}/{health.MaxHp}";
        }
    }
}
