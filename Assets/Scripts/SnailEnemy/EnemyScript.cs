using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    public bool isAgred = false;
    public bool isFollowing = false;
    public bool testScreemer = false;
    public bool inViewField = false;
    public bool isStaned = false;

    private bool isTweening;
    [SerializeField] Transform player;
    [SerializeField] Transform p;
    [SerializeField] GameObject patrol1, patrol2, patrol3;
    private GameObject route;

    public List<Transform> points;
    private NavMeshAgent agent;

    [SerializeField] List<Transform> teleportPoints;
    [SerializeField] List<GameObject> offItems;

    private int index = 0;

    public float angleView;

    public GameObject screemHead, flare,losePanel;

    private AudioSource audio;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //ChangeRoute(patrol1);
    }
    private void Start()
    {

        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!isStaned)
        {
            PlayerDetetector();
            if (isAgred)
            {
                agent.destination = player.position;
                AudioManager();
                CheckForLose();
            }

            if (agent.remainingDistance <= agent.stoppingDistance && !isAgred)
            {
                foreach (Transform point in teleportPoints)
                {
                    if (agent.destination.x == point.position.x && agent.destination.z == point.position.z)
                    {
                        TeleportSnail(points.IndexOf(point));
                    }
                }
                PointChanger();
            }
        }
    }
    public void Staned()
    {
        isStaned = true;
        agent.enabled = false;
        transform.DORotate(new Vector3(-105, 0, 0), 1);
        audio.enabled = false;
        StartCoroutine(StopStaned());
    }
    public void HeardThePlayer()
    {
        isAgred = true;
        StartCoroutine(StopFollowing());
    }
    public void ChangeRoute(GameObject marshrut)
    {
        if (marshrut != route)
        {
            route = marshrut;
            points.Clear();
            foreach (Transform point in marshrut.GetComponentsInChildren<Transform>())
            {
                if (point.gameObject != marshrut)
                    points.Add(point);
            }
            PointChanger();
        }
    }
    private void AudioManager()
    {
        if (!isTweening & audio.enabled)
        {
            StartCoroutine(DoDOTweenMethods());
        }
    }
    private void PointChanger()
    {
        agent.SetDestination(points[index].position);

        index += 1;
        if (index == points.Count)
            index = 0;

    }
    private void TeleportSnail(int i)
    {
        if (Vector3.Distance(player.position, points[i + 1].position) >= 8)
        {
            agent.enabled = false;
            transform.position = points[i + 1].position;
            agent.enabled = true;
        }
    }

    private void PlayerDetetector()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, player.position - transform.position);
        Physics.Raycast(ray, out hit);
        Debug.DrawLine(ray.origin, hit.point, Color.white);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == player.gameObject)
            {
                var playerPos = new Vector3(player.position.x, 0, player.position.z);
                var snailPos = new Vector3(transform.position.x, 0, transform.position.z);
                var toPlayer = (playerPos - snailPos).normalized;
                var res = Vector3.Dot(transform.forward, toPlayer);
                if (Mathf.Acos(res) * Mathf.Rad2Deg < angleView / 2 | Vector3.Distance(snailPos, playerPos) <= 4)
                {
                    isAgred = true;
                    StopCoroutine(StopFollowing());
                }
                inViewField = true;
            }
            else
            {
                inViewField = false;
                if (isAgred)
                {                  
                    StartCoroutine(StopFollowing());
                }
            }
        }
    }
    
    private void CheckForLose()
    {
        //Debug.Log(Vector3.Distance(agent.transform.position, player.position));
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= agent.stoppingDistance + 0.3f & agent.remainingDistance != 0 && Vector3.Distance(agent.transform.position, player.position) < 3)
        {
            Debug.Log($"{agent.remainingDistance} {agent.pathEndPosition}");
            flare.SetActive(false);
            screemHead.SetActive(true);
            foreach (GameObject item in offItems)
            {
                item.SetActive(false);
            }
            StopGame(2.5f);
            agent.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            audio.enabled = false;
            losePanel.SetActive(true);
        }
    }
    public void StopGame(float seconds)
    {
        player.GetComponent<MouseController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;        
        StartCoroutine(Loose(seconds));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < points.Count; i++)
        {
            if (i != points.Count - 1)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            else
                Gizmos.DrawLine(points[i].position, points[0].position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 4);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, angleView / 2, 0) * transform.forward) * 5);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, -angleView / 2, 0) * transform.forward) * 5);
    }

    IEnumerator<WaitForSeconds> DoDOTweenMethods()
    {
        isTweening = true;

        if (inViewField)
        {
            audio.DOFade(1, 3f);
        }
        else
        {
            audio.DOFade(0, 3f);
        }

        yield return null;

        isTweening = false;
    }

    IEnumerator<WaitForSeconds> Loose(float time)
    {
        yield return new WaitForSeconds(time);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("Menu");
    }
    public IEnumerator<WaitForSeconds> StopFollowing()
    {
        yield return new WaitForSeconds(5f);
        if (!inViewField)
        {
            isAgred = false;
            //audio.enabled = false;
        }
    }
    IEnumerator<WaitForSeconds> StopStaned()
    {
        yield return new WaitForSeconds(10f);
        isStaned = false;
        audio.enabled = true;
        agent.enabled = true;
        transform.DORotate(Vector3.zero, 1);
    }
}
