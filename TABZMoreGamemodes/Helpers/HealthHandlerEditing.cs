namespace TABZMGamemodes
{
    public class HealthHandlerEditing
    {
        private const float DEFAULT_RESPAWN_TIME = 5f;
        private static float RespawnTime = DEFAULT_RESPAWN_TIME;

        public static void ChangeRespawnTime(float respawnTime) { RespawnTime = respawnTime; }
        public static void ResetRespawnTime() { RespawnTime = DEFAULT_RESPAWN_TIME; }
        public static float GetRespawnTime() { return RespawnTime; }

        public static void KillLocalPlayer()
        {
            HealthHandler hH = NetworkManager.LocalPlayerPhotonView.GetComponent<HealthHandler>();
            hH.TakeDamage(0f, null, true);
        }
    }
}
