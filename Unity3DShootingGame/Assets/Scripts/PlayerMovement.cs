using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        transform.Translate(dir * speed * Time.deltaTime);

        #region �ʱ�
        //transform.Translate(Vector3 dir);
        //���� ������Ʈ�� �̵���Ű�� ���� �뵵
        //���� ������Ʈ�� ��ġ�� Vector3 �������� �̵��ϰ� �˴ϴ�.
        //(���� ������ ���õ� ������ ���������� �ʰ� �ܼ��� �̵� ������� ����)
        //--> �⺻���� ������

        //transform.position�� ���� ����ص� ��ġ�� ������Ʈ�� position�� �ٲ� �� �ֽ��ϴ�.
        //-- > �ַ� ��Ż ���� ������ ������ ���� �� ȿ����
        //position�� ���� ������ ������Ʈ���� rigidbody�� ���� �ʴ� �� �����ϴ�.
        //(�ݶ��̴����� rigidbody�� ������� ��ġ�� �����ؾ��ϴ� ��찡 �߻��մϴ�.)

        //Rigidbody�� ���� ������Ʈ�� ���� ������ �����ؼ� �浹, ��, �߷� ����
        //�������� ��ȣ�ۿ��� �����ϰ� ���ִ� ������Ʈ�Դϴ�.

        //Rigidbody.AddForce(Vector3 dir, ForceMode mode);
        //�������� ������ ���� �������� �����ϰ�, ���� �ִ� ������ ���� ���������� ó���� ��,
        //�������� ���� �������� ó���� �� �ֽ��ϴ�.
        #endregion

    }
}
