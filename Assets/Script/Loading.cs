using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    static string sceneName;
    [SerializeField] Slider progress;
    public static void LoadScene(string name)
    {
        sceneName = name;
        //���� ����� ���� �ҷ����� �Լ�
        //���� �� �ҷ����� ������ �ƹ� �۾��� ���� �� �� �����ϴ�.
        SceneManager.LoadScene("Loading");
    }


    void Start()
    {

        StartCoroutine(nameof(LoadProcess));
        
    }
    IEnumerator LoadProcess()
    {
        // �񵿱� ����� ���� �ҷ����� �Լ��Դϴ�.
        // ���� �ҷ����� �۾��� �ϴ� ���߿� �ٸ� �۾��� ���� �Ҽ��ִ� �����Դϴ�.
        AsyncOperation scene =SceneManager.LoadSceneAsync(sceneName);

        //allowSceneActivation ���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��� ������ �����ϴ� ����Դϴ�.
        //allowSceneActivation ���� false�� ���� 90 ������ �ҷ����� ���� ������ �Ѿ�� �ʰ� ��ٸ��ϴ�.
        scene.allowSceneActivation = false;

        float timer = 0f;
        //isDone ���� �ε��� ������ �ʾҴٸ� �ݺ��ϵ��� �����մϴ�.
        while (!scene.isDone)
        {
            //�ݺ����� �ѹ� �ݺ� �� �� ���� ����Ƽ���� ������� �Ѱ��ݴϴ�.
            // �ݺ����� ���ư��� ������� �Ѱ����� ������ �ݺ����� ������ ������
            // ȭ���� ���ŵ��� �ʾ� progress ��������Ʈ ���� �ʴ� ���� �߻� �Ҽ� �ֽ��ϴ�.
            yield return null;

            if(scene.progress<0.9f)
            {
                progress.value = scene.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                //progress �� value 0.9 ���� 1�� 1�ʿ� ���ļ� ä�쵵�� �����մϴ�.
                progress.value = Mathf.Lerp(0.9f, 1f, timer);
                //������ ���� �־����� �� �� ���̿� ��ġ�� ���� �����ϱ� ���Ͽ� ������ ���� ���������� ����ϴ� ����Դϴ�.
                if(progress.value>=1f)
                {
                    scene.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
    
 
}