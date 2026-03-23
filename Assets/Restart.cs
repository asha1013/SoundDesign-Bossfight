using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class RestartGame : MonoBehaviour
{
    // 此方法可以绑定到 UI 按钮的 OnClick 事件

    public AK.Wwise.Event buttonClick;
    public void RestartGameButtonClicked()
    {
        buttonClick.Post(gameObject);
        // 加载名为 "GameScene" 的场景
        SceneManager.LoadScene("BossFight");
       
    }
}

