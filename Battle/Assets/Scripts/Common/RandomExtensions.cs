using System;

/// <summary>
/// ƒ‰ƒ“ƒ_ƒ€”’l
/// </summary>
namespace Common
{
    public static class RandomExtensions
    {
        /// <summary>
        /// 0.0f <= x < 1.0f ‚Ì—”‚ğ•Ô‚·
        /// </summary>
        public static float NextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }
    }
}
