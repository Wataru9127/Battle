/// <summary>
/// ƒ^[ƒ“”ŠÇ—
/// </summary>
namespace Common
{
    public class TurnCounter
    {
        public int CurrentTurn { get; private set; } = 0;

        public void TurnIncrement()
        {
            CurrentTurn++;
        }
    }
}
