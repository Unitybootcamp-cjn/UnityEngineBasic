using UnityEngine;

//머티리얼의 offset을 조절해서 배경이 스크롤되는 느낌의 작업 진행

public class Background : MonoBehaviour
{
    public Material backgroundMaterial;
    public Texture2D newTexture;

    public float speed = 0.2f;


    private void Update()
    {
        //방향 위 고정
        Vector2 dir = Vector2.up;

        if( GameManager.instance.isGameOver == false)
        {
            backgroundMaterial.mainTextureOffset += Time.deltaTime * speed * dir;
        }
    }

    [ContextMenu("텍스처 변경")]
    public void TextureChange()
    {
        backgroundMaterial.SetTexture("_BaseMap", newTexture);
        // _BaseMap은 Universal Render PipeLine(URP)에서 사용하는 셰이더 속성의 이름

        // Built in 환경(이전 모드)일 경우에는 다음과 같이 코드를 작성합니다.
        // Standard Shader(기본 쉐이더)에서 지정하고 있는 기본 텍스처의 이름입니다.
        //backgroundMaterial.SetTexture("_MainTex", newTexture);
    }
}
