namespace TABZMGamemodes
{
    public class HealthHandlerEditing
    {
        private const float DEFAULT_RESPAWN_TIME = 5f;
        private static float RespawnTime = DEFAULT_RESPAWN_TIME;

        public static void ChangeRespawnTime(float respawnTime) { RespawnTime = respawnTime; }
        public static void ResetRespawnTime() { RespawnTime = DEFAULT_RESPAWN_TIME; }
        public static float GetRespawnTime() { return RespawnTime; }
        
        public delegate void OnTakeDamageDel(HealthHandler damaged, float damage, PhotonPlayer damager, bool isKillingBlow);
        public static event OnTakeDamageDel OnTakeDamage;
        public static void InvokeOnTakeDamage(HealthHandler damaged, float damage, PhotonPlayer damager, bool isKillingBlow)
        {
            OnTakeDamage?.Invoke(damaged, damage, damager, isKillingBlow);
        }

        public static void KillLocalPlayer()
        {
            HealthHandler hH = NetworkManager.LocalPlayerPhotonView.GetComponent<HealthHandler>();
            hH.TakeDamage(0f, null, true);
        }        
    }
}
