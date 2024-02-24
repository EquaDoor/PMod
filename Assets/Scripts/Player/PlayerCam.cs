using UnityEngine;

public class PlayerCam : MonoBehaviour
{
	[SerializeField] private float sensX = 250f;
	[SerializeField] private float sensY = 250f;

	[SerializeField] private float rotationClamp = 90f;

	[SerializeField] private Transform player;
	
	private float xRotation;
	private float yRotation;

	private void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		// Ввод мышкой
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
	
		// Выставление нынешнего поворота камеры
		yRotation += mouseX;
		xRotation -= mouseY;
		
		// Вертикальный блок
		xRotation = Mathf.Clamp(xRotation, -rotationClamp, rotationClamp);
		
		// Поворот камеры и модели игрока
		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		player.transform.rotation = Quaternion.Euler(0, yRotation, 0);
	}
}