using UnityEngine;

//�߻�뿡 ������ ��ũ��Ʈ
//�߻� ��ư�� ������ ��� �Ѿ� �߻�
//���� �Ѿ˿��� ���⿡ ���� �̵��ϴ� �ڵ尡 ������ �Ǿ�����.
//���� ��ư ������ �����Ǳ�޳� ������ָ� ���� �Ϸ�

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory; //�Ѿ� ����

    public GameObject firePosition;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletFactory, firePosition.transform.position, Quaternion.identity);   
        }
    }
}
