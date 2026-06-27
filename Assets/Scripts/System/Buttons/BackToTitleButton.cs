using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class BackToTitleButton : CustomButton
{

    protected override async void PressingAction()
    {
        await RelayManager.NetworkRunner.Shutdown();
        Destroy(RelayManager.NetworkRunner.gameObject);
        SceneManager.LoadScene("Title");
    }
}
