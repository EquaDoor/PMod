using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("Holder")]
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private int currentIndex;

    [Header("Init")]
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;


    private void Start() {
        foreach (Transform c in transform) { // получение оружия из дочерней папки
            var tmp = c.GetComponent<Weapon>(); // получение компонента из оружия
            weapons.Add(tmp); // добавление в список
            tmp.Init(cam, anim); // инициализация
        }
    }

    private void Update() {

        // выбор оружия
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
            weapons[currentIndex].gameObject.SetActive(false); // скрыть предыдущее оружие пока не поменяли индекс
            currentIndex++; // следующее оружие
            if(currentIndex > weapons.Count - 1) currentIndex = 0; // сделать индекс первым если выше количества оружия
            weapons[currentIndex].gameObject.SetActive(true); // показать следующее оружие
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
            weapons[currentIndex].gameObject.SetActive(false); // скрыть предыдущее оружие пока не поменяли индекс
            currentIndex--; // предыдущее оружие
            if(currentIndex < 0) currentIndex = weapons.Count - 1; // сделать индекс последним если меньше 0
            weapons[currentIndex].gameObject.SetActive(true); // показать следующее оружие
        }


        // кд стрельбы
        if(weapons[currentIndex].rateTimer > 0) 
            weapons[currentIndex].rateTimer -= Time.deltaTime;

        // стрельба
        if(Input.GetKey(KeyCode.Mouse0) && weapons[currentIndex].rateTimer <= 0) 
            weapons[currentIndex].Shoot();

        // перезарядка
        if(Input.GetKeyDown(KeyCode.R) && weapons[currentIndex].currentAmmo < weapons[currentIndex].maxAmmo) 
            weapons[currentIndex].Reload();
    }

}
