namespace TABZMGamemodes
{
    public class HealthHandlerEditing
    {
        private const float DEFAULT_RESPAWN_TIME = 5f;
        private static float RespawnTime = DEFAULT_RESPAWN_TIME;

        public static void ChangeRespawnTime(float respawnTime) { RespawnTime = respawnTime; }
        public static void ResetRespawnTime() { RespawnTime = DEFAULT_RESPAWN_TIME; }
        public static float GetRespawnTime() { return RespawnTime; }
        
        public delegate void OnTakeDamageDel(HealthHandler damaged, float damage, PhotonPlayer damager);
        public static event OnTakeDamageDel OnTakeDamage;
        public static event OnTakeDamageDel OnTakeDamageEvenIfDead;
        
        public static event OnTakeDamageDel OnKill;

        public static void InvokeOnTakeDamage(HealthHandler damaged, float damage, PhotonPlayer damager)
        {
            OnTakeDamage?.Invoke(damaged, damage, damager);
        }
        public static void InvokeOnTakeDamageEvenIfDead(HealthHandler damaged, float damage, PhotonPlayer damager)
        {
            OnTakeDamageEvenIfDead?.Invoke(damaged, damage, damager);
        }

        public static void InvokeOnKill(HealthHandler damaged, float damage, PhotonPlayer damager)
        {
            OnKill?.Invoke(damaged, damage, damager);
        }

        public static void KillLocalPlayer()
        {
            HealthHandler hH = NetworkManager.LocalPlayerPhotonView.GetComponent<HealthHandler>();
            hH.TakeDamage(0f, null, true);
        }        
    }
}
