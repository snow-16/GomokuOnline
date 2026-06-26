using UnityEngine.SceneManagement;

public class RoomLeftRoomButton : CustomButton
{
    protected override async void PressingAction()
    {
        await RelayManager.NetworkRunner.Shutdown();
        Destroy(RelayManager.NetworkRunner.gameObject);
        SceneManager.LoadScene("Title");
    }
}
