using UnityEngine;

namespace Library
{
    public class EnemyAttack : MonoBehaviour
    {
        public static void Attack(EnemyAttackMode mode, Transform t)
        {
            switch (mode.type)
            {
                case EnemyAttackType.Straight:
                    StraightAttack(t.position, mode.bulletPrefab, mode.spacing, mode.velocity, mode.bulletNum);
                    break;
                case EnemyAttackType.Radial:
                    RadialAttack(t.position, mode.bulletPrefab, mode.velocity, mode.bulletNum);
                    break;
            }
        }
        
        public static void Attack(EnemyAttackMode mode, Vector3 pos)
        {
            switch (mode.type)
            {
                case EnemyAttackType.Straight:
                    StraightAttack(pos, mode.bulletPrefab, mode.spacing, mode.velocity, mode.bulletNum);
                    break;
                case EnemyAttackType.Radial:
                    RadialAttack(pos, mode.bulletPrefab, mode.velocity, mode.bulletNum);
                    break;
            }
        }
        
        static void StraightAttack(Vector3 pos, GameObject prefab, float spacing ,float v, int n)
        {
            switch (n % 2)
            {
                case 0 :
                    EvenStraightAttack(pos, prefab, spacing, v, n);
                    break;
                case 1:
                    SingularStraightAttack(pos, prefab, spacing,v, n);
                    break;
            }
        }

        static void SingularStraightAttack(Vector3 pos, GameObject prefab, float spacing, float velocity, int num)
        {
            int m = num / 2;
            for (int i = -m; i < m + 1; i++)
            {
                Vector3 p = pos + new Vector3(i * (prefab.transform.localScale.x + spacing), 0);
                GameObject bullet = Instantiate(prefab, p, Quaternion.identity);
                
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * velocity;
            }
        }

        static void EvenStraightAttack(Vector3 pos, GameObject prefab, float spacing, float velocity, int num)
        {
            float l = prefab.transform.localScale.x + spacing;
            for (int i = 0; i < num / 2; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    float xPos = i == 0 ? l / 2 : l * (i + 0.5f);
                    float dir = j == 0 ? 1 : -1;

                    GameObject bullet = Instantiate(prefab, new Vector3(xPos * dir, 0), Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * velocity;
                }
            }
        }
        
        static void RadialAttack(Vector3 pos, GameObject prefab, float v, int n)
        {
            float angle = 360f / n;
            for (int i = 0; i < n; i++)
            {
                Vector3 dir = new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), 
                    Mathf.Sin(angle * i * Mathf.Deg2Rad));
                GameObject bullet = Instantiate(prefab, pos + dir, Quaternion.identity);

                bullet.GetComponent<Rigidbody2D>().velocity = dir * v;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle * i);
            }
        }
    }
}