using UnityEngine;
using UnityEngine.UI;

public class MouseFollow : MonoBehaviour
{   //ĳ��Ƽ ��ĥ�� ���ÿ��� �ذ� �ʿ� -- ���̵�� ���콺������ ���� �ʻ�ȭ�� ���ð���
    [SerializeField] Image m_line;
    public bool m_path_select;
    public Vector2 m_vec;
    public GameObject m_focus_object;
    public GameObject m_focus_enemy;
    public RaycastHit m_hit;
    //���߱� , �� �ʿ�
    LineRenderer m_lr;
    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;
        m_lr = GetComponent<LineRenderer>();
        m_lr.startWidth = 0.05f;
        m_lr.endWidth = 0.05f;
    }
    void Update()
    {
        if (m_path_select) //�� �߱� 
        {
            m_lr.SetPosition(0, m_focus_object.transform.position);
            if (m_hit.collider != null)
            {
                m_lr.SetPosition(1, m_focus_enemy.transform.position);
            }
            else
            {
                ; m_lr.SetPosition(1, m_vec);
            }
        }

        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư
        {
            if(m_focus_object != null) m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ŭ���� ���콺��ġ�� Ray����
            Physics.Raycast(m_ray, out m_hit);
            if (m_hit.collider != null && m_hit.transform.tag != "Enemy")//���� �ƴѰŸ� ����
            {
                m_focus_object = m_hit.collider.gameObject;
                m_hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //�̵��ϸ� ���� ����� 
            }
        }
        else if (Input.GetMouseButtonDown(1)) //���콺 ������ ��ư
        {
            if(m_focus_enemy != null) m_focus_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(m_ray, out m_hit);
            m_path_select = true;
            if (m_hit.collider != null && m_hit.collider.tag == "Enemy") //�� ���ý�
            {
                m_focus_enemy = m_hit.collider.gameObject;
                m_hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            else m_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (m_path_select) //�װų� ���̻������ ��� ���� �ؾ���
        {
            if(m_hit.collider != null) m_vec = m_hit.transform.position;
            if (m_vec.x > 0) m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);
            else m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, m_vec, Time.deltaTime * 0.5f);
        }
    }
}
